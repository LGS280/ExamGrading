using SubmissionService.Business.ServiceModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Business.Interfaces
{
    public interface ISubmissionHandlerService
    {
        Task<SubmissionResponse> HandleSubmissionAsync(
                Stream fileStream,
                string originalFileName,
                string contentType,
                long fileSize,
                Guid examId,
                Guid uploaderId);

    }
}
