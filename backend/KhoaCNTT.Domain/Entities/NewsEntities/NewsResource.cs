using KhoaCNTT.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaCNTT.Domain.Entities.NewsEntities
{
    public class NewsResource : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public long Size { get; set; }

        // Foreign key 
        public int CreatedBy { get; set; }
        public Admin Admin { get; set; } = null!;
    }
}
