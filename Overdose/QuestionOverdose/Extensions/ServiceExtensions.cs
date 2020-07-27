using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuestionOverdose.BLL.Services;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Repositories;
using QuestionOverdose.Helpers;

namespace QuestionOverdose.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServicesWrapper(this IServiceCollection services)
        {
            services.AddScoped<AuthHelper>();
            services.AddScoped<UserService>();
            services.AddScoped<QuestionService>();

            services.AddScoped<UserRepository>();
            services.AddScoped<TagRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<QuestionRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<AnswerRepository>();
            services.AddScoped<UserTagRepository>();
            services.AddScoped<QuestionTagRepository>();
            services.AddScoped<UserAnswerRepository>();
            services.AddScoped<UserQuestionRepository>();

            services.AddScoped<UnitOfWork>();
            services.AddScoped<EFContext>();
        }

        public static void ConfigureJWTnServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddOptions();
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = appSettings.UrlSettings,
                    ValidAudience = appSettings.UrlSettings,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
    }
}
