using System;
using System.Collections.Generic;

namespace movieStoreDash.Data.models
{
    public partial class ActorInfo
    {
        public short ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FilmInfo { get; set; }
    }
}
