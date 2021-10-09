using System;

namespace Application.Domain.Core.Domain
{
    public abstract class Entity
    {
        public long Id { get; protected set; }
    }
}
