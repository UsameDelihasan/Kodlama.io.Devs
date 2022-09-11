using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using FluentValidation;
using Core.Application.Pipelines.Validation;
using Application.Features.ProgrammingLanguages.Rules;
using Core.Application.Pipelines.Authorization;
using Microsoft.AspNetCore.Http;
using Application.Features.Authorization.Rules;
using Application.Features.Frameworks.Rules;
using Application.Features.GitAccounts.Rules;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<AuthorizationBusinessRules>();
            services.AddScoped<FrameworkBusinessRules>();
            services.AddScoped<GitAccountBusinessRules>();
            



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }
    }
}
