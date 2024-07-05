using MediatR;
using RestApi.Commands;
using RestApi.Database;

namespace RestApi.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly DatabaseContext database;

    public DeleteUserCommandHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.database.Users.FindAsync(new object?[] { request.Id },
            cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new Exception($"Not found user with id {request.Id}");
        }

        this.database.Users.Remove(user);
        await this.database.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}