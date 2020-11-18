using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;


namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of NavigatorSettings.
	/// </summary>
	public class NavigatorSettings:EAAddinFramework.Utilities.AddinSettings
	{
		protected override string addinName => "EANavigator";
		protected override string configSubPath
		{
			get 
			{
				return @"\Geert Bellekens\EANavigator\";
			}
		}
		protected override string defaultConfigAssemblyFilePath 
		{
			get 
			{
				return System.Reflection.Assembly.GetExecutingAssembly().Location;
			}
		}
		public bool isOptionEnabled(UML.Extended.UMLItem parentElement,string option)
		{
			//default
			return true;
		}
		/// <summary>
		/// returns true when the selecting an element in the project browser is the default action on doubleclick.
		/// </summary>
		/// <returns>true when the selecting an element in the project browser is the default action on doubleclick.</returns>
		public bool projectBrowserDefaultAction
		{
			get
			{
				return (this.getValue("defaultAction") == "ProjectBrowser");
			}
			set
			{
				if (value)
				{
					this.setValue("defaultAction", "ProjectBrowser");
				}
				else
				{
					this.setValue("defaultAction", "Properties");
				}
			}
		}
		public bool toolbarVisible
		{
			get
			{
				return getBooleanValue("toolbarVisible");

			}
			set
			{
				setBooleanValue("toolbarVisible",value);
			}
		}
		public bool contextmenuVisible
		{
			get
			{
				return getBooleanValue("contextMenu");
			}
			set
			{
				setBooleanValue("contextMenu",value);
			}
		}
		public bool trackSelectedElement
		{
			get
			{
				return getBooleanValue("trackSelectedElement");
			}
			set
			{
				setBooleanValue("trackSelectedElement",value);
			}
		}
		public bool quickSearchElements
		{
			get
			{
				return getBooleanValue("quickSearchElements");
			}
			set
			{
				setBooleanValue("quickSearchElements",value);
			}
		}
		public bool quickSearchOperations
		{
			get
			{
				return getBooleanValue("quickSearchOperations");
			}
			set
			{
				setBooleanValue("quickSearchOperations",value);
			}
		}
		public bool quickSearchAttributes
		{
			get
			{
				return getBooleanValue("quickSearchAttributes");
			}
			set
			{
				setBooleanValue("quickSearchAttributes",value);
			}
		}
		public bool quickSearchDiagrams
		{
			get
			{
				return getBooleanValue("quickSearchDiagrams");
			}
			set
			{
				setBooleanValue("quickSearchDiagrams",value);
			}
		}
		public bool quickSearchAddToDiagram
		{
			get
			{
				return getBooleanValue("quickSearchAddToDiagram");
			}
			set
			{
				setBooleanValue("quickSearchAddToDiagram", value);
			}
		}
		public bool quickSearchSelectProjectBrowser
		{
			get
			{
				return getBooleanValue("quickSearchSelectProjectBrowser");
			}
			set
			{
				setBooleanValue("quickSearchSelectProjectBrowser",value);
			}
		}
	}
}
