
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.EASpecific;

namespace EAWorksetSharing
{
	/// <summary>
	/// Description of WorkingSetSharingWindow.
	/// </summary>
	public partial class WorkingSetSharingWindow : Form
	{
		private List<WorkingSet> allWorkingSets {get;set;}
		private List<User> allUsers {get;set;}
		private User currentUser {get;set;}
		
		public WorkingSetSharingWindow(List<WorkingSet> allWorkingSets,List<User> allUsers,User currentUser)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.allWorkingSets = allWorkingSets;
			this.allUsers = allUsers;
			this.currentUser = currentUser;
			// initialize content
			this.initializeWorkingSets();
			this.initializeUserList();
			// align filters
			this.alignUserFilters();
			this.alignWorkingSetFilters();
			//check if we have a current user
			if (this.currentUser == null)
			{
				this.meUserButton.Enabled = false;
				this.MyWorkingSetsButton.Enabled = false;
			}
			//enable sorting
			this.WorkingSetsList.ListViewItemSorter = new ListViewColumnSorter();
			this.userList.ListViewItemSorter = new ListViewColumnSorter();
			
		}
		/// <summary>
		/// align the user list filters with the columns
		/// </summary>
		private void alignUserFilters()
		{
			//adjust widths
			int interSpace = 5;
			this.userLoginFilter.Width = this.bottomAtZero(this.userLoginHeader.Width - interSpace);
			this.userFirstNameFilter.Width =  this.bottomAtZero(this.userFirstNameHeader.Width - interSpace);
			this.userLastNameFilter.Width = this.bottomAtZero(this.userLastNameHeader.Width - interSpace);
			//adjust positions
			this.userLoginFilter.Left = this.userList.Left;
			this.userFirstNameFilter.Left = this.userLoginFilter.Left + userLoginHeader.Width;
			this.userLastNameFilter.Left = this.userFirstNameFilter.Left + userFirstNameHeader.Width;
			
		}
		/// <summary>
		/// align the workingset filters with the columns
		/// </summary>
		private void alignWorkingSetFilters()
		{
			//adjust widths
			int interSpace = 5;
			this.WorkingSetNameFilter.Width = this.bottomAtZero(this.workingSetHeader.Width - interSpace);
			this.workingSetLoginFilter.Width = this.bottomAtZero(this.loginHeader.Width - interSpace);
			this.workingSetFirstNameFilter.Width =  this.bottomAtZero(this.firstNameHeader.Width - interSpace);
			this.workingSetLastNameFilter.Width = this.bottomAtZero(this.lastNameHeader.Width - interSpace);
			//adjust positions
			this.WorkingSetNameFilter.Left = this.WorkingSetsList.Left;
			this.workingSetLoginFilter.Left = this.WorkingSetNameFilter.Left + workingSetHeader.Width;
			this.workingSetFirstNameFilter.Left = this.workingSetLoginFilter.Left + loginHeader.Width;
			this.workingSetLastNameFilter.Left = this.workingSetFirstNameFilter.Left + firstNameHeader.Width;
		}
		/// <summary>
		/// returns 0 if the width is negative
		/// </summary>
		/// <param name="width">a width</param>
		/// <returns>0 if width is negative</returns>
		private int bottomAtZero(int width)
		{
			if (width < 0)
			{
				return 0;
			}
			else
			{
				return width;
			}
		}
		private void initializeWorkingSets()
		{
			//first clear all
			this.WorkingSetsList.Items.Clear();
			//then add all workingSets
			foreach (WorkingSet workingSet in this.allWorkingSets) 
			{
				//only add workingsets if they match the filter
				if (filterWorkingSet(workingSet))
				{
					ListViewItem item = new ListViewItem(workingSet.name);
					item.ImageIndex = 0;
					if (workingSet.user != null)
					{
						item.SubItems.Add(workingSet.user.login);
						item.SubItems.Add(workingSet.user.firstName);
						item.SubItems.Add(workingSet.user.lastName);
					}
					else
					{
						item.SubItems.Add("-");
						item.SubItems.Add("-");
						item.SubItems.Add("-");
					}
					//set tag
					item.Tag = workingSet;
					//add to list
					this.WorkingSetsList.Items.Add(item);
				}
			}
		}
		/// <summary>
		/// filters the workingsets based on the values in the filters entered by the user
		/// </summary>
		/// <param name="workingSet">the workingset to be filtered</param>
		/// <returns>true if the workingset matches the filter</returns>
		private bool filterWorkingSet(WorkingSet workingSet)
		{
			bool pass = true;
			if (pass && this.WorkingSetNameFilter.TextLength > 0)
				pass = workingSet.name.StartsWith(this.WorkingSetNameFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			if (pass && this.workingSetLoginFilter.TextLength > 0)
				pass = workingSet.user != null && workingSet.user.login.StartsWith(this.workingSetLoginFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			if (pass && this.workingSetFirstNameFilter.TextLength > 0)
				pass = workingSet.user != null && workingSet.user.firstName.StartsWith(this.workingSetFirstNameFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			if (pass && this.workingSetLastNameFilter.TextLength > 0)
				pass = workingSet.user != null && workingSet.user.lastName.StartsWith(this.workingSetLastNameFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			return pass;
		}
		/// <summary>
		/// initialises the list of users
		/// </summary>
		private void initializeUserList()
		{
			//first clear all
			this.userList.Items.Clear();
			//then start adding the elements
			foreach (User user in this.allUsers) 
			{
				if (this.filterUser(user))
				{
					ListViewItem item = new ListViewItem(user.login);
					item.ImageIndex = 1;
					item.SubItems.Add(user.firstName);
					item.SubItems.Add(user.lastName);
					//set user as tag
					item.Tag = user;
					//add to list
					this.userList.Items.Add(item);
				}
			}
		}
		/// <summary>
		/// filters the given user agains the filters set on the gui
		/// </summary>
		/// <param name="user">the user to filter</param>
		/// <returns>true if the user matches the filter</returns>
		private bool filterUser(User user)
		{
			bool pass = true;
			if (pass && this.userLoginFilter.TextLength > 0)
				pass = user.login.StartsWith(this.userLoginFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			if (pass && this.userFirstNameFilter.TextLength > 0)
				pass = user.firstName.StartsWith(this.userFirstNameFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			if (pass && this.userLastNameFilter.TextLength > 0)
				pass = user.lastName.StartsWith(this.userLastNameFilter.Text,StringComparison.InvariantCultureIgnoreCase);
			return pass;
		}
		
		

		
		void AllWorkingSetsButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.WorkingSetsList.Items) 
			{
				item.Checked = true;	
			}
		}
		
		void NonWorkingSetsButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.WorkingSetsList.Items) 
			{
				item.Checked = false;
			}
		}
		
		void MyWorkingSetsButtonClick(object sender, EventArgs e)
		{
			if(this.currentUser != null)
			{
				foreach (ListViewItem item in this.WorkingSetsList.Items) 
				{
					if (item.SubItems[1].Text == this.currentUser.login)
					{
						item.Checked = true;	
					}
					else
					{
						item.Checked = false;
					}
				}
			}
		}
		
		void AllUsersButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.userList.Items) 
			{
				item.Checked = true;	
			}
		}
		
		void NonUsersButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.userList.Items) 
			{
				item.Checked = false;	
			}
		}
		
		void MeUserButtonClick(object sender, EventArgs e)
		{
			if(this.currentUser != null)
			{
				foreach (ListViewItem item in this.userList.Items) 
				{
					if (item.Text == this.currentUser.login)
					{
						item.Checked = true;	
					}
					else
					{
						item.Checked = false;
					}
				}
			}
		}
		
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Hide();
		}
		
		void CopyButtonClick(object sender, EventArgs e)
		{
			this.copySelectedWorkingSets();
			this.refresh();
		}
		private void copySelectedWorkingSets()
		{
			foreach (ListViewItem workingSetitem in this.WorkingSetsList.CheckedItems)
			{
				WorkingSet workingSet = (WorkingSet)workingSetitem.Tag;
				foreach (ListViewItem userItem in this.userList.CheckedItems) 
				{
					User user = (User)userItem.Tag;
					workingSet.copyToUser(user,this.overWriteCheck.Checked);
				}
			}
		}
		private void refresh()
		{
			if (this.allWorkingSets.Count > 1)
			{
				UTF_EA.Model model = this.allWorkingSets[0].model;
				this.allWorkingSets = model.workingSets;
				this.allUsers = model.users;
				this.currentUser = model.currentUser;
				this.initializeWorkingSets();
				this.initializeUserList();
			}
			
		}
			
		
		void WorkingSetsListColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListViewColumnSorter.sortColumn((ListView)sender, e.Column);
		}
		
		void UserListColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListViewColumnSorter.sortColumn((ListView)sender, e.Column);
		}
			
		void WorkingSetNameFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeWorkingSets();
		}
		
		
		void WorkingSetLoginFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeWorkingSets();
		}
		
		void WorkingSetFirstNameFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeWorkingSets();
		}
		
		void WorkingSetLastNameFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeWorkingSets();
		}
		
		void UserLoginFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeUserList();
		}
		
		void UserFirstNameFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeUserList();
		}
		
		void UserLastNameFilterTextChanged(object sender, EventArgs e)
		{
			this.initializeUserList();
		}
		
		
		void WorkingSetsListColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			this.alignWorkingSetFilters();
		}
		
		void UserListColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			this.alignUserFilters();
		}
	}
}
