using Microsoft.EntityFrameworkCore;
using VxiSurvey.Models.Entitties;

namespace VxiSurvey.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SurveyResponse> SurveyResponse { get; set;}
        public DbSet<QuestionSurveyResponse> QuestionSurveyResponses { get; set; }
    }
}
