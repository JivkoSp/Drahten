namespace DrahtenWeb.Dtos
{
    public class QueryAnswerDto
    {
        public string document_id { get; set; } = string.Empty;
        public float? score { get; set; }
        public DocumentDto? document { get; set; }
    }
}
