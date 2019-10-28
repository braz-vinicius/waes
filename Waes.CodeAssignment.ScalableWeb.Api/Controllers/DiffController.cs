using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waes.CodeAssignment.ScalableWeb.Api.Models;
using Waes.CodeAssignment.ScalableWeb.Api.Services;

namespace Waes.CodeAssignment.ScalableWeb.Api.Controllers
{
    /// <summary>
    /// Api controller responsible for diffs
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly IBinaryDiffService diffService;

        /// <summary>
        /// Initializes a new instance of DiffController
        /// </summary>
        /// <param name="diffService"></param>
        public DiffController(IBinaryDiffService diffService)
        {
            this.diffService = diffService;
        }

        /// <summary>
        /// Compares the difference between two binaries given a specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the comparison operation</param>
        [HttpPost]
        [Route("/api/v1/[controller]/{id}")]
        public ActionResult<DiffResultModel> CompareMembers([FromRoute]int id)
        {
            var result = diffService.GetBinaryDiffResult(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }

        /// <summary>
        /// Adds a left member for comparison given a specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the comparison operation</param>
        /// <param name="request">Request object containing binary data encoded as base64 string</param>
        [HttpPost]
        [Route("/api/v1/[controller]/{id}/left")]
        public ActionResult AddLeftMember([FromRoute]int id, [FromBody]DiffDataRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || !IsBase64String(request.Data))
            {
                return BadRequest();
            }

            try
            {
                var binaryData = Convert.FromBase64String(request.Data);
                diffService.AddLeftMember(id, binaryData);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        /// <summary>
        /// Adds a right member for comparison given a specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">ID of the comparison operation</param>
        /// <param name="request">Request object containing binary data encoded as base64 string</param>
        [HttpPost]
        [Route("/api/v1/[controller]/{id}/right")]
        public ActionResult AddRightMember([FromRoute]int id, [FromBody]DiffDataRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || !IsBase64String(request.Data))
            {
                return BadRequest();
            }
            
            try
            {
                var binaryData = Convert.FromBase64String(request.Data);
                diffService.AddRightMember(id, binaryData);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        /// <summary>
        /// Checks a valid base64 string 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        private bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }
    }
}