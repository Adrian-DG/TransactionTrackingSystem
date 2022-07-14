using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Models
{
    public class Account : ModelMetadata
    {
        public string Description { get; set; }
        public AccountType Type { get; set; }
        // ForeignKeys
        public Guid UserId { get; set; }
        public User User { get; set; }
        // TODO: add Account Type and Description name (identifier)
    }
}