using System.Net;
using System.Text.Json;
using TaskManager.Domain.Exceptions;

namespace TaskManager.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (DomainException ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			context.Response.ContentType = "application/json";

			var response = new { error = ex.Message };

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
		catch
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			var response = new { error = "An unexpected error occured." };

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
