using System;
using System.Collections.Generic;

namespace movieStoreDash.Data.models
{
    public partial class Socialmedia
    {
        public long SocialmediaId { get; set; }
        public short? ActorId { get; set; }
        public string Url { get; set; }
    }
}
