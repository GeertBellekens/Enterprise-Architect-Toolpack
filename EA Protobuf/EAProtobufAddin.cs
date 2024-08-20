using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAddinFramework;
using EA;

namespace EA_Protobuf
{
    public class EAProtobufAddin : EAAddinBase
    {
        // menu constants
        const string menuName = "-&EA Protobuf";
        const string menuGenerate = "&Generate Protobuf";
        const string menuTransform = "&Transform to Protobuf profile";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About EA Protobuf";
        const string outputName = "EA Protobuf";
        public EAProtobufAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuGenerate,
                                menuTransform,
                                //menuSettings,
                                menuAbout
                              };
        }
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuGenerate:
                    this.generateProtobufSchema();
                    break;
                //case menuTransform:
                //    this.transform();
                //    break;
                //case menuSettings:
                //    //TODO
                //    break;
                //case menuAbout:
                //    new AboutWindow().ShowDialog(this.model.mainEAWindow);
                //    break;
            }
        }

        private void generateProtobufSchema()
        {
            ProtobufSchema.GenerateProtoFile("c:\\temp\\test.proto");
        }
    }
}
