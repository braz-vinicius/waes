using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.CodeAssignment.ScalableWeb.Api.Models;

namespace Waes.CodeAssignment.ScalableWeb.Api.Infrastructure
{
    /// <summary>
    /// Represents a Binary data comparer 
    /// </summary>
    public class BinaryComparer
    {
        /// <summary>
        /// Initializes a new instance of BinaryComparer
        /// </summary>
        public BinaryComparer()
        {

        }

        /// <summary>
        /// Compares two binaries and returns a Tuple containing it's result type and (if applicable) offsets
        /// </summary>
        /// <param name="left">left array of bytes to compare</param>
        /// <param name="right">right array of bytes to compare</param>
        /// <returns></returns>
        public Tuple<BinaryComparerEnum, List<DiffOffsetModel>> Compare(byte[] left, byte[] right)
        {
            int? diffStartIndex = null;
            List<DiffOffsetModel> offsets = new List<DiffOffsetModel>();

            if (left.Length != right.Length)
            {
                return new Tuple<BinaryComparerEnum, List<DiffOffsetModel>>(BinaryComparerEnum.DifferentSize, offsets);
            }
            else
            {
                for (int i = 0; i < left.Length; i++)
                {
                    bool bytesAreIdentical = left[i] == right[i];

                    if (!bytesAreIdentical && !diffStartIndex.HasValue)
                    {
                        diffStartIndex = i;
                    }
                    else if (bytesAreIdentical && diffStartIndex.HasValue)
                    {
                        offsets.Add(new DiffOffsetModel
                        {
                            StartIndex = diffStartIndex.Value,
                            Length = (i - diffStartIndex.Value)
                        });

                        diffStartIndex = null;
                    }
                }

                if (diffStartIndex.HasValue)
                {
                    offsets.Add(new DiffOffsetModel
                    {
                        StartIndex = diffStartIndex.Value,
                        Length = (left.Length - diffStartIndex.Value)
                    });

                    return new Tuple<BinaryComparerEnum, List<DiffOffsetModel>>(BinaryComparerEnum.EqualSize, offsets);
                }
                else
                {
                    return new Tuple<BinaryComparerEnum, List<DiffOffsetModel>>(BinaryComparerEnum.IdenticalBinary, offsets);
                }
            }

        }
    }

    /// <summary>
    /// Types of comparison results
    /// </summary>
    public enum BinaryComparerEnum
    {
        /// <summary>
        /// Binaries are of different size
        /// </summary>
        DifferentSize,
        /// <summary>
        /// Binaries are of equal size
        /// </summary>
        EqualSize,
        /// <summary>
        /// Binaries are the same 
        /// </summary>
        IdenticalBinary
    }

}
