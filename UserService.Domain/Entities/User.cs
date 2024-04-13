using System.Collections;
using System.Collections.ObjectModel;
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

            //TODO: Add event
        }

        public void AddContactRequest(ContactRequest contactRequest)
        {
            var alreadyExists = _contactRequests.Any(x => x.UserId == contactRequest.UserId);

            if (alreadyExists)
            {
                throw new ContactRequestAlreadyExistsException(Id, contactRequest.UserId);
            }

            _contactRequests.Add(contactRequest);

            //TODO: Add event
        }

        public void AddToAuditTrail(UserTracking userTracking)
        {
            var alreadyExists = _auditTrail.Contains(userTracking);

            if (alreadyExists)
            {
                throw new UserTrackingAlreadyExistsException(Id, userTracking.Action);
            }

            _auditTrail.Add(userTracking);

            //TODO: Add event
        }
    }
}
