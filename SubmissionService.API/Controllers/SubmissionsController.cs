using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Business.Interfaces;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionHandlerService _submissionHandler;

        public SubmissionsController(ISubmissionHandlerService submissionHandler)
        {
            _submissionHandler = submissionHandler;
        }

        [HttpPost("upload/{examId}")]
        public async Task<IActionResult> UploadSubmission(Guid examId, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("File is required.");

            var uploaderId = Guid.NewGuid(); // Lấy từ claims của JWT Token

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _submissionHandler.HandleSubmissionAsync(
                    stream, file.FileName, file.ContentType, file.Length, examId, uploaderId
                );
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            //catch (Exception)
            //{
            //    // Log lỗi
            //    return StatusCode(500, "An internal server error occurred.");
            //}
        }
    }
}
