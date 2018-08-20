using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class BaseDto
    {
        public BaseDto()
        {
            Id = 0;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
