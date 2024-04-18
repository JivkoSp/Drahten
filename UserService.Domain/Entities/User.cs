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
        private HashSet<BannedUser> _issuedUserBans;
        private List<ContactRequest> _issuedContactRequests;
        private List<ContactRequest> _receivedContactRequests;
        private HashSet<UserTracking> _auditTrail;

        public IReadOnlyCollection<BannedUser> IssuedUserBans
        {
            get { return new ReadOnlyCollection<BannedUser>(_issuedUserBans.ToList()); }
        }

        public IReadOnlyCollection<ContactRequest> IssuedContactRequests
        {
            get { return new ReadOnlyCollection<ContactRequest>(_issuedContactRequests); }
        }

        public IReadOnlyCollection<ContactRequest> ReceivedContactRequests
        {
            get { return new ReadOnlyCollection<ContactRequest>(_receivedContactRequests); }
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
            _issuedUserBans = new HashSet<BannedUser>();
            _issuedContactRequests = new List<ContactRequest>();
            _receivedContactRequests = new List<ContactRequest>();
            _auditTrail = new HashSet<UserTracking>();
        }

        public void BanUser(BannedUser bannedUser)
        {
            var alreadyExists = _issuedUserBans.Contains(bannedUser);

            if (alreadyExists)
            {
                throw new BannedUserAlreadyExistsException(Id, bannedUser.ReceiverUserId);
            }

            _issuedUserBans.Add(bannedUser);

            AddEvent(new BannedUserAdded(this, bannedUser));
        }

        public void UnbanUser(UserID bannedUserId)
        {
            var bannedUser = _issuedUserBans.SingleOrDefault(x => x.ReceiverUserId == bannedUserId);

            if (bannedUser == null)
            {
                throw new BannedUserNotFoundException(Id, bannedUserId);
            }

            _issuedUserBans.Remove(bannedUser);

            AddEvent(new BannedUserRemoved(this, bannedUser));
        }

        public void AddContactRequest(ContactRequest contactRequest)
        {
            var alreadyExists = _receivedContactRequests.Any(x => x.IssuerUserId == contactRequest.IssuerUserId);

            if (alreadyExists)
            {
                throw new ContactRequestAlreadyExistsException(Id, contactRequest.IssuerUserId);
            }

            _receivedContactRequests.Add(contactRequest);

            AddEvent(new ContactRequestAdded(this, contactRequest));
        }
        public void RemoveIssuedContactRequest(UserID receiverUserId)
        {
            var contactRequest = _issuedContactRequests.SingleOrDefault(x => x.ReceiverUserId == receiverUserId);

            if (contactRequest == null)
            {
                throw new ContactRequestNotFoundException(Id, receiverUserId);
            }

            _issuedContactRequests.Remove(contactRequest);

            AddEvent(new ContactRequestRemoved(this, contactRequest));
        }

        public void RemoveReceivedContactRequest(UserID issuerUserId)
        {
            var contactRequest = _receivedContactRequests.SingleOrDefault(x => x.IssuerUserId == issuerUserId);

            if (contactRequest == null)
            {
                throw new ContactRequestNotFoundException(Id, issuerUserId);
            }

            _receivedContactRequests.Remove(contactRequest);

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
