using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Events;
using PublicHistoryService.Domain.Exceptions;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class AddViewedUser
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private ViewedUser GetViewedUser()
        {
            var viewedUser = new ViewedUser(viewerUserID: Guid.NewGuid(), viewedUserID: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedUser;
        }

        public AddViewedUser()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ViewedUserAlreadyExistsException when the following condition is met:
        //Another ViewedUser value object with the same values already exists.
        [Fact]
        public void Duplicate_ViewedUser_Throws_ViewedUserAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var viewedUser = GetViewedUser();

            user.AddViewedUser(viewedUser);

            //ACT
            var exception = Record.Exception(() => user.AddViewedUser(viewedUser));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedUserAlreadyExistsException>();
        }

        //Should add ViewedUser value object to internal collection of ViewedUser value objects
        //and produce ViewedUserAdded domain event.
        //The ViewedUserAdded domain event should contain:
        //1. The same user entity to which the ViewedUser value object was added.
        //2. The same ViewedUser value object that was added to the internal collection of ViewedUser value objects.
        [Fact]
        public void Adds_ViewedUser_And_Produces_ViewedUserAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var viewedUser = GetViewedUser();

            //ACT
            var exception = Record.Exception(() => user.AddViewedUser(viewedUser));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.ViewedUsers.Count().ShouldBe(1);

            var viewedUserAddedEvent = user.DomainEvents.FirstOrDefault() as ViewedUserAdded;

            viewedUserAddedEvent.ShouldNotBeNull();

            viewedUserAddedEvent.User.ShouldBeSameAs(user);

            viewedUserAddedEvent.ViewedUser.ShouldBeSameAs(viewedUser);
        }
    }
}
