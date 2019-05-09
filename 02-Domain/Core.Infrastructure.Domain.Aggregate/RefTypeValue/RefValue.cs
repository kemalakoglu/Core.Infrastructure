using System;
using System.Collections.Generic;
using System.Text;
using Core.Infrastructure.Domain.Aggregate;
using Core.Infrastructure.Domain.Aggregate.Base;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefValue:BaseEntity
    {
        public string Name { get; protected set; }
        public RefType RefType { get; protected set; }
    }
}
