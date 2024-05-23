namespace VxiSurvey.Models.Entitties
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public Guid ResponseGuid { get; set; }

        //relations
        public List<QuestionSurveyResponse> QuestionSurveyResponses { get; set; }
    }
}
