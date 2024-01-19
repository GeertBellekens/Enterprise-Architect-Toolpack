﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPElementFactory
    {
        public static ISAPElement CreateSAPElement(UMLEA.ElementWrapper elementWrapper)
        {
            var stereotype = elementWrapper.stereotypes.FirstOrDefault()?.name;

            if (stereotype == SAPAuthorization.stereotype)
            {
                return new SAPAuthorization(elementWrapper as UMLEA.InstanceSpecification);
            }
            if (stereotype == SAPAuthorizationObject.stereotype)
            {
                return new SAPAuthorizationObject(elementWrapper as UMLEA.Class);
            }
            if (stereotype == BOPFBusinessObject.stereotype)
            {
                return new BOPFBusinessObject(elementWrapper as UMLEA.Component);
            }
            if (stereotype == BOPFDetermination.stereotype)
            {
                return new BOPFDetermination(elementWrapper as UMLEA.Activity);
            }
            if (stereotype == BOPFNode.stereotype)
            {
                return new BOPFNode(elementWrapper as UMLEA.Class);
            }
            if (stereotype == BOPFAction.stereotype)
            {
                return new BOPFAction(elementWrapper as UMLEA.Activity);
            }
            if (stereotype == BOPFValidation.stereotype)
            {
                return new BOPFValidation(elementWrapper as UMLEA.Activity);
            }
            if (stereotype == BOPFQuery.stereotype)
            {
                return new BOPFQuery(elementWrapper as UMLEA.Activity);
            }
            if (stereotype == CompositeRole.stereotype)
            {
                return new CompositeRole(elementWrapper as UMLEA.Class);
            }
            if (stereotype == FunctionModule.stereotype)
            {
                return new FunctionModule(elementWrapper as UMLEA.Class);
            }
            if (stereotype == RolePackage.stereotype)
            {
                return new RolePackage(elementWrapper as UMLEA.Class);
            }
            if (stereotype == RolePackage.stereotype)
            {
                return new RolePackage(elementWrapper as UMLEA.Class);
            }
            if (string.IsNullOrEmpty(stereotype) 
                && elementWrapper is UMLEA.Class)
            {
                return new SAPClass(elementWrapper as UMLEA.Class);
            }
            if (string.IsNullOrEmpty(stereotype)
                && elementWrapper is UMLEA.DataType)
            {
                return new SAPDatatype(elementWrapper as UMLEA.DataType);
            }
            if (stereotype == SAPTable.stereotype)
            {
                return new SAPTable(elementWrapper as UMLEA.Class);
            }
            if (stereotype == SingleRole.stereotype)
            {
                return new SingleRole(elementWrapper as UMLEA.Class);
            }
            
            //default case
            return null;
        }
    }
}
