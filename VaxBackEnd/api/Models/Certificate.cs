using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace api.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }
     
        public string FilePath { get; set; }
         public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}









