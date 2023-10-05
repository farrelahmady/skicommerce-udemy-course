namespace API.Errors
{
	public class ApiException : ApiResponse
	{
		public ApiException(int statusCode, string message, string details) : base(statusCode, message)
		{
			Details = details;
		}
		public ApiException(int statusCode, string message) : base(statusCode, message)
		{
		}
		public ApiException(int statusCode) : base(statusCode)
		{
		}


		public string Details { get; set; }
	}
}