using MediatR;
using Microsoft.EntityFrameworkCore;
using RestApi.Commands;
using RestApi.Database;

namespace RestApi.Handlers;

public class DeleteUsersByPropCommandHandler : IRequestHandler<DeleteUserByPropCommand, bool>
{
    private readonly DatabaseContext database;

    public DeleteUsersByPropCommandHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<bool> Handle(DeleteUserByPropCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await this.database.Users
                .Where(u => EF.Property<string>(u, request.Property) == request.Value)
                .ToListAsync(cancellationToken: cancellationToken);

            if (users.Count != 0)
            {
                this.database.Users.RemoveRange(users);
                await this.database.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong while deleting users!");
        }
    }
}