using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Middleware;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Models.Profiles;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Services;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace BaseCodeAPI
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         ConfigServices(builder.Services);

         var app = builder.Build();

         app.UseSwagger();

         app.UseSwaggerUI(options =>
         {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Net Angulary");
            options.DefaultModelsExpandDepth(-1); 
            options.InjectStylesheet("/public/swagger-ui/custom.css");
            options.RoutePrefix = "swagger";
         });

         app.UseStaticFiles();
         app.UseHttpsRedirection();
         app.UseSpa(spa =>
         {
            spa.Options.SourcePath = "wwwroot";

            if (app.Environment.IsDevelopment())
            {
               spa.UseAngularCliServer(npmScript: "start");
            }
         });

         app.UseRouting();
         app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         app.TokenMiddlewareBuilder();
         app.UseAuthentication();
         app.UseAuthorization();
         app.MapControllers();
         app.Run("http://*:5005");
      }

      public static void ConfigServices(IServiceCollection services)
      {
         services.AddCors();
         services.AddControllers();
         services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot/dist";});     
         services.AddHttpContextAccessor();
         services.AddSwaggerGen();
         services.AddAutoMapper(typeof(AutoMapperProfile));
         services.AddScoped<IAuthenticate, AuthenticateService>();

         services.AddScoped<UserService>();
         services.AddScoped<ClientService>();
         services.AddScoped<AddressService>();

         services.AddScoped<IRepository<UserModel>, UserRepository>();
         services.AddScoped<IRepository<AddressModel>, AddressRepository>();
         services.AddScoped<IRepository<ClientModel>, ClientRepository>();

         services.AddAuthentication();
         services.AddAuthorization();

         services.AddScoped<IServicesFactory, ServicesFactory>();
      }

   }
}
