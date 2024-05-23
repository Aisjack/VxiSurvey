namespace VxiSurvey.Models.Entitties
{
    public class QuestionSurveyResponse : Question
    {
        public int QuestionReferenceId { get; set; }

        //relations
        public int SurveyResponseId { get; set; }
        public SurveyResponse SurveyResponse { get; set; }
    }
}
