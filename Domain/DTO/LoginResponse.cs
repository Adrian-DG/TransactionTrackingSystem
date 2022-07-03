namespace Domain.DTO
{
    public class LoginResponse : ServerResponse
    {
		public string UserId { get; set; }
		public string Token { get; set; }
    }
}