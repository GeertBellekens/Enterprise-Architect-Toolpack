/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 22/01/2012
 * Time: 13:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EAWorksetSharing
{
	partial class WorkingSetSharingWindow
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingSetSharingWindow));
			this.allWorkingSetsButton = new System.Windows.Forms.Button();
			this.nonWorkingSetsButton = new System.Windows.Forms.Button();
			this.MyWorkingSetsButton = new System.Windows.Forms.Button();
			this.meUserButton = new System.Windows.Forms.Button();
			this.nonUsersButton = new System.Windows.Forms.Button();
			this.allUsersButton = new System.Windows.Forms.Button();
			this.WorkingSetsList = new System.Windows.Forms.ListView();
			this.workingSetHeader = new System.Windows.Forms.ColumnHeader();
			this.loginHeader = new System.Windows.Forms.ColumnHeader();
			this.firstNameHeader = new System.Windows.Forms.ColumnHeader();
			this.lastNameHeader = new System.Windows.Forms.ColumnHeader();
			this.iconsImageList = new System.Windows.Forms.ImageList(this.components);
			this.userList = new System.Windows.Forms.ListView();
			this.userLoginHeader = new System.Windows.Forms.ColumnHeader();
			this.userFirstNameHeader = new System.Windows.Forms.ColumnHeader();
			this.userLastNameHeader = new System.Windows.Forms.ColumnHeader();
			this.cancelButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.overWriteCheck = new System.Windows.Forms.CheckBox();
			this.WorkingSetNameFilter = new System.Windows.Forms.TextBox();
			this.workingSetLoginFilter = new System.Windows.Forms.TextBox();
			this.workingSetFirstNameFilter = new System.Windows.Forms.TextBox();
			this.workingSetLastNameFilter = new System.Windows.Forms.TextBox();
			this.userLoginFilter = new System.Windows.Forms.TextBox();
			this.userFirstNameFilter = new System.Windows.Forms.TextBox();
			this.userLastNameFilter = new System.Windows.Forms.TextBox();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// allWorkingSetsButton
			// 
			this.allWorkingSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.allWorkingSetsButton.Location = new System.Drawing.Point(8, 414);
			this.allWorkingSetsButton.Name = "allWorkingSetsButton";
			this.allWorkingSetsButton.Size = new System.Drawing.Size(42, 22);
			this.allWorkingSetsButton.TabIndex = 7;
			this.allWorkingSetsButton.Text = "All";
			this.allWorkingSetsButton.UseVisualStyleBackColor = true;
			this.allWorkingSetsButton.Click += new System.EventHandler(this.AllWorkingSetsButtonClick);
			// 
			// nonWorkingSetsButton
			// 
			this.nonWorkingSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nonWorkingSetsButton.Location = new System.Drawing.Point(56, 414);
			this.nonWorkingSetsButton.Name = "nonWorkingSetsButton";
			this.nonWorkingSetsButton.Size = new System.Drawing.Size(45, 22);
			this.nonWorkingSetsButton.TabIndex = 8;
			this.nonWorkingSetsButton.Text = "None";
			this.nonWorkingSetsButton.UseVisualStyleBackColor = true;
			this.nonWorkingSetsButton.Click += new System.EventHandler(this.NonWorkingSetsButtonClick);
			// 
			// MyWorkingSetsButton
			// 
			this.MyWorkingSetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MyWorkingSetsButton.Location = new System.Drawing.Point(107, 414);
			this.MyWorkingSetsButton.Name = "MyWorkingSetsButton";
			this.MyWorkingSetsButton.Size = new System.Drawing.Size(106, 22);
			this.MyWorkingSetsButton.TabIndex = 9;
			this.MyWorkingSetsButton.Text = "My Working Sets";
			this.MyWorkingSetsButton.UseVisualStyleBackColor = true;
			this.MyWorkingSetsButton.Click += new System.EventHandler(this.MyWorkingSetsButtonClick);
			// 
			// meUserButton
			// 
			this.meUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.meUserButton.Location = new System.Drawing.Point(102, 414);
			this.meUserButton.Name = "meUserButton";
			this.meUserButton.Size = new System.Drawing.Size(39, 22);
			this.meUserButton.TabIndex = 12;
			this.meUserButton.Text = "Me";
			this.meUserButton.UseVisualStyleBackColor = true;
			this.meUserButton.Click += new System.EventHandler(this.MeUserButtonClick);
			// 
			// nonUsersButton
			// 
			this.nonUsersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nonUsersButton.Location = new System.Drawing.Point(51, 414);
			this.nonUsersButton.Name = "nonUsersButton";
			this.nonUsersButton.Size = new System.Drawing.Size(45, 22);
			this.nonUsersButton.TabIndex = 11;
			this.nonUsersButton.Text = "None";
			this.nonUsersButton.UseVisualStyleBackColor = true;
			this.nonUsersButton.Click += new System.EventHandler(this.NonUsersButtonClick);
			// 
			// allUsersButton
			// 
			this.allUsersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.allUsersButton.Location = new System.Drawing.Point(3, 414);
			this.allUsersButton.Name = "allUsersButton";
			this.allUsersButton.Size = new System.Drawing.Size(42, 22);
			this.allUsersButton.TabIndex = 10;
			this.allUsersButton.Text = "All";
			this.allUsersButton.UseVisualStyleBackColor = true;
			this.allUsersButton.Click += new System.EventHandler(this.AllUsersButtonClick);
			// 
			// WorkingSetsList
			// 
			this.WorkingSetsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.WorkingSetsList.CheckBoxes = true;
			this.WorkingSetsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.workingSetHeader,
									this.loginHeader,
									this.firstNameHeader,
									this.lastNameHeader});
			this.WorkingSetsList.Location = new System.Drawing.Point(8, 39);
			this.WorkingSetsList.Name = "WorkingSetsList";
			this.WorkingSetsList.Size = new System.Drawing.Size(405, 369);
			this.WorkingSetsList.SmallImageList = this.iconsImageList;
			this.WorkingSetsList.TabIndex = 16;
			this.WorkingSetsList.UseCompatibleStateImageBehavior = false;
			this.WorkingSetsList.View = System.Windows.Forms.View.Details;
			this.WorkingSetsList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.WorkingSetsListColumnClick);
			this.WorkingSetsList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.WorkingSetsListColumnWidthChanging);
			// 
			// workingSetHeader
			// 
			this.workingSetHeader.Text = "Working Set";
			this.workingSetHeader.Width = 145;
			// 
			// loginHeader
			// 
			this.loginHeader.Text = "Login";
			this.loginHeader.Width = 68;
			// 
			// firstNameHeader
			// 
			this.firstNameHeader.Text = "First Name";
			this.firstNameHeader.Width = 80;
			// 
			// lastNameHeader
			// 
			this.lastNameHeader.Text = "Last Name";
			this.lastNameHeader.Width = 107;
			// 
			// iconsImageList
			// 
			this.iconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsImageList.ImageStream")));
			this.iconsImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconsImageList.Images.SetKeyName(0, "WorkingSetIcon.png");
			this.iconsImageList.Images.SetKeyName(1, "UserIcon.png");
			// 
			// userList
			// 
			this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.userList.CheckBoxes = true;
			this.userList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.userLoginHeader,
									this.userFirstNameHeader,
									this.userLastNameHeader});
			this.userList.Location = new System.Drawing.Point(3, 39);
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(351, 369);
			this.userList.SmallImageList = this.iconsImageList;
			this.userList.TabIndex = 17;
			this.userList.UseCompatibleStateImageBehavior = false;
			this.userList.View = System.Windows.Forms.View.Details;
			this.userList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.UserListColumnClick);
			this.userList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.UserListColumnWidthChanging);
			// 
			// userLoginHeader
			// 
			this.userLoginHeader.Text = "Login";
			this.userLoginHeader.Width = 85;
			// 
			// userFirstNameHeader
			// 
			this.userFirstNameHeader.Text = "First Name";
			this.userFirstNameHeader.Width = 100;
			// 
			// userLastNameHeader
			// 
			this.userLastNameHeader.Text = "Last Name";
			this.userLastNameHeader.Width = 183;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(699, 444);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 15;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// copyButton
			// 
			this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.copyButton.Location = new System.Drawing.Point(618, 444);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 23);
			this.copyButton.TabIndex = 14;
			this.copyButton.Text = "Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.CopyButtonClick);
			// 
			// overWriteCheck
			// 
			this.overWriteCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.overWriteCheck.Location = new System.Drawing.Point(529, 444);
			this.overWriteCheck.Name = "overWriteCheck";
			this.overWriteCheck.Size = new System.Drawing.Size(83, 24);
			this.overWriteCheck.TabIndex = 13;
			this.overWriteCheck.Text = "Overwrite";
			this.overWriteCheck.UseVisualStyleBackColor = true;
			// 
			// WorkingSetNameFilter
			// 
			this.WorkingSetNameFilter.Location = new System.Drawing.Point(8, 13);
			this.WorkingSetNameFilter.Name = "WorkingSetNameFilter";
			this.WorkingSetNameFilter.Size = new System.Drawing.Size(139, 20);
			this.WorkingSetNameFilter.TabIndex = 0;
			this.WorkingSetNameFilter.TextChanged += new System.EventHandler(this.WorkingSetNameFilterTextChanged);
			// 
			// workingSetLoginFilter
			// 
			this.workingSetLoginFilter.Location = new System.Drawing.Point(155, 12);
			this.workingSetLoginFilter.Name = "workingSetLoginFilter";
			this.workingSetLoginFilter.Size = new System.Drawing.Size(51, 20);
			this.workingSetLoginFilter.TabIndex = 1;
			this.workingSetLoginFilter.TextChanged += new System.EventHandler(this.WorkingSetLoginFilterTextChanged);
			// 
			// workingSetFirstNameFilter
			// 
			this.workingSetFirstNameFilter.Location = new System.Drawing.Point(212, 12);
			this.workingSetFirstNameFilter.Name = "workingSetFirstNameFilter";
			this.workingSetFirstNameFilter.Size = new System.Drawing.Size(78, 20);
			this.workingSetFirstNameFilter.TabIndex = 2;
			this.workingSetFirstNameFilter.TextChanged += new System.EventHandler(this.WorkingSetFirstNameFilterTextChanged);
			// 
			// workingSetLastNameFilter
			// 
			this.workingSetLastNameFilter.Location = new System.Drawing.Point(296, 12);
			this.workingSetLastNameFilter.Name = "workingSetLastNameFilter";
			this.workingSetLastNameFilter.Size = new System.Drawing.Size(108, 20);
			this.workingSetLastNameFilter.TabIndex = 3;
			this.workingSetLastNameFilter.TextChanged += new System.EventHandler(this.WorkingSetLastNameFilterTextChanged);
			// 
			// userLoginFilter
			// 
			this.userLoginFilter.Location = new System.Drawing.Point(1, 13);
			this.userLoginFilter.Name = "userLoginFilter";
			this.userLoginFilter.Size = new System.Drawing.Size(63, 20);
			this.userLoginFilter.TabIndex = 4;
			this.userLoginFilter.TextChanged += new System.EventHandler(this.UserLoginFilterTextChanged);
			// 
			// userFirstNameFilter
			// 
			this.userFirstNameFilter.Location = new System.Drawing.Point(70, 13);
			this.userFirstNameFilter.Name = "userFirstNameFilter";
			this.userFirstNameFilter.Size = new System.Drawing.Size(89, 20);
			this.userFirstNameFilter.TabIndex = 5;
			this.userFirstNameFilter.TextChanged += new System.EventHandler(this.UserFirstNameFilterTextChanged);
			// 
			// userLastNameFilter
			// 
			this.userLastNameFilter.Location = new System.Drawing.Point(165, 12);
			this.userLastNameFilter.Name = "userLastNameFilter";
			this.userLastNameFilter.Size = new System.Drawing.Size(183, 20);
			this.userLastNameFilter.TabIndex = 6;
			this.userLastNameFilter.TextChanged += new System.EventHandler(this.UserLastNameFilterTextChanged);
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(2, -1);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.WorkingSetsList);
			this.splitContainer.Panel1.Controls.Add(this.allWorkingSetsButton);
			this.splitContainer.Panel1.Controls.Add(this.nonWorkingSetsButton);
			this.splitContainer.Panel1.Controls.Add(this.workingSetLastNameFilter);
			this.splitContainer.Panel1.Controls.Add(this.MyWorkingSetsButton);
			this.splitContainer.Panel1.Controls.Add(this.workingSetFirstNameFilter);
			this.splitContainer.Panel1.Controls.Add(this.WorkingSetNameFilter);
			this.splitContainer.Panel1.Controls.Add(this.workingSetLoginFilter);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.userLastNameFilter);
			this.splitContainer.Panel2.Controls.Add(this.userLoginFilter);
			this.splitContainer.Panel2.Controls.Add(this.userFirstNameFilter);
			this.splitContainer.Panel2.Controls.Add(this.userList);
			this.splitContainer.Panel2.Controls.Add(this.allUsersButton);
			this.splitContainer.Panel2.Controls.Add(this.meUserButton);
			this.splitContainer.Panel2.Controls.Add(this.nonUsersButton);
			this.splitContainer.Size = new System.Drawing.Size(780, 445);
			this.splitContainer.SplitterDistance = 416;
			this.splitContainer.TabIndex = 18;
			// 
			// WorkingSetSharingWindow
			// 
			this.AcceptButton = this.copyButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(782, 476);
			this.Controls.Add(this.overWriteCheck);
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.splitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(798, 514);
			this.Name = "WorkingSetSharingWindow";
			this.Text = "Copy Working Sets";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ImageList iconsImageList;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TextBox userLastNameFilter;
		private System.Windows.Forms.TextBox userFirstNameFilter;
		private System.Windows.Forms.TextBox userLoginFilter;
		private System.Windows.Forms.TextBox workingSetLastNameFilter;
		private System.Windows.Forms.TextBox workingSetFirstNameFilter;
		private System.Windows.Forms.TextBox workingSetLoginFilter;
		private System.Windows.Forms.TextBox WorkingSetNameFilter;
		private System.Windows.Forms.CheckBox overWriteCheck;
		private System.Windows.Forms.Button copyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ListView userList;
		private System.Windows.Forms.ColumnHeader userLastNameHeader;
		private System.Windows.Forms.ColumnHeader userFirstNameHeader;
		private System.Windows.Forms.ColumnHeader userLoginHeader;
		private System.Windows.Forms.ColumnHeader lastNameHeader;
		private System.Windows.Forms.ColumnHeader firstNameHeader;
		private System.Windows.Forms.ColumnHeader loginHeader;
		private System.Windows.Forms.ColumnHeader workingSetHeader;
		private System.Windows.Forms.ListView WorkingSetsList;
		private System.Windows.Forms.Button allUsersButton;
		private System.Windows.Forms.Button nonUsersButton;
		private System.Windows.Forms.Button meUserButton;
		private System.Windows.Forms.Button MyWorkingSetsButton;
		private System.Windows.Forms.Button nonWorkingSetsButton;
		private System.Windows.Forms.Button allWorkingSetsButton;
	}
}
