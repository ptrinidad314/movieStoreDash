using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieStoreDash.Models
{
    public class ActorDTO
    {
        public int actorID { get; set; }
        public string bio { get; set; }
        public string imageUrl { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string socialMediaURL { get; set; }
        public bool autoOpenURL { get; set; }
    }
}
