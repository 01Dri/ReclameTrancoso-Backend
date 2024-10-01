using Application.Factory;
using Application.Services;
using Application.UseCases.Auth;
using Application.UseCases.Building;
using Application.UseCases.Complaint;
using Application.UseCases.Manager;
using Application.UseCases.Resident;
using Application.Validations.Auth;
using Application.Validations.Complaint;
using Application.Validations.Manager;
using Application.Validations.Resident;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Auth;
using Domain.Models.DTOs.Building;
using Domain.Models.DTOs.Complaint;
using Domain.Models.DTOs.Manager;
using Domain.Models.DTOs.Resident;
using Domain.Models.Pagination;
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
    public static IServiceCollection ConfigureService
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IApartmentRepository, ApartmentRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IResidentRepository, ResidentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IApartmentsResidentsRepository, ApartmentsResidentsRepository>();
        services.AddScoped<IBuildingResidentsRepository, BuildingsResidentsRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IComplaintRepository, ComplaintRepository>();
        services.AddScoped<IResidentComplaintRepository, ResidentComplaintRepository>();
        
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IManagerComplaintCommentsRepository, ManagerComplaintCommentsRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        
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
            GetBuildingsUseCase>();
        
        services.AddScoped<IUseCaseHandler<LoginRequestDTO, TokenResponseDTO>,
            LoginUseCase>();
        
        services.AddScoped<IUseCaseHandler<RefreshTokenRequestDTO, TokenResponseDTO>,
            RefreshTokenUseCase>();
        
        services.AddScoped<IUseCaseHandler<GetByIdRequest, ResidentResponseDTO>,
            GetResidentByIdUseCase>();
        
        services.AddScoped<IUseCaseHandler<ComplaintCreateRequestDTO, CreatedResponse>,
            ComplaintCreateUseCase>();
        
        services.AddScoped<IUseCaseHandler<GetRequestPaginatedById, PagedResponseDto<ComplaintDto>>,
            ComplaintsGetByResidentIdUseCase>();
        
        services.AddScoped<IUseCaseHandler<
            ManagerAddCommentRequestDTO, 
            ManagerAddCommentResponseDTO>,
            ManagerAddCommentUseCase>();
           
        services.AddScoped<IUseCaseHandler<ComplaintUpdateRequestDTO, ComplaintDto>,
            ComplaintUpdateUseCase>();
        
        services.AddScoped<IUseCaseHandler<GetRequestPaginated, PagedResponseDto<ComplaintDto>>,
            ComplaintGetUseCase>();

        
        return services;
    }

    public static IServiceCollection ConfigureUseCasesHandlersRes(this IServiceCollection services)
    {
        services.AddScoped<IUseCaseHandlerRes<DeleteRequest>,
            ComplaintDeleteByIdUseCase>();
        
        return services;
    }


    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ResidentRegisterRequestDTO>, RegisterResidentRequestValidation>();
        services.AddScoped<IValidator<LoginRequestDTO>, LoginRequestValidation>();
        services.AddScoped<IValidator<RefreshTokenRequestDTO>, RefreshTokenValidation>();
        services.AddScoped<IValidator<ComplaintCreateRequestDTO>, CreateComplaintValidator>();
        services.AddScoped<IValidator<ManagerAddCommentRequestDTO>, ManagerAddCommentValidator>();
        services.AddScoped<IValidator<ComplaintUpdateRequestDTO>, ComplaintUpdateValidator>();


        return services;
    }
    
    
}