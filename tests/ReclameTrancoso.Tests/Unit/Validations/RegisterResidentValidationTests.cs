using Application.Validations.Resident;
using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace ReclameTrancoso.Tests.Unit.Validations;

public class RegisterResidentValidationTests
{
    private readonly Mock<IBuildingRepository> _mockBuildingRepository;
    private readonly Mock<IApartmentRepository> _mockApartmentRepository;
    private readonly Mock<IResidentRepository> _mockResidentRepository;
    private readonly Mock<IApartmentsResidentsRepository> _mockApartmentsResidentsRepository;
    private readonly RegisterResidentRequestValidation _validator;

    public RegisterResidentValidationTests()
    {
        _mockBuildingRepository = new Mock<IBuildingRepository>();
        _mockApartmentRepository = new Mock<IApartmentRepository>();
        _mockResidentRepository = new Mock<IResidentRepository>();
        _mockApartmentsResidentsRepository = new Mock<IApartmentsResidentsRepository>();

        _validator = new RegisterResidentRequestValidation
        (
            _mockBuildingRepository.Object,
            _mockApartmentRepository.Object,
            _mockResidentRepository.Object,
            _mockApartmentsResidentsRepository.Object
        );
    }

    [Fact]
    public async Task Test_Successfully()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique",
            Email = "diego@gmail.com",
            Cpf = "868.115.090-15",
            Password = "SenhaTeste123",
            BuildingId = 1,
            ApartmentId = 1
        };
        _mockApartmentRepository.Setup(x =>
            x.ExistsByIdAsync(mockRegisterResidentDto.ApartmentId)).ReturnsAsync(true);
        _mockBuildingRepository.Setup(x => 
            x.ExistsByIdAsync(mockRegisterResidentDto.BuildingId)).ReturnsAsync(true);
        _mockApartmentsResidentsRepository.Setup(x =>
            x.AlreadyExistOwnerApartmentAsync(mockRegisterResidentDto.ApartmentId)).ReturnsAsync(false);
        
        _mockResidentRepository.Setup(x => 
            x.AnyByCPFAsync(It.IsAny<string>())).ReturnsAsync(false);
        _mockResidentRepository.Setup(x =>
            x.AnyByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);


        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Test_Name_ShouldReturn_NotNullMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Email = "diego@gmail.com",
            Cpf = "868.115.090-15",
            BuildingId = 1,
            ApartmentId = 1
        };

        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Nome não pode ser vazio.");
    }
    
    [Fact]
    public async Task Test_Name_ShouldReturn_MinimumLengthMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "die",
            Email = "diego@gmail.com",
            Cpf = "868.115.090-15",
            BuildingId = 1,
            ApartmentId = 1
        };

        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Nome deve ter no minimo 5 caracteres.");
    }
    
    [Fact]
    public async Task Test_Name_ShouldReturn_MaximumLengthMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes Pereira Da Silva Fernando de Albuquerque",
            Email = "diego@gmail.com",
            Cpf = "868.115.090-15",
            BuildingId = 1,
            ApartmentId = 1
        };

        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Nome deve ter no maximo 50 caracteres.");
    }
    
    [Fact]
    public async Task Test_Email_ShouldReturn_InvalidEmailMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diego.com.br",
            Cpf = "868.115.090-15",
            BuildingId = 1,
            ApartmentId = 1
        };

        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email deve ser válido.");
    }
    
    [Fact]
    public async Task Test_CPF_ShouldReturn_InvalidLengthCPFMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "13048245944",
            BuildingId = 1,
            ApartmentId = 1
        };

        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Cpf)
            .WithErrorMessage("CPF deve ter 14 caracteres, Ex: 868.115.090-15");
    }
    
    [Fact]
    public async Task Test_CPF_ShouldReturn_AlreadyExistMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            BuildingId = 1,
            ApartmentId = 1
        };

        _mockResidentRepository.Setup(x => x.AnyByCPFAsync(mockRegisterResidentDto.Cpf))
            .ReturnsAsync(true);
        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Cpf)
            .WithErrorMessage("CPF já cadastrado");
    }
    
    [Fact]
    public async Task Test_Password_ShouldReturn_MinimumLengthMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "12345",
            BuildingId = 1,
            ApartmentId = 1
        };

        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Senha deve ter no mínimo 6 caracteres");
    }
    
    [Fact]
    public async Task Test_Password_ShouldReturn_InvalidFormatMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123",
            BuildingId = 1,
            ApartmentId = 1
        };  

        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Senha deve conter pelo menos uma letra maiúscula");
    }
    
    [Fact]
    public async Task Test_BuildingID_ShouldReturn_NotNullMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123",
            ApartmentId = 1
        };  

        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.BuildingId)
            .WithErrorMessage("ID do bloco é obrigatório");
    }
    
    [Fact]
    public async Task Test_BuildingID_ShouldReturn_NotExistMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123",
            BuildingId = 1,
            ApartmentId = 1
        };

        _mockBuildingRepository.Setup(x =>
            x.ExistsByIdAsync(mockRegisterResidentDto.BuildingId)).ReturnsAsync(false);
        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.BuildingId)
            .WithErrorMessage("Bloco não existe");
    }
    
    [Fact]
    public async Task Test_ApartmentID_ShouldReturn_NotNullMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123"
        };  

        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.ApartmentId)
            .WithErrorMessage("ID do apartamento é obrigatório");
    }
    
    [Fact]
    public async Task Test_ApartmentID_ShouldReturn_NotExistMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123",
            BuildingId = 1,
            ApartmentId = 1
        };

        _mockApartmentsResidentsRepository.Setup(x =>
            x.AlreadyExistOwnerApartmentAsync(mockRegisterResidentDto.ApartmentId)).ReturnsAsync(false);
        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.ApartmentId)
            .WithErrorMessage("Apartamento não existe");
    }
    
    [Fact]
    public async Task Test_ApartmentID_ShouldReturn_AlreadyOwnerApartmentMessageError()
    {
        var mockRegisterResidentDto = new ResidentRegisterRequestDTO()
        {
            Name = "Diego Henrique Magahaes",
            Email = "diegohenrique@gmail.com",
            Cpf = "868.115.090-15",
            Password = "carro123",
            BuildingId = 1,
            ApartmentId = 1
        };

        _mockApartmentsResidentsRepository.Setup(x =>
            x.AlreadyExistOwnerApartmentAsync(mockRegisterResidentDto.ApartmentId)).ReturnsAsync(true);
        
        _mockApartmentRepository.Setup(x =>
            x.ExistsByIdAsync(mockRegisterResidentDto.BuildingId)).ReturnsAsync(true);
        
        var result = await _validator.TestValidateAsync(mockRegisterResidentDto);
        result.ShouldHaveValidationErrorFor(x => x.ApartmentId)
            .WithErrorMessage("Apartamento já tem um proprietário");
    }
   
}

