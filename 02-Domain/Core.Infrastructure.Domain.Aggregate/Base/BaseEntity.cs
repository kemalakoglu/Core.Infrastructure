using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Infrastructure.Domain.Aggregate.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; protected set; }

        public bool Status { get; protected set; }

        public DateTime? InsertDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public bool IsActive { get; protected set; }

        //public abstract int getId();
    }
}
