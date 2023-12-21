using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UATP_RapidPay.Interfaces.CardManagement;
using UATP_RapidPay.Interfaces.PaymentFees;
using UATP_RapidPay.Services.CardManagement;
using UATP_RapidPay.Services.PaymentFees;

namespace UATP_RapidPay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register IConfiguration to access appsettings.json
            services.AddSingleton<IConfiguration>(Configuration);

            // Register services for Card Management Module
            services.AddScoped<ICardService, CardService>();
            services.AddSingleton<AuthenticationService>();

            // Register services for Payment Fees Module
            services.AddSingleton<IUfeService, UfeService>();
            services.AddScoped<IPaymentFeesService, PaymentFeesService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UATP_RapidPay", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UATP_RapidPay v1"));
            }

            app.UseRouting();

            // Authentication middleware
            app.Use(async (context, next) =>
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    return;
                }

                var authService = context.RequestServices.GetService<AuthenticationService>();
                var encodedCredentials = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                var decodedCredentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var username = decodedCredentials.Split(':', 2)[0];
                var password = decodedCredentials.Split(':', 2)[1];

                if (!authService.IsAuthenticated(username, password))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    return;
                }

                await next.Invoke();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
