using System.Collections.ObjectModel;
using UserService.Domain.Events;
using UserService.Domain.Exceptions;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private UserFullName _userFullName;
        private UserNickName _userNickName;
        private UserEmailAddress _userEmailAddress;
        private HashSet<BannedUser> _bannedUsers;
        private List<ContactRequest> _contactRequests;
        private HashSet<UserTracking> _auditTrail;

        public IReadOnlyCollection<BannedUser> BannedUsers
        {
            get { return new ReadOnlyCollection<BannedUser>(_bannedUsers.ToList()); }
        }

        public IReadOnlyCollection<ContactRequest> ContactRequests
        {
            get { return new ReadOnlyCollection<ContactRequest>(_contactRequests); }
        }

        public IReadOnlyCollection<UserTracking> AuditTrail
        {
            get { return new ReadOnlyCollection<UserTracking>(_auditTrail.ToList()); }
        }

        private User()
        {
        }

        internal User(UserID userId, UserFullName userFullName, UserNickName userNickName, UserEmailAddress userEmailAddress)
        {
            ValidateConstructorParameters<NullUserParameterException>([userId, userFullName, userNickName, userEmailAddress]);

            Id = userId;
            _userFullName = userFullName;
            _userNickName = userNickName;
            _userEmailAddress = userEmailAddress;
            _bannedUsers = new HashSet<BannedUser>();
            _contactRequests = new List<ContactRequest>();
            _auditTrail = new HashSet<UserTracking>();
        }

        public void BanUser(BannedUser bannedUser)
        {
            var alreadyExists = _bannedUsers.Contains(bannedUser);

            if (alreadyExists)
            {
                throw new BannedUserAlreadyExistsException(Id, bannedUser.UserId);
            }

            _bannedUsers.Add(bannedUser);

            AddEvent(new BannedUserAdded(this, bannedUser));
        }

        public void UnbanUser(UserID bannedUserId)
        {
            var bannedUser = _bannedUsers.SingleOrDefault(x => x.UserId == bannedUserId);

            if (bannedUser == null)
            {
                throw new BannedUserNotFoundException(Id, bannedUserId);
            }

            _bannedUsers.Remove(bannedUser);

            AddEvent(new BannedUserRemoved(this, bannedUser));
        }

        public void AddContactRequest(ContactRequest contactRequest)
        {
            var alreadyExists = _contactRequests.Any(x => x.UserId == contactRequest.UserId);

            if (alreadyExists)
            {
                throw new ContactRequestAlreadyExistsException(Id, contactRequest.UserId);
            }

            _contactRequests.Add(contactRequest);

            AddEvent(new ContactRequestAdded(this, contactRequest));
        }

        public void RemoveContactRequest(ContactRequest contactRequest)
        {
            var alreadyExists = _contactRequests.Any(x => x.UserId == contactRequest.UserId);

            if (alreadyExists == false)
            {
                throw new ContactRequestNotFoundException(Id, contactRequest.UserId);
            }

            _contactRequests.Remove(contactRequest);

            AddEvent(new ContactRequestRemoved(this, contactRequest));
        }

        public void AddToAuditTrail(UserTracking userTracking)
        {
            var alreadyExists = _auditTrail.Contains(userTracking);

            if (alreadyExists)
            {
                throw new UserTrackingAlreadyExistsException(Id, userTracking.Action);
            }

            _auditTrail.Add(userTracking);

            AddEvent(new UserTrackingAuditAdded(this, userTracking));
        }
    }
}
