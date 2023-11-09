using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Converters;
using TodoList.Common.Auth;
using TodoList.Common.Filters;
using TodoList.Common.GlobalMiddlewares;
using TodoList.WebAPI.Auth;
using TodoList.WebAPI.Extensions;

namespace TodoList.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>())
                .AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));

            builder.Services.AddTodoListContext(builder.Configuration);
            builder.Services.RegisterCustomServices();
            builder.Services.AddAutoMapper();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication()
               .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>(BasicAuthDefaults.AuthenticationScheme, null);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GenericExceptionHandlerMiddleware>();

            app.UseCors(opt => opt
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}