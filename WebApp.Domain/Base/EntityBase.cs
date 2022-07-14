using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            _events = new List<BaseDomainEvent>();
        }

        [NotMapped]
        private List<BaseDomainEvent> _events;
        [NotMapped]
        public IReadOnlyList<BaseDomainEvent> Events => _events.AsReadOnly();

        protected void AddEvent(BaseDomainEvent @event)
        {
            _events.Add(@event);
        }

        protected void RemoveEvent(BaseDomainEvent @event)
        {
            _events.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _events?.Clear();
        }
    }
    public abstract class EntityBase<TKey> : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("0")]
        public virtual TKey Id { get; set; }
    }
}