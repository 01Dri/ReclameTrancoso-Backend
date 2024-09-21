using Application.UseCases.Auth;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using FluentValidation;
using Moq;
using ReclameTrancoso.Domain.Interfaces.Auth;
using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Exceptions.Exceptions;
using Xunit;

namespace ReclameTrancoso.Tests.Unit.UseCases;

public class LoginUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IPasswordEncoder> _passwordEncoder;
    private readonly Mock<ITokenService<User, TokenResponseDTO>> _tokenService;
    private readonly Mock<IValidator<LoginRequestDTO>> _validator;
    private readonly Mock<ITokenRepository> _tokenRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly LoginUseCase _useCase;

    public LoginUseCaseTests()
    {
        _userRepository = new Mock<IUserRepository>();
        _passwordEncoder = new Mock<IPasswordEncoder>();
        _tokenService = new Mock<ITokenService<User, TokenResponseDTO>>();
        _validator = new Mock<IValidator<LoginRequestDTO>>();
        _tokenRepository = new Mock<ITokenRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _useCase = new LoginUseCase
            (
                _userRepository.Object,
                _passwordEncoder.Object,
                _tokenService.Object,
                _validator.Object,
                _tokenRepository.Object,
                _unitOfWork.Object
            );
    }

    [Fact]
    public async Task Test_Successfully_WithCPF()
    {
        var mockLoginRequest = new LoginRequestDTO()
        {
            Cpf = "282.772.680-76",
            Password = "SenhaTestee"
        };
        _userRepository.Setup(x
            => x.GetByCPFAsync(mockLoginRequest.Cpf)).ReturnsAsync(new User()
        {
            Password = "SenhaTestee",
            Cpf = "282.772.680-76",
            Id = 12
        });
        _passwordEncoder.Setup(x =>
            x.IsValidAsync(mockLoginRequest.Password,
                It.IsAny<string>())).ReturnsAsync(true);
        _tokenService.Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns(new TokenResponseDTO(1,"token", "refreshToken", DateTime.Now, DateTime.Now));

        await _useCase.Handle(mockLoginRequest, new CancellationToken());
        
        _unitOfWork.Verify(x => x.Begin(), Times.Once);
        _unitOfWork.Verify(x => x.Commit(), Times.Once);
        _unitOfWork.Verify(x => x.Rollback(), Times.Never);
        _tokenRepository.Verify(x => x.SaveAsync(It.IsAny<TokenEntity>()), Times.Once);
        _tokenService.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);

    }
    
    [Fact]
    public async Task Test_Successfully_WithEmail()
    {
        var mockLoginRequest = new LoginRequestDTO()
        {
            Email = "diego@gmail.com",
            Password = "SenhaTestee"
        };
        
        _userRepository.Setup(x
            => x.GetByEmailAsync(mockLoginRequest.Email)).ReturnsAsync(new User()
        {
            Password = "SenhaTestee",
            Cpf = "282.772.680-76",
            Id = 12
        });
        _passwordEncoder.Setup(x =>
            x.IsValidAsync(mockLoginRequest.Password,
                It.IsAny<string>())).ReturnsAsync(true);
        _tokenService.Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns(new TokenResponseDTO(1,"token", "refreshToken", DateTime.Now, DateTime.Now));

        await _useCase.Handle(mockLoginRequest, new CancellationToken());
        
        _unitOfWork.Verify(x => x.Begin(), Times.Once);
        _unitOfWork.Verify(x => x.Commit(), Times.Once);
        _unitOfWork.Verify(x => x.Rollback(), Times.Never);
        _tokenRepository.Verify(x => x.SaveAsync(It.IsAny<TokenEntity>()), Times.Once);
        _tokenService.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);

    }

    [Fact]
    public async Task Test_ShouldToReturn_UserNotFoundByCPFException()
    {
        var mockLoginRequest = new LoginRequestDTO()
        {
            Cpf = "130.482.459-44",
            Password = "SenhaTestee"
        };

        _userRepository.Setup(x
            => x.GetByCPFAsync(mockLoginRequest.Cpf)).ReturnsAsync((User?)null);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _useCase.Handle(mockLoginRequest, new CancellationToken());
        });
        Assert.Equal("Usuário não encontrado", exception.Message);
        _unitOfWork.Verify(x => x.Begin(), Times.Once());
        _unitOfWork.Verify(x => x.Rollback(), Times.Once());
        _unitOfWork.Verify(x => x.Commit(), Times.Never);

    }
    
    [Fact]
    public async Task Test_ShouldToReturn_UserNotFoundByEmailException()
    {
        var mockLoginRequest = new LoginRequestDTO()
        {
            Email = "diego@gmail.com",
            Password = "SenhaTestee"
        };

        _userRepository.Setup(x
            => x.GetByEmailAsync(mockLoginRequest.Email)).ReturnsAsync((User?)null);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _useCase.Handle(mockLoginRequest, new CancellationToken());
        });
        Assert.Equal("Usuário não encontrado", exception.Message);
        _unitOfWork.Verify(x => x.Begin(), Times.Once());
        _unitOfWork.Verify(x => x.Rollback(), Times.Once());
        _unitOfWork.Verify(x => x.Commit(), Times.Never);

    }

    [Fact]
    public async Task Test_ShouldToReturn_PasswordInvalidException()
    {
        var mockLoginRequest = new LoginRequestDTO()
        {
            Cpf = "130.482.459-44",
            Password = "SenhaTestee"
        };

        _userRepository.Setup(x
            => x.GetByCPFAsync(mockLoginRequest.Cpf)).ReturnsAsync(new User()
        {
            Password = "SenhaTesteeeDiferente"
        });

        _passwordEncoder.Setup(x
            => x.IsValidAsync(mockLoginRequest.Password,
                It.IsAny<string>())).ReturnsAsync(false);

        var exception = await Assert.ThrowsAsync<InvalidPasswordException>(async () =>
        {
            await _useCase.Handle(mockLoginRequest, new CancellationToken());
        });
        Assert.Equal("Senha incorreta", exception.Message);
        
        _unitOfWork.Verify(x => x.Begin(), Times.Once());
        _unitOfWork.Verify(x => x.Rollback(), Times.Once());
        _unitOfWork.Verify(x => x.Commit(), Times.Never);


    }
    
    
}