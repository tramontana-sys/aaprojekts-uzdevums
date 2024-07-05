using MediatR;
using Microsoft.EntityFrameworkCore;
using RestApi.Database;

namespace RestApi.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly DatabaseContext database;

    public UpdateUserCommandHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await this.database.Users.FindAsync(new object?[] { request.Id },
            cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new Exception($"Not found user with id {request.Id}");
        }

        //TODO test this
        user.Name = request.Name;
        user.Email = request.Email;
        user.Verified = request.Verified;
        
        this.database.Entry(user).State = EntityState.Modified;
        await this.database.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}