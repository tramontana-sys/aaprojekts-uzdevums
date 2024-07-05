using FluentAssertions;
using RestApi.Commands;

namespace RestApi.Tests.HandlerTest;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateUser()
    {
        // Arrange
        var context = DatabaseContextFactory.CreateInMemoryDatabaseContext();
        var handler = new CreateUserCommandHandler(context);

        var command = new CreateUserCommand
        {
            Name = "Hamster One",
            Email = "hamster@one.com",
            Verified = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var user = await context.Users.FindAsync(result.Id);
        user.Should().NotBeNull();
        user.Name.Should().Be("Hamster One");
        user.Email.Should().Be("hamster@one.com");
        user.Verified.Should().BeTrue();
    }
}