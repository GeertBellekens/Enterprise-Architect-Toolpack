using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAddinFramework.Utilities;

namespace EAScriptAddin
{
    public class EAScriptAddinSettings : AddinSettings
    {
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
		/// use tagged values as a way to map elements.
		/// If false we use relations with "link to element feature"
		/// </summary>
		public bool developerMode
		{
			get => this.getBooleanValue("developerMode");
			set => this.setBooleanValue("developerMode", value);
		}
	}
}
