using TopicArticleService.Application.Services;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    public class AddArticleCommentLikeHandler : ICommandHandler<AddArticleCommentLikeCommand>
    {
        private IArticleRepository _articleRepository;

        //TODO:
        //IArticleCommentRepository with
        //GetArticleCommentByIdAsync
        //AddArticleCommentAsync
        //UpdateArticleCommentAsync
        //DeleteArticleCommentAsync

        public AddArticleCommentLikeHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;

        }

        public Task HandleAsync(AddArticleCommentLikeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
