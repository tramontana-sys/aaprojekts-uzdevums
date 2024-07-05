using FluentValidation.TestHelper;
using RestApi.Commands;
using RestApi.Validators;

namespace RestApi.Tests.Validators;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator validator;

    public CreateUserCommandValidatorTests()
    {
        this.validator = new CreateUserCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsNull()
    {
        var command = new CreateUserCommand { Email = "hamster@one.com" };
        var result = this.validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenEmailIsNull()
    {
        var command = new CreateUserCommand { Name = "Hamster One" };
        var result = this.validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void ShouldNotHaveAnyErrorsWhenCommandIsValid()
    {
        var command = new CreateUserCommand { Name = "Hamster One", Email = "hamster@one.com" };
        var result = this.validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}