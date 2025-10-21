using ExamService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamService.Data
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) { }

        public DbSet<Exam> Exams { get; set; }
    }

}
