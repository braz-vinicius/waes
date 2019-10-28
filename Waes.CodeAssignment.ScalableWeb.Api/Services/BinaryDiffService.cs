using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.CodeAssignment.ScalableWeb.Api.Entities;
using Waes.CodeAssignment.ScalableWeb.Api.Infrastructure;
using Waes.CodeAssignment.ScalableWeb.Api.Models;

namespace Waes.CodeAssignment.ScalableWeb.Api.Services
{
    /// <summary>
    /// Business service responsible for managing the binary diff comparisons
    /// </summary>
    public class BinaryDiffService : IBinaryDiffService
    {
        private readonly IRepository<BinaryDiffComparisonEntity> repository;
        private readonly BinaryComparer binaryComparer;

        /// <summary>
        /// Initializes a new BinaryDiffService
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="binaryComparer"></param>
        public BinaryDiffService(IRepository<BinaryDiffComparisonEntity> repository, BinaryComparer binaryComparer)
        {
            this.repository = repository;
            this.binaryComparer = binaryComparer;
        }

        /// <summary>
        /// Gets the difference model given an operation <paramref name="id"/>
        /// </summary>
        /// <param name="id">id of the operation</param>
        /// <returns></returns>
        public DiffResultModel GetBinaryDiffResult(int id)
        {
            var entity = repository.Get(x => x.Id == id).SingleOrDefault();

            if (entity == null)
            {
                return null;
            }
            else
            {
                var result = CompareBinaries(id);

                return result;

            }

        }

        /// <summary>
        /// Compares two binaries given an operation <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DiffResultModel CompareBinaries(int id)
        {

            var comparisonModel = repository.Get(x => x.Id == id).Single();
            var comparisonResult = binaryComparer.Compare(comparisonModel.LeftMember, comparisonModel.RightMember);
            var result = new DiffResultModel() { DiffOffsets = comparisonResult.Item2 };

            switch (comparisonResult.Item1)
            {
                case BinaryComparerEnum.DifferentSize:
                    result.ComparisonResult = "The binaries have different sizes";
                    break;
                case BinaryComparerEnum.EqualSize:
                    result.ComparisonResult = "The binaries are of equal size";
                    break;
                case BinaryComparerEnum.IdenticalBinary:
                    result.ComparisonResult = "The binaries are identical";
                    break;
                default:
                    break;
            }

            return result;

        }

        /// <summary>
        /// Adds the binary left member for comparison
        /// </summary>
        /// <param name="id">id of the comparison</param>
        /// <param name="data">binary data to be compared</param>
        public void AddLeftMember(int id, byte[] data)
        {
            AddOrUpdateMember(id, data, "left");
        }

        /// <summary>
        /// Adds the binary right member for comparison
        /// </summary>
        /// <param name="id">id of the comparison</param>
        /// <param name="data">binary data to be compared</param>
        public void AddRightMember(int id, byte[] data)
        {
            AddOrUpdateMember(id, data, "right");
        }

        /// <summary>
        /// Adds or update a binary member for comparison
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="memberSide"></param>
        private void AddOrUpdateMember(int id, byte[] data, string memberSide)
        {
            var entity = repository.Get(x => x.Id == id).SingleOrDefault();

            if (entity == null)
            {
                repository.Insert(new BinaryDiffComparisonEntity
                {
                    Id = id,
                    LeftMember = memberSide == "left" ? data : null,
                    RightMember = memberSide == "right" ? data : null
                });

                return;
            }

            if (memberSide == "right")
            {
                entity.RightMember = data;
            }
            else
            {
                entity.LeftMember = data;
            }


            repository.Update(entity);
        }

    }
}
