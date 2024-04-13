using UserService.Domain.Events;

namespace UserService.Domain.Entities
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; protected set; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;
        private bool _versionIncremented = false;
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected void AddEvent(IDomainEvent domainEvent)
        {
            if (!_domainEvents.Any() && _versionIncremented == false)
            {
                Version++;
                _versionIncremented = true;
            }

            _domainEvents.Add(domainEvent);
        }

        //The domain events will live within one http request life time.
        public void ClearEvents() => _domainEvents.Clear();

        //Once anything in the aggregate is modified, the version of the aggregate must be incremented.
        //That means that the version of the aggregate will be sign that something in the aggregate is changed.
        protected void IncrementVersion()
        {
            //If the aggregate is changed multiple times, increment the version ONLY once.
            if (_versionIncremented) return;

            Version++;
            _versionIncremented = true;
        }
    }
}
