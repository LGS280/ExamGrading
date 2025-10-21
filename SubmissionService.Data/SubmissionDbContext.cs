using Microsoft.EntityFrameworkCore;
using SubmissionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionService.Data
{
    public class SubmissionDbContext : DbContext
    {
        public SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : base(options) { }

        public DbSet<Submission> Submissions { get; set; }
    }
}
