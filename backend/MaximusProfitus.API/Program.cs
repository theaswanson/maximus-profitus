using MaximusProfitus.Core.Products;
using Square;

namespace MaximusProfitus.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            var squareClient = BuildSquareClient(builder.Environment, builder.Configuration);

            builder.Services.AddSingleton<ISquareClient>(squareClient);
            builder.Services.AddTransient<IProductService, SquareProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            static SquareClient BuildSquareClient(IWebHostEnvironment webHostEnvironment, IConfigurationRoot configuration)
            {
                var squareEnvironment = webHostEnvironment.IsDevelopment()
                    ? Square.Environment.Sandbox
                    : Square.Environment.Production;

                var accessToken = configuration["Square:AccessToken"];

                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Invalid Square access token. Ensure your access token is properly configured.");
                }

                return new SquareClient.Builder()
                    .Environment(squareEnvironment)
                    .AccessToken(accessToken)
                    .Build();
            }
        }
    }
}