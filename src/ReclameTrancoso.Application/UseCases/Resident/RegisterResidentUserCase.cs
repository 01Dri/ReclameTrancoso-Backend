using Application.Mappers;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Resident;
using FluentValidation;
using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;
using ReclameTrancoso.Domain.Interfaces.Transactions;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Resident
{
    
    // Criar Os mappers, salvar resident no banco e inserir nas tabelas intermediarias
    public class RegisterResidentUserCase : IUseCaseHandler<ResidentRegisterRequestDTO, ResidentRegisterResponseDTO>
    {
        private readonly IValidator<ResidentRegisterRequestDTO> _validator;
        private readonly IResidentRepository _residentRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBuildingResidentsRepository _buildingResidentsRepository;
        private readonly IApartmentsResidentsRepository _apartmentsResidentsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterResidentUserCase(IValidator<ResidentRegisterRequestDTO> validator, IResidentRepository residentRepository, IBuildingRepository buildingRepository, IApartmentRepository apartmentRepository, IBuildingResidentsRepository buildingResidentsRepository, IApartmentsResidentsRepository apartmentsResidentsRepository, IUserRepository userRepository, IPasswordEncoder passwordEncoder, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _residentRepository = residentRepository;
            _buildingRepository = buildingRepository;
            _apartmentRepository = apartmentRepository;
            _buildingResidentsRepository = buildingResidentsRepository;
            _apartmentsResidentsRepository = apartmentsResidentsRepository;
            _userRepository = userRepository;
            _passwordEncoder = passwordEncoder;
            _unitOfWork = unitOfWork;
        }


        public async Task<ResidentRegisterResponseDTO> Handle(ResidentRegisterRequestDTO request, CancellationToken cancellationToken)
        {
            await this._validator.ValidateAndThrowAsync(request, cancellationToken);
            _unitOfWork.Begin();
            try
            {
                var resident = request.ToEntity();
                var user = resident.User;
                var building = await _buildingRepository.GetByIdAsync(request.BuildingId);
                var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId);
                await SaveResidentAndUser(resident, user);
                await SaveBuildingResidentsAndApartmentResidents(apartment, building, resident);
                _unitOfWork.Commit();
                return resident.ToRegisterUseCaseResult();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw new FailedToSaveEntityException("Failed to save resident");
            }
        }

        private async Task SaveResidentAndUser(Domain.Models.Resident resident, User user)
        {
            await _residentRepository.SaveAsync(resident);
            user.ResidentId = resident.Id;
            user.Password = await this._passwordEncoder.HashPasswordAsync(user.Password);
            await _userRepository.SaveAsync(user);
        }

        private async Task SaveBuildingResidentsAndApartmentResidents(Apartment apartment, Building building, Domain.Models.Resident resident)
        {
            var buildingResidents = new BuildingResident() { Building = building, Resident = resident };
            var apartmentsResidents = new ApartmentResident() { Apartment = apartment, Resident = resident };

            await _buildingResidentsRepository.SaveAsync(buildingResidents);
            await _apartmentsResidentsRepository.SaveAsync(apartmentsResidents);

        }
    }
    
    
}