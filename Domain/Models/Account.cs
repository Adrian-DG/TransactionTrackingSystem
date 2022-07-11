using Domain.Abstractions;

namespace Domain.Models
{
    public class Account : ModelMetadata
    {
        // ForeignKeys
        public Guid UserId { get; set; }
        public User User { get; set; }
        // TODO: add Account Type and Description name (identifier)
    }
}