using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using FluentValidation;
using ReclameTrancoso.Domain.Interfaces.Auth;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Auth;

public class RefreshTokenUseCase : IUseCaseHandler<RefreshTokenRequestDTO, TokenResponseDTO>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenService<User, TokenResponseDTO> _tokenService;
    private readonly IValidator<RefreshTokenRequestDTO> _validator;

    public RefreshTokenUseCase(ITokenRepository tokenRepository, ITokenService<User, TokenResponseDTO> tokenService, IValidator<RefreshTokenRequestDTO> validator)
    {
        _tokenRepository = tokenRepository;
        _tokenService = tokenService;
        _validator = validator;
    }


    public async Task<TokenResponseDTO> Handle(RefreshTokenRequestDTO? request, CancellationToken cancellationToken)
    {
        await this._validator.ValidateAndThrowAsync(request);
        var tokenEntity = await _tokenRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (tokenEntity != null)
        {
            var tokenResponse = _tokenService.GenerateToken(tokenEntity.User);
            tokenEntity.AccessToken = tokenResponse.AccessToken;
            tokenEntity.RefreshToken = tokenResponse.RefreshToken;
            tokenEntity.AccessTokenExpiresAt = tokenResponse.AccessTokenExpiresAt;
            tokenEntity.RefreshTokenExpiresAt = tokenResponse.RefreshTokenExpiresAt;
            await _tokenRepository.SaveAsync(tokenEntity);
            return tokenResponse;

        }

        throw new FailedToRefreshTokenException("Refresh token inválido");
    }
}