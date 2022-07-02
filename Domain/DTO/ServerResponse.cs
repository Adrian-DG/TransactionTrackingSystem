namespace Domain.DTO
{
    public class ServerResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }


        private ServerResponse GetResponse(bool status)
        {
            return status 
            ? new ServerResponse { Title = "Ok", Message = "Process finish successfully", Status = status }
            : new ServerResponse { Title = "Error", Message = "Something went wrong!!", Status = status };
        }


    }
}