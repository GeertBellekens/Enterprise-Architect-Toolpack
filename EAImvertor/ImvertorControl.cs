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
using System.Linq;

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
		public EAImvertorJob selectedJob
		{
			get
			{
				try
				{
					var selectedItems = imvertorJobGrid.SelectedItems;
					if (selectedItems.Count > 0)
					{
						return selectedItems[0].Tag as EAImvertorJob;
					}
					else
					{
						return null;
					}
				}
				catch(Exception)
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
			this.refreshToolTip.SetToolTip(this.refreshButton, "Refresh Status");
			this.resizeGridColumns();
			this.enableDisable();
		}
		public void addJob(EAImvertorJob job)
		{
			this.jobs.Insert(0,job);
			var row = new ListViewItem(job.sourcePackage.name);
			row.SubItems.Add(job.status);
			row.Tag = job;
			this.imvertorJobGrid.Items.Insert(0,row);
			row.Selected = true;
		}

		
		public void refreshJobInfo(EAImvertorJob imvertorJob)
		{
			try
			{
				foreach (ListViewItem row in imvertorJobGrid.Items) 
				{
					var currentJob = (EAImvertorJob) row.Tag;
					if (imvertorJob == null || currentJob.Equals(imvertorJob) )
					{
						string statusString = currentJob.status;
						
						if (!(currentJob.status.StartsWith("Finished") || currentJob.status.StartsWith("Error")))
						{
						   	if (currentJob.timedOut)
						   	{
						   		statusString += " (Timed Out)";
						   	}
						   	else if (currentJob.status.StartsWith("In Progress"))
						   	{
						   		if (currentJob.message.Length > 0) statusString += string.Format(" ({0})",currentJob.message);
						   	}
						    else if (currentJob.tries > 0)
							{
								statusString += new string('.',currentJob.tries);
							}
						 }
						else if (currentJob.status.StartsWith("Finished"))
						{
							if (currentJob.message.Length > 0) statusString += string.Format(" ({0})",currentJob.message);
						}
						else if (currentJob.status.StartsWith("Error"))
						{
							statusString += @" (See "+EAAddinFramework.Utilities.Logger.logFileName+" for more info)";
						}
						row.SubItems[1].Text =statusString;
					}

				}
				setSelectedJobDetails();
				this.enableDisable();
			}
			catch(Exception)
			{
				//do nothing. TODO: figure out a thread safe way to refresh control
			}
		}
		private void resizeGridColumns()
		{
			//set the last column to fill
			this.imvertorJobGrid.Columns[imvertorJobGrid.Columns.Count - 1].Width = -2;
		}
		private void enableDisable()
		{
			if (this.selectedJob != null
			    && this.selectedJob.status == "Finished")
			{
				this.resultsButton.Enabled = (!string.IsNullOrEmpty(this.selectedJob.downloadPath));
				this.viewWarningsButton.Enabled = (this.selectedJob.errors.Count > 0 || this.selectedJob.warnings.Count > 0);
				this.refreshButton.Enabled = false;
				this.reportButton.Enabled = true;
			}
			else
			{
				this.resultsButton.Enabled = false;
				this.viewWarningsButton.Enabled = false;
				this.refreshButton.Enabled = (this.selectedJob != null && this.selectedJob.timedOut);
				this.reportButton.Enabled = false;
			}
			//we can always retry as long as there's a job selected
			this.retryButton.Enabled = (this.selectedJob != null);
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

		}
		public event EventHandler viewWarningsButtonClick;
		void ViewWarningsButtonClick(object sender, EventArgs e)
		{
			if (this.viewWarningsButtonClick != null)
			{
				viewWarningsButtonClick(sender, e);
			}
		}
		public event EventHandler reportButtonClick;
		void ReportButtonClick(object sender, EventArgs e)
		{
			if (this.reportButtonClick != null)
			{
				reportButtonClick(sender, e);
			}
		}
		void ImvertorJobGridResize(object sender, EventArgs e)
		{
			this.resizeGridColumns();
		}
		private void setSelectedJobDetails()
		{
			if (this.selectedJob != null)
			{
				this.jobIDTextBox.Text = selectedJob.jobID;
				this.propertiesTextBox.Text = selectedJob.settings.Properties;
				this.processTextBox.Text = selectedJob.settings.ProcessName;
				this.historyFileTextBox.Text = selectedJob.settings.HistoryFilePath;
			}
			else
			{
				//clear fields
				this.jobIDTextBox.Text = string.Empty;
				this.propertiesTextBox.Text = string.Empty;
				this.processTextBox.Text = string.Empty;
				this.historyFileTextBox.Text = string.Empty;
			}
		}
		void ImvertorJobGridSelectedIndexChanged(object sender, EventArgs e)
		{
			setSelectedJobDetails();
			this.enableDisable();
		}
		
		void RefreshButtonClick(object sender, EventArgs e)
		{
			if (this.selectedJob != null)
			{
				this.selectedJob.refreshStatus();
				this.refreshJobInfo(this.selectedJob);
			}
		}
		public event EventHandler publishButtonClick = delegate{};
		void PublishButtonClick(object sender, EventArgs e)
		{
			publishButtonClick(sender, e);
		}
		public void setPublishEnabled(bool enabled)
		{
			this.publishButton.Enabled = enabled;
		}
		public void setSelectedPackageName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
			    this.selectedPackageLabel.Text = string.Empty;
			}
			else
			{
				this.selectedPackageLabel.Text = "Package: " +name;
			}
		}


		
	}
}
