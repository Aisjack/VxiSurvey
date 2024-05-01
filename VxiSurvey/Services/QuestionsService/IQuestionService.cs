using VxiSurvey.Models.Entitties;

namespace VxiSurvey.Services.QuestionsService
{
    public interface IQuestionService
    {
        public List<Question> questions { get; }
        public List<Question> answeredSurveyQuestions { get; }
        Task GetAllQuestions(int departmentId);
        Task FinishSurvey(List<Question> clientAnsweredQuestions);
    }
}
