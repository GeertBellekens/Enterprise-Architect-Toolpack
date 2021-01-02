using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAddinFramework.Utilities;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAScriptAddin
{
    public class EAScriptAddinSettings : AddinSettings
    {
		public Model model { get; set; }
		public EAScriptAddinSettings(Model model)
		{
			this.model = model;
		}
		protected override string addinName => "EA-Matic";
		protected override string configSubPath
		{
			get
			{
				return @"\Bellekens\EA-Matic\";
			}
		}
		protected override string defaultConfigAssemblyFilePath
		{
			get
			{
				return System.Reflection.Assembly.GetExecutingAssembly().Location;
			}
		}
		/// <summary>
		/// the path to store the scripts
		/// </summary>
		public string scriptPath
		{
			get => this.getValue("scriptPath");
			set => this.setValue("scriptPath",value);
		}
		/// <summary>
		/// use tagged values as a way to map elements.
		/// If false we use relations with "link to element feature"
		/// </summary>
		public bool developerMode
		{

			get
			{
				return this.getListValue("developerMode").Contains(this.model?.projectGUID);
			}
			set
			{
				if (this.model != null)
				{
					var projectGUIDs = this.getListValue("developerMode");
					if (value)
					{
						//make sure the projectGUID is added to the settings
						if (!projectGUIDs.Contains(this.model.projectGUID))
						{
							projectGUIDs.Add(this.model.projectGUID);
						}
					}
					else
					{
						//remove the project guid from the settings if needed
						if (projectGUIDs.Contains(this.model.projectGUID))
						{
							projectGUIDs.Remove(this.model.projectGUID);
						}
					}
					this.setListValue("developerMode", projectGUIDs);
				}
			}
		}
	}
}
