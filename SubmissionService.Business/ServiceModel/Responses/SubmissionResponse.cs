using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Business.ServiceModel.Responses
{
    public class SubmissionResponse
    {
        public Guid SubmissionId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
    }
}
