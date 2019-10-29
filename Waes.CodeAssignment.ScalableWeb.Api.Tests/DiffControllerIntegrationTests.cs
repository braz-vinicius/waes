using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Waes.CodeAssignment.ScalableWeb.Api.Controllers;
using Waes.CodeAssignment.ScalableWeb.Api.Infrastructure;
using Waes.CodeAssignment.ScalableWeb.Api.Models;
using Waes.CodeAssignment.ScalableWeb.Api.Services;
using Waes.CodeAssignment.ScalableWeb.Api.Tests.TestHelpers;

namespace Waes.CodeAssignment.ScalableWeb.Api.Tests
{
    [TestClass]
    public class DiffControllerIntegrationTests
    {

        [TestMethod]
        public void DiffController_CompareMembers_ShouldReturn_DifferentSizeBinaries()
        {

            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 1;

            var testMockOriginal = File.ReadAllBytes("TestData\\waes.bmp");
            var testMockEnlarged = File.ReadAllBytes("TestData\\waes-large.bmp");

            controller.AddLeftMember(testId, new DiffRequest { Data = Convert.ToBase64String(testMockOriginal) });
            controller.AddRightMember(testId, new DiffRequest { Data = Convert.ToBase64String(testMockEnlarged) });

            var actualResult = controller.CompareMembers(testId);
            var expectedResult = new DiffResult { ComparisonResult = "The binaries have different sizes", DiffOffsets = new List<DiffOffset>() };

            Assert.AreEqual(expectedResult.ComparisonResult, actualResult.Value.ComparisonResult);
            CollectionAssert.AreEquivalent(expectedResult.DiffOffsets, actualResult.Value.DiffOffsets);
        }

        [TestMethod]
        public void DiffController_CompareMembers_ShouldReturn_SameSizeIdenticalBinaries()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 2;

            var testMockOriginal = File.ReadAllBytes("TestData\\waes.bmp");
            var testMockEnlarged = File.ReadAllBytes("TestData\\waes.bmp");

            controller.AddLeftMember(testId, new Models.DiffRequest { Data = Convert.ToBase64String(testMockOriginal) });
            controller.AddRightMember(testId, new Models.DiffRequest { Data = Convert.ToBase64String(testMockEnlarged) });

            var actualResult = controller.CompareMembers(testId);
            var expectedResult = new DiffResult { ComparisonResult = "The binaries are identical", DiffOffsets = new List<DiffOffset>() };

            Assert.AreEqual(expectedResult.ComparisonResult, actualResult.Value.ComparisonResult);
            CollectionAssert.AreEqual(expectedResult.DiffOffsets, actualResult.Value.DiffOffsets);

        }

        [TestMethod]
        public void DiffController_CompareMembers_ShouldReturn_SameSizeDifferentBinaries()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 3;

            var testMockOriginal = File.ReadAllBytes("TestData\\waes.bmp");
            var testMockEnlarged = File.ReadAllBytes("TestData\\waes-inverted.bmp");

            controller.AddLeftMember(testId, new Models.DiffRequest { Data = Convert.ToBase64String(testMockOriginal) });
            controller.AddRightMember(testId, new Models.DiffRequest { Data = Convert.ToBase64String(testMockEnlarged) });

            var actualResult = controller.CompareMembers(testId);
            var expectedResult = new DiffResult
            {
                ComparisonResult = "The binaries are of equal size",
                DiffOffsets = new List<DiffOffset>() {
                    new DiffOffset { StartIndex = 74, Length = 1 },
                    new DiffOffset { StartIndex = 106, Length = 1 },
                    new DiffOffset { StartIndex = 138, Length = 5712 } }
            };

            Assert.AreEqual(expectedResult.ComparisonResult, actualResult.Value.ComparisonResult);
            CollectionAssert.AreEquivalent(expectedResult.DiffOffsets.ToSimpleList(), actualResult.Value.DiffOffsets.ToSimpleList());
        }

        [TestMethod]
        public void DiffController_AddLeftMember_ShouldReturn_HttpBadRequest_EmptyBase64String()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 4;

            var response = controller.AddLeftMember(testId, new DiffRequest { Data = "" });

            var actualResult = response as BadRequestResult;
            var expectedResult = new BadRequestResult();

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [TestMethod]
        public void DiffController_AddLeftMember_ShouldReturn_HttpBadRequest_InvalidBase64String()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 5;

            var response = controller.AddLeftMember(testId, new DiffRequest { Data = "InvalidBase64String" });

            var actualResult = response as BadRequestResult;
            var expectedResult = new BadRequestResult();

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [TestMethod]
        public void DiffController_AddRightMember_ShouldReturn_HttpBadRequest_EmptyBase64String()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 6;

            var response = controller.AddRightMember(testId, new DiffRequest { Data = "" });

            var actualResult = response as BadRequestResult;
            var expectedResult = new BadRequestResult();

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);

        }

        [TestMethod]
        public void DiffController_AddRightMember_ShouldReturn_HttpBadRequest_InvalidBase64String()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 7;

            var response = controller.AddRightMember(testId, new DiffRequest { Data = "InvalidBase64String" });

            var actualResult = response as BadRequestResult;
            var expectedResult = new BadRequestResult();

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [TestMethod]
        public void DiffController_CompareMembers_ShouldReturn_HttpNotFound_NonExistentId()
        {
            var controller = new DiffController(new BinaryDiffService(new BinaryRepository(), new BinaryComparer()));
            int testId = 1000;

            var actualResult = controller.CompareMembers(testId).Result as NotFoundResult;
            var expectedResult = new NotFoundResult();

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }
    }
}
