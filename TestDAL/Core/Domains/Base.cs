using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestDAL.Core.Domains
{
    public class Base
    {
        public Base() {
            Id = 0;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}