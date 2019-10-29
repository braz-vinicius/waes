using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waes.CodeAssignment.ScalableWeb.Api.Models
{
    /// <summary>
    /// Request that wraps a base64 string encoded binary to be compared
    /// </summary>
    public class DiffRequest
    {
        /// <summary>
        /// Base64 string containing the binary data
        /// </summary>
        public string Data { get; set; }
    }
}
