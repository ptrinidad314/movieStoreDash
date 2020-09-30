﻿using System;
using System.Collections.Generic;

namespace movieStoreDash.Data.models
{
    public partial class Language
    {
        public Language()
        {
            FilmLanguage = new HashSet<Film>();
            FilmOriginalLanguage = new HashSet<Film>();
        }

        public byte LanguageId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LastUpdate { get; set; }

        public virtual ICollection<Film> FilmLanguage { get; set; }
        public virtual ICollection<Film> FilmOriginalLanguage { get; set; }
    }
}
