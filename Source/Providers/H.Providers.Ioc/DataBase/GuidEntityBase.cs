using System;

namespace H.Providers.Ioc
{
    public abstract class GuidEntityBase : EntityBase<Guid>
    {
        public GuidEntityBase()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
