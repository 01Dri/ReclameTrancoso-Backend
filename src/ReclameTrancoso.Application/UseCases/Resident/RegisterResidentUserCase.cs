using Application.Mappers;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Resident;
using FluentValidation;

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

        public RegisterResidentUserCase(IValidator<ResidentRegisterRequestDTO> validator, IResidentRepository residentRepository, IBuildingRepository buildingRepository, IApartmentRepository apartmentRepository, IBuildingResidentsRepository buildingResidentsRepository, IApartmentsResidentsRepository apartmentsResidentsRepository, IUserRepository userRepository)
        {
            _validator = validator;
            _residentRepository = residentRepository;
            _buildingRepository = buildingRepository;
            _apartmentRepository = apartmentRepository;
            _buildingResidentsRepository = buildingResidentsRepository;
            _apartmentsResidentsRepository = apartmentsResidentsRepository;
            _userRepository = userRepository;
        }

        public async Task<ResidentRegisterResponseDTO> Handle(ResidentRegisterRequestDTO request, CancellationToken cancellationToken)
        {
            await this._validator.ValidateAndThrowAsync(request);
            var resident = request.ToEntity();
            var user = resident.User;
            var building = await _buildingRepository.GetByIdAsync(request.BuildingId);
            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId);
            
            await _residentRepository.SaveAsync(resident);
            user.ResidentId = resident.Id;
            await _userRepository.SaveAsync(user);

            var buildingResidents = new BuildingResident() { Building = building, Resident = resident };
            var apartmentsResidents = new ApartmentResident() { Apartment = apartment, Resident = resident };

            await _buildingResidentsRepository.SaveAsync(buildingResidents);
            await _apartmentsResidentsRepository.SaveAsync(apartmentsResidents);

            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return new ResidentRegisterResponseDTO();
        }
    }
}