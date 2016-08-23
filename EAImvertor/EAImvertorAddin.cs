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
		//constructor
        public EAImvertorAddin():base()
		{
			this.menuHeader = menuName;
			this.settings = new EAImvertorSettings();
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
						this.imvertorControl.viewWarningsButtonClick += this.viewWarningsButtonClick;
						this.imvertorControl.reportButtonClick += this.reportButtonClick;
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
		/// <summary>
		/// initialize the add-in class
		/// </summary>
		/// <param name="Repository"></param>
		private void initialize(EA.Repository Repository)
		{
			//initialize the model
			this.model = new UTF_EA.Model(Repository);
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
						publish();
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
				if (!selectedPackage.stereotypes.Any
				    					(x => this.settings.imvertorStereotypes.Any
				     						(y => y.Equals(x.name,StringComparison.InvariantCultureIgnoreCase))))
				{
					IsEnabled = false;
				}
			}
			else
			{
				IsEnabled = true;
			}
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
		private void publish()
		{
            //get selected package
            var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
            if (selectedPackage != null) publish(selectedPackage);
		}
		private void publish(UML.Classes.Kernel.Package selectedPackage)
		{
			EAImvertorJobSettings jobSettings = new EAImvertorJobSettings(this.settings); 
			if (new ImvertorStartJobForm(jobSettings).ShowDialog(this.model.mainEAWindow) == DialogResult.OK)
			{
				//somebody called the imvertor, we can show the control
				this._imvertorCalled = true;
				//create new backgroundWorker
				var imvertorJobBackgroundWorker = new BackgroundWorker();
				//imvertorJobBackgroundWorker.WorkerSupportsCancellation = true; //TODO: implement cancellation
				imvertorJobBackgroundWorker.WorkerReportsProgress = true;
				imvertorJobBackgroundWorker.DoWork += imvertorBackground_DoWork;
				imvertorJobBackgroundWorker.ProgressChanged += imvertorBackground_ProgressChanged;
				imvertorJobBackgroundWorker.RunWorkerCompleted += imvertorBackgroundRunWorkerCompleted;
	            
	            var imvertorJob = new EAImvertorJob(selectedPackage, jobSettings);
	            
	            //update gui
	            this.imvertorControl.addJob(imvertorJob);
	            //show the control
	            this.model.showWindow(windowName);
	            
	            //start job in the background
	            imvertorJobBackgroundWorker.RunWorkerAsync(imvertorJob);
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
			this.imvertorControl.refreshJobInfo(e.UserState as EAImvertorJob);
		}

		private void imvertorBackgroundRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._imvertorControl.refreshJobInfo(e.Result as EAImvertorJob);
			Logger.log("imvertorJob finished");
		}
	}
}