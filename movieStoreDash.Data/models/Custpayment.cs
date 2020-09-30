using System;
using System.Collections.Generic;

namespace movieStoreDash.Data.models
{
    public partial class Custpayment
    {
        public short PaymentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        public int? RentalId { get; set; }
    }
}
