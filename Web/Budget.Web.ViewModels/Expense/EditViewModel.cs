namespace Budget.Web.ViewModels.Expense
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Budget.Services.Mapping;
    using Budget.Data.Models;

    public class EditViewModel : IMapFrom<Expense>
    {
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
