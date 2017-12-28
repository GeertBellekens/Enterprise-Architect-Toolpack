/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 2/07/2016
 * Time: 7:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;
using System.ComponentModel;
using EAAddinFramework.Utilities;

namespace EAImvertor
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAImvertorAddin: EAAddinFramework.EAAddinBase
	{
		 // define menu constants
        const string menuName = "-&EA Imvertor";
        const string menuAbout = "&About";
        const string menuSettings = "&Settings";
        const string menuPublish = "&Publish to Imvertor";
        const string windowName = "Imvertor";
        
        //attributes
        private UTF_EA.Model model = null;
    	private ImvertorControl _imvertorControl;
       	private bool fullyLoaded = false;
       	private EAImvertorSettings settings;
       	private bool _imvertorCalled = false;
       	//indicates if a job can be started or has to be put in the waiting queue
       	private bool canJobStart = true;
       	//the list of jobs waiting to be started
       	private List<EAImvertorJob> waitingjobs = new List<EAImvertorJob>();
       	//the job that is currently blocking the others from starting.
       	//we need this because while a job is exporting to xmi no other jobs should be allowed to start
       	private EAImvertorJob blockingJob;
		//constructor
        public EAImvertorAddin():base()
		{
			this.menuHeader = menuName;

		}
		
        private ImvertorControl imvertorControl
		{
			get
			{
				//we cannot show windows in the lite edition
				if (this.fullyLoaded && ! this.model.isLiteEdition && _imvertorCalled)
				{
					if (this._imvertorControl == null)
					{
						this._imvertorControl = this.model.addWindow(windowName, "EAImvertor.ImvertorControl") as ImvertorControl;
						this._imvertorControl.resultsButtonClick += this.resultsButtonClick;
						this._imvertorControl.retryButtonClick += this.retryButtonClick;
						this._imvertorControl.viewWarningsButtonClick += this.viewWarningsButtonClick;
						this._imvertorControl.reportButtonClick += this.reportButtonClick;
						this._imvertorControl.publishButtonClick += this.publishButtonClick;
					}
				}
				return this._imvertorControl;
			}
		}
        /// <summary>
        /// Initializes the model and schemaFactory with the new Repository object.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		public override void EA_FileOpen(EA.Repository Repository)
		{
			//initialize the model
			this.initialize(Repository);
		}
		public override void EA_FileClose(EA.Repository Repository)
		{
			var tempPath = Path.Combine(Path.GetTempPath(),@"EAImvertor\");
			//delete temp directory
			try
			{
				//make sure all file handles are released before deleting the directory
				GC.Collect();
        		GC.WaitForPendingFinalizers();
        		//Then delete the temp directory
				Directory.Delete(tempPath,true);
			}
			catch(Exception)
			{
				//swallow exception, there might be anothor process using the temp directory
			}
		}
		/// <summary>
		/// initialize the add-in class
		/// </summary>
		/// <param name="Repository"></param>
		private void initialize(EA.Repository Repository)
		{
			//initialize the model
			this.model = new UTF_EA.Model(Repository);
			//set the settings
			this.settings = new EAImvertorSettings(this.model);
			// indicate that we are now fully loaded
	        this.fullyLoaded = true;
		}
		/// <summary>
		/// reacts to the event that the resultsButton is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void resultsButtonClick(object sender, EventArgs e)
		{
			if (this.imvertorControl.selectedJob != null)
				this.imvertorControl.selectedJob.openResults();
		}
		/// <summary>
		/// reacts tot he even that the report button is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void reportButtonClick(object sender, EventArgs e)
		{
			if (this.imvertorControl.selectedJob != null)
				this.imvertorControl.selectedJob.showReport();
		}

		/// <summary>
		/// reacts to the event that the viewWarningsButton is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void viewWarningsButtonClick(object sender, EventArgs e)
		{
			if (this.imvertorControl.selectedJob != null)
				this.imvertorControl.selectedJob.viewWarnings();
		}
		/// <summary>
		/// reacts to the event that the retryButton is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void retryButtonClick(object sender, EventArgs e)
		{
			
			if (this.imvertorControl.selectedJob != null)
			{
				var sourcePackage = this.imvertorControl.selectedJob.sourcePackage;
				if (sourcePackage != null)
				{
					var modelPackage = this.model.getElementByGUID(sourcePackage.uniqueID);
					if (modelPackage != null)
					{
						publish(sourcePackage,this.imvertorControl.selectedJob.settings);
					}
				}
			}
		}

		public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
		{
			List<string> menuOptionsList = new List<string>();
			if (this.fullyLoaded)
			{
				var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
				if (selectedPackage != null)
				{
					menuOptionsList.Add(menuPublish);
				}
			}
			if ( MenuLocation == "MainMenu") 
			{
				menuOptionsList.Add(menuSettings);
				menuOptionsList.Add(menuAbout);
			}
			this.menuOptions = menuOptionsList.ToArray();
			//call base operation
			return base.EA_GetMenuItems(Repository, MenuLocation, MenuName);
		}
		public override void EA_GetMenuState(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
		{
			//only enable publish menu on a limited set of stereotypes
			if (ItemName == menuPublish)
			{
				var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
				IsEnabled = canBePublished(selectedPackage);
			}
			else
			{
				IsEnabled = true;
			}
		}
		/// <summary>
		/// a package can be published if it's stereotype is present in the list of allowed stereotypes in the settings
		/// </summary>
		/// <param name="package">the package to publish</param>
		/// <returns>whether or not a package can be published</returns>
		private bool canBePublished(UML.Classes.Kernel.Package package)
		{
			return ( package != null
					&& (package.stereotypes.Any
				    					(x => this.settings.imvertorStereotypes.Any
				 							(y => y.Equals(x.name,StringComparison.InvariantCultureIgnoreCase)))
			         || package.taggedValues.Any(x => "imvertor".Equals(x.name, StringComparison.InvariantCultureIgnoreCase) 
			                                        && "model".Equals(x.tagValue.ToString(), StringComparison.InvariantCultureIgnoreCase)))
				&& EAImvertorJob.getProjectPackage(package) != null);
		}
		/// <summary>
		/// only needed for the about menu
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch(ItemName) 
			{
		       case menuAbout :
		            new AboutWindow().ShowDialog(this.model.mainEAWindow);
		            break;
		       case menuSettings:
		            new EAImvertorSettingsForm(this.settings).ShowDialog(this.model.mainEAWindow);
		            break;
		       case menuPublish:
		            publish();
		            break;
			}
		}
		public override void EA_OnContextItemChanged(EA.Repository Repository, string GUID, EA.ObjectType ot)
		{
			if (this.model != null)
			{
				var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
				//in case a package is selected that can be publised we show the control;
				if (selectedPackage != null 
				    && canBePublished(selectedPackage))
				{
					_imvertorCalled = true;		
				}
				if (this.imvertorControl != null)
				{
					if (selectedPackage != null)
					{
						this.imvertorControl.setSelectedPackageName(selectedPackage.name);
						this.imvertorControl.setPublishEnabled(canBePublished(selectedPackage));
					}
					else
					{
						this.imvertorControl.setSelectedPackageName(string.Empty);
						this.imvertorControl.setPublishEnabled(false);
					}
				}
			}

		}
		void publishButtonClick(object sender, EventArgs e)
		{
			publish();
		}

		private void publish()
		{
            //get selected package
            var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
            if (canBePublished(selectedPackage)) publish(selectedPackage);
		}
		private void publish(UML.Classes.Kernel.Package selectedPackage)
		{
			EAImvertorJobSettings jobSettings = new EAImvertorJobSettings(this.settings); 
			if (new ImvertorStartJobForm(jobSettings).ShowDialog(this.model.mainEAWindow) == DialogResult.OK)
			{
				publish(selectedPackage, jobSettings);
			}
		}
		private void publish(UML.Classes.Kernel.Package selectedPackage, EAImvertorJobSettings jobSettings)
		{
			//somebody called the imvertor, we can show the control
			this._imvertorCalled = true;
			var imvertorJob = new EAImvertorJob(selectedPackage, jobSettings);
			this.startJob(imvertorJob);

		}
		/// <summary>
		/// start an ImvertorJob if possible. Else the job will be added to the waiting list
		/// </summary>
		/// <param name="imvertorJob">the job to start</param>
		private void startJob(EAImvertorJob imvertorJob)
		{
			if (this.canJobStart)
			{
				//create new backgroundWorker
				var imvertorJobBackgroundWorker = new BackgroundWorker();
				//imvertorJobBackgroundWorker.WorkerSupportsCancellation = true; //TODO: implement cancellation
				imvertorJobBackgroundWorker.WorkerReportsProgress = true;
				imvertorJobBackgroundWorker.DoWork += imvertorBackground_DoWork;
				imvertorJobBackgroundWorker.ProgressChanged += imvertorBackground_ProgressChanged;
				imvertorJobBackgroundWorker.RunWorkerCompleted += imvertorBackgroundRunWorkerCompleted;
	            //update gui
	            this.imvertorControl.addJob(imvertorJob);
	            //show the control
	            this.model.showWindow(windowName);
	            
	            //start job in the background
	            imvertorJobBackgroundWorker.RunWorkerAsync(imvertorJob);
			}
			else
			{
				//job cannot be started, we add it to the waiting jobs
				this.waitingjobs.Add(imvertorJob);
			}
		}

		private void imvertorBackground_DoWork(object sender, DoWorkEventArgs e)
		{
			var imvertorJob = e.Argument as EAImvertorJob;
			if (imvertorJob != null)
				imvertorJob.startJob(sender as BackgroundWorker );
			//pass the job as result
			e.Result = imvertorJob;
		}

		void imvertorBackground_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//if the current job is exporting we have to stop the other jobs from starting
			var currentJob = (EAImvertorJob)e.UserState;
			if (currentJob.status.StartsWith("Exporting"))
			{
				this.blockingJob = currentJob;
				this.canJobStart = false;
			}
			else
			{
				if (this.blockingJob == currentJob)
				{
					this.canJobStart = true;
					this.startNextJob();
				}
			}
			this.imvertorControl.refreshJobInfo(currentJob);
		}
		/// <summary>
		/// start the next job in the waiting line (if any)
		/// </summary>
		private void startNextJob()
		{
			var nextJob = this.waitingjobs.FirstOrDefault();
			if (nextJob != null)
			{
				waitingjobs.Remove(nextJob);
				this.startJob(nextJob);
			}
		}

		private void imvertorBackgroundRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._imvertorControl.refreshJobInfo(e.Result as EAImvertorJob);
		}
	}
}