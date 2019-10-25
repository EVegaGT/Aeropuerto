using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }

        public virtual ICollection<IDomainEvent> Events { get; set; }
    }
}
