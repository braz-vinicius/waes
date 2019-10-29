using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.CodeAssignment.ScalableWeb.Api.Services;
using static Waes.CodeAssignment.ScalableWeb.Api.Services.BinaryDiffService;

namespace Waes.CodeAssignment.ScalableWeb.Api.Models
{
    /// <summary>
    /// Represents a binary comparison result response
    /// </summary>
    public class DiffResult
    {
        /// <summary>
        /// Initializes a new DiffResult instance
        /// </summary>
        public DiffResult()
        {
            DiffOffsets = new List<DiffOffset>();
        }

        /// <summary>
        /// Friendly message with the comparison result of the binaries
        /// </summary>
        public string ComparisonResult { get; set; }

        /// <summary>
        /// List of offsets where the differences are located
        /// </summary>
        public List<DiffOffset> DiffOffsets { get; set; }
    }

}
