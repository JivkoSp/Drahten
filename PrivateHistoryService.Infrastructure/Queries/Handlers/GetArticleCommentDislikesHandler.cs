﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleCommentDislikesHandler : IQueryHandler<GetArticleCommentDislikesQuery, List<DislikedArticleCommentDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleCommentDislikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<DislikedArticleCommentDto>> HandleAsync(GetArticleCommentDislikesQuery query)
        {
            var dislikedArticleCommentReadModels = await _readDbContext.DislikedArticleComments
               .Where(x => x.UserId == query.UserId.ToString())
               .Include(x => x.User)
               .AsNoTracking()
               .ToListAsync();

            return _mapper.Map<List<DislikedArticleCommentDto>>(dislikedArticleCommentReadModels);
        }
    }
}
