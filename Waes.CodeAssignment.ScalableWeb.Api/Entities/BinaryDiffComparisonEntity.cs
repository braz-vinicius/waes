using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waes.CodeAssignment.ScalableWeb.Api.Entities
{
    /// <summary>
    /// Entity representing the binary diff comparison
    /// </summary>
    public class BinaryDiffComparisonEntity
    {
        /// <summary>
        /// Entity key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// An array of bytes of the left side of the comparison operation
        /// </summary>
        public byte[] LeftMember { get; set; }

        /// <summary>
        /// An array of bytes of the right side of the comparison operation
        /// </summary>
        public byte[] RightMember { get; set; }

    }
}
