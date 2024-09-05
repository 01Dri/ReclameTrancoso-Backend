using Domain.Interfaces;
using Domain.Models.DTOs.Resident;
using FluentValidation;

namespace Application.UseCases.Resident
{
    public class RegisterResidentUserCase : IUseCaseHandler<ResidentRegisterRequestDTO, ResidentRegisterResponseDTO>
    {
        private readonly IValidator<ResidentRegisterRequestDTO> _validator;

        public RegisterResidentUserCase(IValidator<ResidentRegisterRequestDTO> validator)
        {
            _validator = validator;
        }

        public async Task<ResidentRegisterResponseDTO> Handle(ResidentRegisterRequestDTO request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return new ResidentRegisterResponseDTO();
        }
    }
}