using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; }
		public string Message { get; }
		public ApiResponse(int statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
		}

		public ApiResponse(int statusCode)
		{
			StatusCode = statusCode;
			Message = GetDefaultMessageForStatusCode(statusCode);
		}

		private static string GetDefaultMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "Bad Request",
				401 => "Unauthorized",
				404 => "Not Found",
				500 => "Internal Server Error",
				_ => null
			};
		}
	}
}