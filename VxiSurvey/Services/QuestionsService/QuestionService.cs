using VxiSurvey.Data;
using VxiSurvey.Models.Entitties;

namespace VxiSurvey.Services.QuestionsService
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _db;
        public QuestionService(DataContext db)
        {
            _db = db;
        }

        public List<Question> questions { get; private set; } = new();
        public List<Question> answeredSurveyQuestions { get; private set; } = new();

        List<Question> DbQuestions = new()
        {
            new Question{ Id = 1, Rating = 3, DepartmentId = (int)Department.SDIM, 
                Text = "How satisfied are you with the resolution time for your recent IT issue?"},
            new Question{ Id = 2, Rating = 3, DepartmentId = (int)Department.SDIM, 
                Text = "Rate the professionalism and courtesy of the support staff who assisted you."},
            new Question{ Id = 3, Rating = 3, DepartmentId = (int)Department.SDIM,
                Text = "On a scale of 1-5, how would you rate the overall quality of the IT service provided?"},
            new Question{ Id = 4, Rating = 3, DepartmentId = (int)Department.SDIM,
                Text = "Did the support team effectively communicate the steps taken to resolve your issue?"},
            new Question{ Id = 5, Rating = 3, DepartmentId = (int)Department.SDIM,
                Text = " How satisfied are you with the clarity and helpfulness of the instructions provided during the support process?"},
            new Question{ Id = 6, Rating = 3, DepartmentId = (int)Department.SDIM,
                Text = "Were your expectations met regarding the handling of your IT incident?"},
            new Question{ Id = 7, Rating = 3, DepartmentId = (int)Department.SDIM,
                Text = "How likely are you to recommend our Service Desk/Incident Management Team to a colleague or friend?"},
            new Question{ Id = 8, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "On a scale of 1-5, how satisfied are you with the responsiveness of our IT desktop support team?"},
            new Question{ Id = 9, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "Rate the effectiveness of the solutions provided by our IT desktop engineers on a scale of 1-5."},
            new Question{ Id = 10, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "How would you rate the clarity and communication of the IT desktop engineers?"},
            new Question{ Id = 11, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "How likely are you to recommend our IT desktop support services to others?"},
            new Question{ Id = 12, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "Did the IT desktop support team meet your expectations?"},
            new Question{ Id = 13, Rating = 3, DepartmentId = (int)Department.DS,
                Text = "Were your IT issues resolved in a timely manner?"}

        };

        public async Task GetAllQuestions(int departmentId)
        {
            questions = DbQuestions.Where(q => q.DepartmentId == departmentId).ToList();
        }

        public async Task FinishSurvey(List<Question> clientAnsweredQuestions)
        {
            answeredSurveyQuestions = clientAnsweredQuestions;

            var SurveyResponseData = new SurveyResponse
            {
                ResponseGuid = Guid.NewGuid()
            };

            var ServiceTransaction = _db.Database.BeginTransaction();

            try
            {
                await _db.SurveyResponse.AddAsync(SurveyResponseData);
                await _db.SaveChangesAsync();

                List<QuestionSurveyResponse> ServiceQuestionSurveyResponses = new List<QuestionSurveyResponse>();

                foreach (var question in answeredSurveyQuestions)
                {
                    ServiceQuestionSurveyResponses.Add(new QuestionSurveyResponse
                    {
                        Rating = question.Rating,
                        DepartmentId = question.DepartmentId,
                        QuestionReferenceId = question.Id,
                        SurveyResponseId = SurveyResponseData.Id
                    });
                }

                await _db.QuestionSurveyResponses.AddRangeAsync(ServiceQuestionSurveyResponses);
                await _db.SaveChangesAsync();

                ServiceTransaction.Commit();
            }
            catch
            {
                ServiceTransaction.Rollback();
            }

        }
    }
}
