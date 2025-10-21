using ExamService.Business.Interfaces;
using ExamService.Business.ServiceModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamManagementService _examService;

        public ExamsController(IExamManagementService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _examService.GetAllExamsAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var exam = await _examService.GetExamByIdAsync(id);
            if (exam == null) return NotFound();
            return Ok(exam);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Chỉ có Admin mới được tạo kỳ thi
        public async Task<IActionResult> CreateExam(CreateExam createExam)
        {
            // Lấy ID của người dùng đang thực hiện hành động từ token
            var creatorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(creatorIdString, out var creatorId))
            {
                return Unauthorized();
            }

            var newExam = await _examService.CreateExamAsync(createExam, creatorId);

            // Trả về 201 Created cùng với vị trí của tài nguyên mới
            return CreatedAtAction(nameof(GetExamById), new { id = newExam.Id }, newExam);
        }

    }
}
