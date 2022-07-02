namespace Domain.DTO
{
    public record UserModelDTO 
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}