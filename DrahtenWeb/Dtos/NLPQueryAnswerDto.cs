namespace DrahtenWeb.Dtos
{
    public class NLPQueryAnswerDto
    {
        public string DocumentId { get; set; } 
        public string Answer { get; set; }
        public string AnswerType { get; set; }
        public float Score { get; set; }
        public string Context { get; set; }
        public DocumentDto Document { get; set; }
    }
}
