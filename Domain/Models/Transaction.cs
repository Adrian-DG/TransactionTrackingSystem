using Domain.Abstractions;

namespace Domain.Models
{
    public class Transaction : ModelMetadata
    {
        public string Concept { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        // Foreign keys
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

    }
}