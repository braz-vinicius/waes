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
    public class DiffResultModel
    {
        /// <summary>
        /// Initializes a new DiffResultModel instance
        /// </summary>
        public DiffResultModel()
        {
            DiffOffsets = new List<DiffOffsetModel>();
        }

        /// <summary>
        /// Friendly message with the comparison result of the binaries
        /// </summary>
        public string ComparisonResult { get; set; }

        /// <summary>
        /// List of offsets where the differences are located
        /// </summary>
        public List<DiffOffsetModel> DiffOffsets { get; set; }
    }

    /// <summary>
    /// Represents an offset result from the comparison 
    /// </summary>
    public class DiffOffsetModel
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
