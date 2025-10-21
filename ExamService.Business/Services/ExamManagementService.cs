using ExamService.Business.Interfaces;
using ExamService.Business.ServiceModels.Requests;
using ExamService.Business.ServiceModels.Response;
using ExamService.Data.Interfaces;
using ExamService.Data.Models;

namespace ExamService.Business.Services
{
    public class ExamManagementService : IExamManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        // Có thể inject thêm AutoMapper để tự động map Entity sang DTO

        public ExamManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ExamResponse?> CreateExamAsync(CreateExam examDto, Guid creatorId)
        {
            var exam = new Exam
            {
                Id = Guid.NewGuid(),
                Name = examDto.Name,
                Subject = examDto.Subject,
                ExamDate = examDto.ExamDate,
                CreatedByUserId = creatorId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Exams.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();

            // Map thủ công Entity sang DTO
            return new ExamResponse
            {
                Id = exam.Id,
                Name = exam.Name,
                Subject = exam.Subject,
                ExamDate = exam.ExamDate,
                CreatedAt = exam.CreatedAt
            };
        }

        public async Task<IEnumerable<ExamResponse>> GetAllExamsAsync()
        {
            var exams = await _unitOfWork.Exams.GetAllAsync();
            // Map danh sách
            return exams.Select(e => new ExamResponse
            {
                Id = e.Id,
                Name = e.Name,
                Subject = e.Subject,
                ExamDate = e.ExamDate,
                CreatedAt = e.CreatedAt
            });
        }

        public async Task<ExamResponse?> GetExamByIdAsync(Guid id)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(id);
            if (exam == null) return null;

            return new ExamResponse
            {
                Id = exam.Id,
                Name = exam.Name,
                Subject = exam.Subject,
                ExamDate = exam.ExamDate,
                CreatedAt = exam.CreatedAt
            };
        }
    }
}
