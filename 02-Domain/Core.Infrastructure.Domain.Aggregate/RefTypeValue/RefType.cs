using System;
using System.Collections.Generic;
using System.Text;
using Core.Infrastructure.Domain.Aggregate;
using Core.Infrastructure.Domain.Aggregate.Base;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefType:BaseEntity
    {
        public RefType() { }

        public RefType(long Id, bool Status, DateTime? InsertDate, DateTime? UpdateDate, string Name, bool IsActive)
        {
            this.Id = Id;
            this.Status = Status;
            this.InsertDate = InsertDate;
            this.UpdateDate = UpdateDate;
            this.Name = Name;
            this.IsActive = IsActive;
        }
        public string Name { get; protected set; }
    }
}
