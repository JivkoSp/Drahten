﻿using Newtonsoft.Json;
using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Presentation.Dtos;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterUserArticleTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        //*** IMPORTANT *** - The CreateArticleCommand must have valid TopicId (TopicId for Topic that already exist in the database).

        private async Task<RegisterUserArticleCommand> PrepareRegisterUserArticleCommandAsync()
        {
            var createArticleCommand = new CreateArticleCommand(Guid.NewGuid(), "prev title TEST", "title TEST", "content TEST",
                "2022-10-10T14:38:00", "no author", "no link", Guid.Parse("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"));

            await Post(createArticleCommand, "/topic-article-service/articles");

            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            await Post(registerUserCommand, "/topic-article-service/users");

            var registerUserArticleCommand = new RegisterUserArticleCommand(createArticleCommand.ArticleId, registerUserCommand.UserId);

            return registerUserArticleCommand;
        }

        public RegisterUserArticleTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //The UserArticle will link existing User with existing Article in the database.

        //Should return http status code 201 if UserArticle is created from the specified parameters in RegisterUserArticleCommand.
        [Fact]
        public async Task Register_UserArticle_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var registerUserArticleCommand = await PrepareRegisterUserArticleCommandAsync();

            //ACT
            var response = await Post(registerUserArticleCommand, 
                $"/topic-article-service/users/{registerUserArticleCommand.UserId}/articles/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/topic-article-service/users/{registerUserArticleCommand.UserId}/articles/");
        }

        //Should return http status code 200 when the previously created UserArticle is successfully received from the database.
        [Fact]
        public async Task Register_UserArticle_Endpoint_Should_Add_UserArticle_With_Given_ArticleId_And_UserId_To_The_Database()
        {
            //ARRANGE
            var registerUserArticleCommand = await PrepareRegisterUserArticleCommandAsync();

            await Post(registerUserArticleCommand, $"/topic-article-service/users/{registerUserArticleCommand.UserId}/articles/");

            //ACT
            var response = await Get($"/topic-article-service/users/articles/{registerUserArticleCommand.ArticleId.ToString("N")}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var userDto = JsonConvert.DeserializeObject<List<UserDto>>(Convert.ToString(responseDto.Result)).FirstOrDefault();

            userDto.ShouldNotBeNull();

            userDto.UserId.ShouldBe(registerUserArticleCommand.UserId);
        }
    }
}
