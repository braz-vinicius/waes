using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Waes.CodeAssignment.ScalableWeb.Api.Models;

namespace Waes.CodeAssignment.ScalableWeb.Api.Tests.TestHelpers
{
    public static class DiffOffSetHelperExtension
    {
        public static List<string> ToSimpleList(this List<DiffOffsetModel> diffOffset)
        {
            return diffOffset.Select(x => $"{x.StartIndex};{x.Length}").ToList();

        }
    }
}
