using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class CreateArticleHandler : ICommandHandler<CreateArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleFactory _articleFactory;
        private readonly IArticleReadService _articleService;

        public CreateArticleHandler(IArticleRepository articleRepository, IArticleFactory articleFactory, IArticleReadService articleService)
        {
            _articleRepository = articleRepository;
            _articleFactory = articleFactory;
            _articleService = articleService;
        }

        public async Task HandleAsync(CreateArticleCommand command)
        {
            var alreadyExists = await _articleService.ExistsByIdAsync(command.ArticleId.ToString("N"));

            if(alreadyExists)
            {
                throw new ArticleAlreadyExistsException(command.ArticleId);
            }

            var article = _articleFactory.Create(command.ArticleId, command.PrevTitle, command.Title, command.Content, 
                command.PublishingDate, command.Author, command.Link, command.TopicId);

            await _articleRepository.AddArticleAsync(article);
        }
    } 
}
