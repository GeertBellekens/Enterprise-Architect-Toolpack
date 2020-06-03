using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAValidator
{
    public abstract class CheckItem
    {
        public string name { get; }
        public abstract bool? selected { get; set; }
        public virtual CheckStatus Status { get; protected set; } = CheckStatus.NotValidated;
        /// <summary>
        ///Total number of elements found  
        /// </summary>
        public virtual int? NumberOfElementsFound { get; set; }
        /// <summary>
        /// Number of Validation Results found (using Query)
        /// </summary>
        public virtual int? NumberOfValidationResults { get; set; }                               

        public string StatusName
        {
            get
            {
                switch (this.Status)
                {
                    case CheckStatus.Failed:
                        return "Failed";
                    case CheckStatus.NotValidated:
                        return "Not Validated";
                    case CheckStatus.Passed:
                        return "Passed";
                    case CheckStatus.Error:
                        return "ERROR";
                    default:
                        return "Unknown";//should not happen
                }
            }
        }
    }
}
