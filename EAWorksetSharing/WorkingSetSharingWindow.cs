
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
			
			this.initializeWorkingSets();
			this.initializeUserList();
			if (this.currentUser == null)
			{
				this.meUserButton.Enabled = false;
				this.MyWorkingSetsButton.Enabled = false;
			}
			
		}
		
		private void initializeWorkingSets()
		{
			//first clear all
			this.WorkingSetsList.Items.Clear();
			//then add all workingSets
			foreach (WorkingSet workingSet in this.allWorkingSets) 
			{
				ListViewItem item = new ListViewItem(workingSet.name);
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
		private void initializeUserList()
		{
			//first clear all
			this.userList.Items.Clear();
			//then start adding the elements
			foreach (User user in this.allUsers) 
			{
				ListViewItem item = new ListViewItem(user.login);
				item.SubItems.Add(user.firstName);
				item.SubItems.Add(user.lastName);
				//set user as tag
				item.Tag = user;
				//add to list
				this.userList.Items.Add(item);
			}
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
					workingSet.copyToUser(user);
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
			
	}
}
