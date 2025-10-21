
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SubmissionService.Business.Interfaces;
using SubmissionService.Business.Services;
using SubmissionService.Data;
using SubmissionService.Data.Interfaces;
using SubmissionService.Data.Repositories;

namespace SubmissionService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // --- Cấu hình Neon DB (PostgreSQL) ---
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SubmissionDbContext>(options =>
                options.UseNpgsql(connectionString,
                    pgOptions => pgOptions.MigrationsAssembly("SubmissionService.Data")));

            // --- Cấu hình Supabase Storage ---
            var supabaseUrl = builder.Configuration["Supabase:Url"];
            var supabaseKey = builder.Configuration["Supabase:Key"];
            var supabaseBucket = builder.Configuration["Supabase:BucketName"];
            builder.Services.AddSingleton<IFileStorageService>(provider =>
                new SupabaseStorageService(supabaseUrl, supabaseKey, supabaseBucket));

            // --- Đăng ký Dependencies (Repository & Business) ---
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Triển khai Unit of Work
            builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            builder.Services.AddScoped<ISubmissionHandlerService, SubmissionHandlerService>();


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //============================================================================
            // Add Swagger
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "claim-request-api", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            //============================================================================

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
