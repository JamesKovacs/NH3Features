using System;

namespace Core {
    public abstract class Entity {
        public virtual Guid Id { get; private set; }
    }
}