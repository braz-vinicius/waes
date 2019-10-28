using Waes.CodeAssignment.ScalableWeb.Api.Models;

namespace Waes.CodeAssignment.ScalableWeb.Api.Services
{
    /// <summary>
    /// Represents an interface for a domain service that compares two binaries
    /// </summary>
    public interface IBinaryDiffService
    {
        /// <summary>
        /// Adds an array of bytes into the left side of the comparison operation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        void AddLeftMember(int id, byte[] data);

        /// <summary>
        /// Adds an array of bytes into the right side of the comparison operation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        void AddRightMember(int id, byte[] data);

        /// <summary>
        /// Gets the difference result between two binaries given an operation <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DiffResultModel GetBinaryDiffResult(int id);
    }
}