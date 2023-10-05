namespace API.Errors
{
	public class ApiValidationErrorResponse : ApiResponse
	{
		public ApiValidationErrorResponse() : base(400)
		{
		}

		public ApiValidationErrorResponse(int statusCode, string message) : base(statusCode, message)
		{
		}

		public Dictionary<string, string[]> Errors { get; set; }
	}
}