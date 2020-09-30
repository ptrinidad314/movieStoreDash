using System;
using System.Collections.Generic;

namespace movieStoreDash.Data.models
{
    public partial class SalesByFilmCategory
    {
        public string Category { get; set; }
        public decimal? TotalSales { get; set; }
    }
}
