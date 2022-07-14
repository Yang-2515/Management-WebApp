using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Base
{
    public abstract class BaseDomainEvent : INotification
    {
        public BaseDomainEvent()
        {
            EventId = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public virtual Guid EventId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
