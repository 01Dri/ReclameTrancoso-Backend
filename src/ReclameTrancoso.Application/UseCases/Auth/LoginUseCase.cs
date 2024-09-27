using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using FluentValidation;
using ReclameTrancoso.Domain.Interfaces.Auth;
using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Auth;

public class LoginUseCase : IUseCaseHandler<LoginRequestDTO, TokenResponseDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly ITokenService<User, TokenResponseDTO> _tokenService;
    private readonly IValidator<LoginRequestDTO> _validator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IManagerRepository _managerRepository;
    private readonly IResidentRepository _residentRepository;

    public LoginUseCase(IUserRepository userRepository, IPasswordEncoder passwordEncoder, ITokenService<User, TokenResponseDTO> tokenService, IValidator<LoginRequestDTO> validator, ITokenRepository tokenRepository, IUnitOfWork unitOfWork, IManagerRepository managerRepository, IResidentRepository residentRepository)
    {
        _userRepository = userRepository;
        _passwordEncoder = passwordEncoder;
        _tokenService = tokenService;
        _validator = validator;
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
        _managerRepository = managerRepository;
        _residentRepository = residentRepository;
    }


    public async Task<TokenResponseDTO> Handle(LoginRequestDTO request, CancellationToken cancellationToken)
    {
        await this._validator.ValidateAndThrowAsync(request, cancellationToken);
        _unitOfWork.Begin();
        try
        {
            var user = await this._userRepository.GetByCPFAsync(request.Cpf) ??
                       await this._userRepository.GetByEmailAsync(request.Email) ??
                       throw new NotFoundException("Usuário não encontrado");
            
            
            if (!await _passwordEncoder.IsValidAsync(request.Password, user.Password))
            {
                throw new InvalidPasswordException("Senha incorreta");
            }

            var tokenResponse = _tokenService.GenerateToken(user);
            var userToken = user.Token ?? null;
            
            if (userToken != null)
            {
                this.UpdateToken(userToken, tokenResponse);
            }
            else
            {
                userToken = new TokenEntity()
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    AccessTokenExpiresAt = tokenResponse.AccessTokenExpiresAt,
                    RefreshTokenExpiresAt = tokenResponse.RefreshTokenExpiresAt,
                    UserId = user.Id,
                    User = user
                };
            }
            await _tokenRepository.SaveAsync(userToken);

            long? entityId;

            entityId = await _managerRepository.ExistByUserIdAsync(user.Id);
            tokenResponse.EntityId = new 
                EntityIdResponseDTO(entityId.HasValue, entityId ??
                                    await _residentRepository.ExistByUserIdAsync(user.Id));

            _unitOfWork.Commit();
            
            return tokenResponse;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    private void  UpdateToken(TokenEntity tokenEntity, TokenResponseDTO newToken)
    {
        tokenEntity.AccessToken = newToken.AccessToken;
        tokenEntity.RefreshToken = newToken.RefreshToken;
        tokenEntity.AccessTokenExpiresAt = newToken.AccessTokenExpiresAt;
        tokenEntity.RefreshTokenExpiresAt = newToken.RefreshTokenExpiresAt;
    }

}