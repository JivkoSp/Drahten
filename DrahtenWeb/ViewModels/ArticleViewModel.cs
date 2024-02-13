using DrahtenWeb.Dtos;

namespace DrahtenWeb.ViewModels
{
    public class ArticleViewModel
    {
        public string DocumentId { get; set; } = string.Empty;
        public DocumentDto? Document { get; set; }
    }
}
