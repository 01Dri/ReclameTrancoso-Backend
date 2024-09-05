using Application.Factory;
using Application.UseCases.Resident;
using Application.Validations.Resident;
using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReclameTrancoso.IoC;

public static class DependencyInjectionRegister
{
    
    public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IResidentRepository, ResidentRepository>();

        return services;
    }
    public static IServiceCollection ConfigureUseCasesHandlers(this IServiceCollection services)
    {
        services.AddScoped<IUseCaseHandlerFactory, UseCaseHandlerFactory>();
        
        services.AddScoped<IUseCaseHandler<ResidentRegisterRequestDTO, ResidentRegisterResponseDTO>, RegisterResidentUserCase>();
        return services;
    }


    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ResidentRegisterRequestDTO>, ResidentRegisterRequestValidation>();
        return services;
    }
    
    
}