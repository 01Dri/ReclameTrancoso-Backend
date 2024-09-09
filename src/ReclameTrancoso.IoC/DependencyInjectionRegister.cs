using Application.Factory;
using Application.Services;
using Application.UseCases.Auth;
using Application.UseCases.Building;
using Application.UseCases.Resident;
using Application.Validations.Auth;
using Application.Validations.Resident;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Auth;
using Domain.Models.DTOs.Building;
using Domain.Models.DTOs.Resident;
using FluentValidation;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReclameTrancoso.Domain.Interfaces.Auth;
using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;
using ReclameTrancoso.Domain.Interfaces.Transactions;

namespace ReclameTrancoso.IoC;

public static class DependencyInjectionRegister
{
    
    public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IResidentRepository, ResidentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IApartmentsResidentsRepository, ApartmentsResidentsRepository>();
        services.AddScoped<IBuildingResidentsRepository, BuildingsResidentsRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IPasswordEncoder, BCryptPasswordEncoder>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
        services.AddScoped<ITokenService<User, TokenResponseDTO>, JWTService>();
        

        return services;
    }
    public static IServiceCollection ConfigureUseCasesHandlers(this IServiceCollection services)
    {
        services.AddScoped<IUseCaseHandlerFactory, UseCaseHandlerFactory>();
        
        services.AddScoped<IUseCaseHandler<ResidentRegisterRequestDTO, ResidentRegisterResponseDTO>,
            RegisterResidentUserCase>();
        
        services.AddScoped<IUseCaseHandler<GetRequest, BuildingResponseDTO>,
            BuildingUseCase>();
        
        services.AddScoped<IUseCaseHandler<LoginRequestDTO, TokenResponseDTO>,
            LoginUseCase>();
        return services;
    }


    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ResidentRegisterRequestDTO>, RegisterResidentRequestValidation>();
        services.AddScoped<IValidator<LoginRequestDTO>, LoginRequestValidation>();

        return services;
    }
    
    
}