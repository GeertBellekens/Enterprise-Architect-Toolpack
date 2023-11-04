using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class BOPFDetermination : SAPElement<UMLEA.Activity>
    {
        public static string stereotype => "BOPF_determination";
        const string afterLoadingTagName = "After Loading";
        const string beforeRetrieveTagName = "Before Retrieve";
        const string afterModifyTagName = "After Modify";
        const string afterValidationTagName = "After Validation";
        const string duringCheckAndDetermineTagName = "During Check&Determine";
        const string beforeSaveBeforeConsistencyTagName = "Before Save (Before Consistency Check)";
        const string beforeSaveFinalize = "Before Save (Finalize)";
        const string beforeSaveDrawNumbers = "Before Save (Draw numbers)";
        const string duringSave = "During Save (Before Writing Data)";
        const string afterCommitTagName = "After Commit";
        const string afterFailedSaveTagName = "After Failed Save Attempt";
        const string cleanupTagName = "Cleanup";
        const string categoryTagName = "Category";
        const string determinationClassTagName = "Determination Class";
        
        public BOPFDetermination(string name, BOPFNode ownerNode, string key)
            : base(name, ownerNode.wrappedElement, stereotype, key)
        {
            this.owner = ownerNode;
        }
        public BOPFDetermination(UMLEA.Activity activity) : base(activity) { }

        BOPFNodeOwner _owner;
        public BOPFNodeOwner owner
        {
            get => this._owner;
            set
            {
                //get the existing composition before setting the owner
                var composition = this.compositionToOwner;
                this._owner = value;
                //check if there is a composition to the new owner
                if (composition == null)
                {
                    composition = this.compositionToOwner;
                }
                //ceate new composition if needed
                if (composition == null)
                {
                    composition = new SAPComposition(value, this);
                }
                composition.source = value;
                composition.save();

                //set ownership of wrapped element 
                this.wrappedElement.owner = value.elementWrapper;
                this.save();
            }
        }
        public SAPComposition compositionToOwner
        {
            get => SAPComposition.getExisitingComposition(this.owner, this);
        }
        public SAPClass determinationClass
        {
            get => new SAPClass(this.getLinkProperty<UMLEA.Class>(determinationClassTagName));
            set => this.setLinkProperty(determinationClassTagName, value.wrappedElement);
        }
        public string category
        {
            get => this.getStringProperty(categoryTagName);
            set => this.setStringProperty(categoryTagName, value);
        }
        public bool evaluateAfterLoading
        {
            get => this.getBoolProperty(afterLoadingTagName);
            set => this.setBoolProperty(afterLoadingTagName, value);
        }
        public bool evaluateBeforeRetrieve
        {
            get => this.getBoolProperty(beforeRetrieveTagName);
            set => this.setBoolProperty(beforeRetrieveTagName, value);
        }
        public bool evaluateAfterModify
        {
            get => this.getBoolProperty(afterModifyTagName);
            set => this.setBoolProperty(afterModifyTagName, value);
        }
        public bool evaluateAfterValidation
        {
            get => this.getBoolProperty(afterValidationTagName);
            set => this.setBoolProperty(afterValidationTagName, value);
        }
        public bool evaluateDuringCheckAndDetermine
        {
            get => this.getBoolProperty(duringCheckAndDetermineTagName);
            set => this.setBoolProperty(duringCheckAndDetermineTagName, value);
        }
        public bool evaluateBeforeSaveBeforeConsistency
        {
            get => this.getBoolProperty(beforeSaveBeforeConsistencyTagName);
            set => this.setBoolProperty(beforeSaveBeforeConsistencyTagName, value);
        }
        public bool evaluateBeforeSaveFinalize
        {
            get => this.getBoolProperty(beforeSaveFinalize);
            set => this.setBoolProperty(beforeSaveFinalize, value);
        }
        public bool evaluateBeforeSaveDrawNumbers
        {
            get => this.getBoolProperty(beforeSaveDrawNumbers);
            set => this.setBoolProperty(beforeSaveDrawNumbers, value);
        }
        public bool evaluateDuringSave
        {
            get => this.getBoolProperty(duringSave);
            set => this.setBoolProperty(duringSave, value);
        }
        public bool evaluateAfterCommit
        {
            get => this.getBoolProperty(afterCommitTagName);
            set => this.setBoolProperty(afterCommitTagName, value);
        }
        public bool evaluateAfterFailedSave
        {
            get => this.getBoolProperty(afterFailedSaveTagName);
            set => this.setBoolProperty(afterFailedSaveTagName, value);
        }
        public bool evaluateCleanup
        {
            get => this.getBoolProperty(cleanupTagName);
            set => this.setBoolProperty(cleanupTagName, value);
        }

    }
}
