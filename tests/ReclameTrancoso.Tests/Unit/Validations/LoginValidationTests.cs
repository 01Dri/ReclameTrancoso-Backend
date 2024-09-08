using Application.Validations.Auth;
using Domain.Models.DTOs.Auth;
using FluentValidation.TestHelper;
using Xunit;

namespace ReclameTrancoso.Tests.Unit.Validations
{
    public class LoginValidationTests
    {
        private readonly LoginRequestValidation _validator;

        public LoginValidationTests()
        {
            _validator = new LoginRequestValidation();
        }

        [Fact]
        public async Task Test_Successfully_WithCPF()
        {
            var validDto = new LoginRequestDTO
            {
                Cpf = "868.115.090-15",
                Password = "ValidPassword1"
            };

            var result = await _validator.TestValidateAsync(validDto);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Fact]
        public async Task Test_Successfully_WithEmail()
        {
            var validDto = new LoginRequestDTO
            {
                Email = "diego@gmail.com",
                Password = "ValidPassword1"
            };

            var result = await _validator.TestValidateAsync(validDto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Cpf_ShouldHaveValidationError_WhenInvalid()
        {
            var invalidCpfDto = new LoginRequestDTO
            {
                Cpf = "123.456.789-00",
                Password = "ValidPassword1"
            };

            var result = await _validator.TestValidateAsync(invalidCpfDto);

            result.ShouldHaveValidationErrorFor(x => x.Cpf)
                .WithErrorMessage("CPF inválido");
        }

        [Fact]
        public async Task Password_ShouldHaveValidationError_WhenTooShort()
        {
            var shortPasswordDto = new LoginRequestDTO
            {
                Cpf = "868.115.090-15",
                Password = "Short" 
            };

            var result = await _validator.TestValidateAsync(shortPasswordDto);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Senha deve ter no mínimo 6 caracteres");
        }

        [Fact]
        public async Task Password_ShouldHaveValidationError_WhenNoUppercase()
        {
            var noUppercasePasswordDto = new LoginRequestDTO
            {
                Cpf = "868.115.090-15",
                Password = "password1" 
            };

            var result = await _validator.TestValidateAsync(noUppercasePasswordDto);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Senha deve conter pelo menos uma letra maiúscula");
        }

        [Fact]
        public async Task Cpf_And_Email_ShouldHaveValidationError_WhenBothProvided()
        {
            var dtoWithBoth = new LoginRequestDTO
            {
                Cpf = "868.115.090-15",
                Email = "test@example.com",
                Password = "ValidPassword1"
            };

            var result = await _validator.TestValidateAsync(dtoWithBoth);

            result.ShouldHaveValidationErrorFor("Cpf-Email")
                .WithErrorMessage("Forneça apenas uma forma de login, CPF ou Email");
        }
        


        [Fact]
        public async Task Email_ShouldHaveValidationError_WhenInvalid()
        {
            var invalidEmailDto = new LoginRequestDTO
            {
                Email = "invalid-email",
                Password = "ValidPassword1"
            };

            var result = await _validator.TestValidateAsync(invalidEmailDto);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email deve ser válido.");
        }
    }
}
