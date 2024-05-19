using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class RemoveViewedUser
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

        public RemoveViewedUser()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ViewedUserNotFoundException when the following condition is met:
        //There is no ViewedUser value object with the same values.
        [Fact]
        public void ViewedUser_NotFound_Throws_ViewedUserNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var viewedUser = GetViewedUser();

            //ACT
            var exception = Record.Exception(() => user.RemoveViewedUser(viewedUser));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedUserNotFoundException>();
        }

        //Should remove ViewedUser value object from internal collection of ViewedUser value objects
        //and produce ViewedUserRemoved domain event.
        //The ViewedUserRemoved domain event should contain:
        //1. The same user entity that the ViewedUser value object was removed from.
        //2. The same ViewedUser value object that was removed from the internal collection of ViewedUser value objects.
        [Fact]
        public void Removes_ViewedUser_And_Produces_ViewedUserRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var viewedUser = GetViewedUser();

            user.AddViewedUser(viewedUser);

            //ACT
            var exception = Record.Exception(() => user.RemoveViewedUser(viewedUser));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.ViewedUsers.Count().ShouldBe(0);

            var viewedUserAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ViewedUserAdded)) as ViewedUserAdded;

            var viewedUserRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ViewedUserRemoved)) as ViewedUserRemoved;

            viewedUserAddedEvent.ShouldNotBeNull();

            viewedUserAddedEvent.User.ShouldBeSameAs(user);

            viewedUserAddedEvent.ViewedUser.ShouldBeSameAs(viewedUser);

            viewedUserRemovedEvent.ShouldNotBeNull();

            viewedUserRemovedEvent.User.ShouldBeSameAs(user);

            viewedUserRemovedEvent.ViewedUser.ShouldBeSameAs(viewedUser);
        }
    }
}
