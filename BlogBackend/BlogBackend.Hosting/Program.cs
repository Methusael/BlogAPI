using BlogBackend.Application.Services;
using BlogBackend.Infrastructure.Data;
using BlogBackend.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Text;

namespace BlogBackend.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (!builder.Environment.IsEnvironment("Testing"))
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
            }

            Console.WriteLine("Adding logging.");
            builder.Services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddDebug();
            });

            Console.WriteLine("Adding controllers.");
            builder.Services.AddControllers();

            Console.WriteLine("Configure authentication.");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kPwuwFQxv2GGTAga3wMeMAjTfWlGJ4o3m11OqMUcbQuJ5Dw90Td6YyFFZI5zKDLfWCh4jrVp3zqOtKrzJlMRdYiqu1YSxsPc7fGh"))
                };
            });

            Console.WriteLine("Configure authorization.");
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            Console.WriteLine("Adding repositories.");
            RepositoryInjector.Register(builder.Services);

            Console.WriteLine("Adding mappers.");
            MapperInjector.Register(builder.Services);

            Console.WriteLine("Adding services.");
            ReadServiceInjector.Register(builder.Services);
            WriteServiceInjector.Register(builder.Services);
            builder.Services.AddScoped<IAuthService, AuthService>();

            Console.WriteLine("Done.");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}