namespace DrahtenWeb.Dtos
{
    public class NLPQueryAnswerDto
    {
        public string document_id { get; set; } = string.Empty;
        public string? answer { get; set; }
        public string? answer_type { get; set; }
        public float? score { get; set; }
        public string? context { get; set; }
        public DocumentDto? document { get; set; }
    }
}
