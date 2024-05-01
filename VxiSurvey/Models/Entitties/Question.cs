namespace VxiSurvey.Models.Entitties
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; } = 3;
        public int? DepartmentId { get; set; }
    }

    public enum Department
    {
        DS = 0,
        SDIM = 1
    }
}
