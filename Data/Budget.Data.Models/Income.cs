namespace Budget.Data.Models
{
    using System;

    using Budget.Data.Common.Models;

    public class Income : BaseDeletableModel<int>
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsRepeating { get; set; }
    }
}
