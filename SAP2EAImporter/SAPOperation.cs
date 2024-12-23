using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPOperation
    {
        internal UMLEA.Operation wrappedOperation { get; private set; }
        public SAPOperation(ISAPElement owner, string name, int pos)
        {
            //check if operation already exists
            this.wrappedOperation = owner.elementWrapper.ownedOperations
                .OfType<UMLEA.Operation>()
                .Where(x => x.name == name)
                .FirstOrDefault();
            //create new operation
            if (this.wrappedOperation == null)
            {
                this.wrappedOperation = owner.elementWrapper.addOwnedElement<UMLEA.Operation>((name));
                this.save();
            }
            //set the position
            if (this.wrappedOperation.position != pos)
            {
                this.wrappedOperation.position = pos;
                this.save();
            }
        }
        public string name
        {
            get => this.wrappedOperation.name;
            set => this.wrappedOperation.name = value;
        }
        public void save()
        {
            this.wrappedOperation.save();
        }

        internal void addOrUpdateParameter(string parameterName, string parameterType, string parameterDirection)
        {
            //check if parameter already exists
            var parameter = this.wrappedOperation.ownedParameters
                .OfType<UMLEA.Parameter>()
                .Where(x => x.name == parameterName)
                .FirstOrDefault();
            if (parameter == null)
            {
                //create new parameter
                parameter = this.wrappedOperation.addOwnedParameter(parameterName);
                parameter.save();
            }
            var dirty = false;
            //update parameter properties
            if (parameter.type.name != parameterType)
            {
                parameter.type = this.wrappedOperation.EAModel.factory.createPrimitiveType(parameterType);
                dirty = true;
            }
            UML.Classes.Kernel.ParameterDirectionKind newParameterDirection;
            if ("return".Equals(parameterDirection, StringComparison.InvariantCultureIgnoreCase))
            {
                newParameterDirection = UML.Classes.Kernel.ParameterDirectionKind._return;
            }
            else if ("out".Equals(parameterDirection, StringComparison.InvariantCultureIgnoreCase))
            {
                newParameterDirection = UML.Classes.Kernel.ParameterDirectionKind._out;
            }
            else if ("inout".Equals(parameterDirection, StringComparison.InvariantCultureIgnoreCase))
            {
                newParameterDirection = UML.Classes.Kernel.ParameterDirectionKind._inout;
            }
            else
            {
                //default: in
                newParameterDirection = UML.Classes.Kernel.ParameterDirectionKind._in;
            }
            if (parameter.direction != newParameterDirection)

            {
                parameter.direction = newParameterDirection;
                dirty = true;
            }
            //save if needed
            if (dirty)
            {
                parameter.save();
            }
        }
    }
}
