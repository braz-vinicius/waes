using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waes.CodeAssignment.ScalableWeb.Api.Models
{
    /// <summary>
    /// Represents an offset result from the comparison 
    /// </summary>
    public class DiffOffset
    {
        /// <summary>
        /// The index position where the difference started
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// The length of the difference found
        /// </summary>
        public int Length { get; set; }

    }
}
