using System;
using System.Configuration;
using System.Reflection;

namespace GlossaryManager {

  public class GlossaryManagerSettings
    : EAAddinFramework.Utilities.AddinSettings
  {
    protected override string configSubPath	{
      get { return @"\Bellekens\GlossaryManager\"; }
    }

    protected override string defaultConfigFilePath {
      get { return Assembly.GetExecutingAssembly().Location; }
    }

    public string outputName {
      get { return this.getValue("outputName");        }
      set {        this.setValue("outputName", value); }
    }

  }
}
