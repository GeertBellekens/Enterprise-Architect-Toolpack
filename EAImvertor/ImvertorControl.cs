/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 2/07/2016
 * Time: 7:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EAImvertor
{
	[Guid("16F72602-8F86-4E26-AFBD-4A0FB873CA09")]
	[ComVisible(true)]
	/// <summary>
	/// Description of ImvertorControl.
	/// </summary>
	public partial class ImvertorControl : UserControl
	{
		//private attributes
		private List<EAImvertorJob> jobs = new List<EAImvertorJob>();
		private EAImvertorJob selectedJob
		{
			get
			{
				if (this.imvertorJobGrid.SelectedItems.Count > 0)
				{
					return this.imvertorJobGrid.SelectedItems[0].Tag as EAImvertorJob;
				}
				else
				{
					return null;
				}
			}
		}
		public ImvertorControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.resizeGridColumns();
			
		}
		public void addJob(EAImvertorJob job)
		{
			this.jobs.Insert(0,job);
			this.refreshJobInfo();
		}
		public void refreshJobInfo()
		{
			this.imvertorJobGrid.Items.Clear();
			foreach (var job in this.jobs) 
			{
				var row = new ListViewItem(job.sourcePackage.name);
				row.SubItems.Add(job.status);
				row.Tag = job;
				this.imvertorJobGrid.Items.Add(row);
			}
		}
		private void resizeGridColumns()
		{
			//set the last column to fill
			this.imvertorJobGrid.Columns[imvertorJobGrid.Columns.Count - 1].Width = -2;
		}
		public void clear()
		{
			//TODO: clear the control
		}	
		public event EventHandler retryButtonClick;
		void RetryButtonClick(object sender, EventArgs e)
		{
			if (this.retryButtonClick != null)
			{
				retryButtonClick(sender, e);
			}
		}
		public event EventHandler resultsButtonClick;
		void ResultsButtonClick(object sender, EventArgs e)
		{
			if (this.resultsButtonClick != null)
			{
				resultsButtonClick(sender, e);
			}
			//TODO: figure out where this should happen
			if (this.selectedJob != null)
				this.selectedJob.downloadResults();
		}
		void ImvertorJobGridResize(object sender, EventArgs e)
		{
			this.resizeGridColumns();
		}
		void ImvertorJobGridSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.selectedJob != null)
			{
				this.jobIDTextBox.Text = selectedJob.jobID;
			}
			else
			{
				//clear fields
				this.jobIDTextBox.Text = string.Empty;
			}
		}
		void ViewWarningsButtonClick(object sender, EventArgs e)
		{
			if (this.selectedJob != null)
				this.selectedJob.viewReport();
		}
		
	}
}
