using Application.UseCases.Resident;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Resident;
using FluentValidation;
using Moq;
using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Exceptions.Exceptions;
using Xunit;

namespace ReclameTrancoso.Tests.Unit.UseCases;

public class RegisterResidentUseCaseTests
{
    private readonly Mock<IValidator<ResidentRegisterRequestDTO>> _validator;
    private readonly Mock<IResidentRepository> _residentRepository;
    private readonly Mock<IBuildingRepository> _buildingRepository;
    private readonly Mock<IApartmentRepository> _apartmentRepository;
    private readonly Mock<IBuildingResidentsRepository> _buildingResidentsRepository;
    private readonly Mock<IApartmentsResidentsRepository> _apartmentsResidentsRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IPasswordEncoder> _passwordEncoder;
    private readonly Mock<IUnitOfWork> _unitOfWork;


    private RegisterResidentUserCase _userCase;

    public RegisterResidentUseCaseTests()
    {
        _validator = new Mock<IValidator<ResidentRegisterRequestDTO>>();
        _residentRepository = new Mock<IResidentRepository>();
        _buildingRepository = new Mock<IBuildingRepository>();
        _apartmentRepository = new Mock<IApartmentRepository>();
        _buildingResidentsRepository = new Mock<IBuildingResidentsRepository>();
        _apartmentsResidentsRepository = new Mock<IApartmentsResidentsRepository>();
        _userRepository = new Mock<IUserRepository>();
        _passwordEncoder = new Mock<IPasswordEncoder>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _userCase = new RegisterResidentUserCase
        (
            _validator.Object,
            _residentRepository.Object,
            _buildingRepository.Object,
            _apartmentRepository.Object,
            _buildingResidentsRepository.Object,
            _apartmentsResidentsRepository.Object,
            _userRepository.Object,
            _passwordEncoder.Object,
            _unitOfWork.Object
        );
    }

    [Fact]
    public async Task Handle_ThrowsException_ValidationFails_RollbackCalled()
    {
        var request = new ResidentRegisterRequestDTO();
        _buildingRepository
            .Setup(v => v.GetByIdAsync(It.IsAny<long?>()))
            .ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<FailedToSaveEntityException>(() =>
            _userCase.Handle(request, CancellationToken.None));

        _unitOfWork.Verify(u => u.Rollback(), Times.Once);
        _unitOfWork.Verify(u => u.Commit(), Times.Never);
        _unitOfWork.Verify(u => u.Begin(), Times.Once);

    }
    
    [Fact]
    public async Task Handle_Successfully()
    {
        var request = new ResidentRegisterRequestDTO();

        _buildingRepository.Setup(x => x.GetByIdAsync(It.IsAny<long?>()))
            .ReturnsAsync(It.IsAny<Building>());
        
        _apartmentRepository.Setup(x => x.GetByIdAsync(It.IsAny<long?>()))
            .ReturnsAsync(It.IsAny<Apartment>());

        await _userCase.Handle(request, new CancellationToken());

        _unitOfWork.Verify(u => u.Rollback(), Times.Never);
        _unitOfWork.Verify(u => u.Commit(), Times.Once);
        _unitOfWork.Verify(u => u.Begin(), Times.Once);
        
        _residentRepository.Verify( x => x.SaveAsync(It.IsAny<Resident>()), Times.Once());
        _userRepository.Verify( x => x.SaveAsync(It.IsAny<User>()), Times.Once());
        _passwordEncoder.Verify(x => x.HashPasswordAsync(It.IsAny<string>()), Times.Once);
        
        _buildingResidentsRepository.Verify(x => x.SaveAsync(It.IsAny<BuildingResident>()), Times.Once);
        _apartmentsResidentsRepository.Verify(x => x.SaveAsync(It.IsAny<ApartmentResident>()), Times.Once);

    }
}