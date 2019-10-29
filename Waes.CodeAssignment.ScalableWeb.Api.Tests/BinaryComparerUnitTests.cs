using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Waes.CodeAssignment.ScalableWeb.Api.Infrastructure;
using Waes.CodeAssignment.ScalableWeb.Api.Models;
using Waes.CodeAssignment.ScalableWeb.Api.Tests.TestHelpers;

namespace Waes.CodeAssignment.ScalableWeb.Api.Tests.Unit
{
    [TestClass]
    public class BinaryComparerUnitTests
    {
        [TestMethod]
        public void BinaryComparer_ShouldReturn_DifferentSizesAndBinaries()
        {
            var binaryComparer = new BinaryComparer();

            var leftBinary = new byte[10];
            var rightBinary = new byte[15];

            new Random().NextBytes(leftBinary);
            new Random().NextBytes(rightBinary);

            var actualResult = binaryComparer.Compare(leftBinary, rightBinary);
            var expectedResult = new Tuple<BinaryComparerEnum, List<DiffOffset>>(BinaryComparerEnum.DifferentSize, new List<DiffOffset>());

            Assert.AreEqual(actualResult.Item1, expectedResult.Item1);
            CollectionAssert.AreEquivalent(expectedResult.Item2.ToSimpleList(), actualResult.Item2.ToSimpleList());

        }

        [TestMethod]
        public void BinaryComparer_ShouldReturn_SameSizesAndDifferentBinaries()
        {
            var binaryComparer = new BinaryComparer();

            var leftBinary = new byte[15] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            var rightBinary = new byte[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };

            var actualResult = binaryComparer.Compare(leftBinary, rightBinary);
            var expectedResult = new Tuple<BinaryComparerEnum, List<DiffOffset>>(BinaryComparerEnum.EqualSize, new List<DiffOffset>() {
                    new DiffOffset { StartIndex = 0, Length = 15 } }
);

            Assert.AreEqual(actualResult.Item1, expectedResult.Item1);
            CollectionAssert.AreEquivalent(expectedResult.Item2.ToSimpleList(), actualResult.Item2.ToSimpleList());

        }

        [TestMethod]
        public void BinaryComparer_ShouldReturn_SameSizesAndIdenticalBinaries()
        {
            var binaryComparer = new BinaryComparer();

            //var 
            var leftBinary = new byte[15];
            new Random().NextBytes(leftBinary);
            var rightBinary = leftBinary;

            var actualResult = binaryComparer.Compare(leftBinary, rightBinary);
            var expectedResult = new Tuple<BinaryComparerEnum, List<DiffOffset>>(BinaryComparerEnum.IdenticalBinary, new List<DiffOffset>());

            Assert.AreEqual(actualResult.Item1, expectedResult.Item1);
            CollectionAssert.AreEquivalent(expectedResult.Item2.ToSimpleList(), actualResult.Item2.ToSimpleList());

        }
    }
}
