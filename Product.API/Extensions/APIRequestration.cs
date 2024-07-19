using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Product.API.Errors;
using System.Reflection;

namespace Product.API.Extensioins
{
    public static class APIRequestration
    {
        public static IServiceCollection AddAPIRequestration(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7221");
                });
            });
            return services;
        }
    }
}
