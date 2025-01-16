using System.Collections;

namespace GestionTareas.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var resp = new
                {
                    Error = "Id no encontrado",
                    StackTrace = ex.StackTrace
                };

                await context.Response.WriteAsJsonAsync(resp);
            }
            
        }
    }
}
