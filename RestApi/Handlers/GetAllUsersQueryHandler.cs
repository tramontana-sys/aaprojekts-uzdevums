using MediatR;
using Microsoft.EntityFrameworkCore;
using RestApi.Database;
using RestApi.Models;
using RestApi.Queries;

namespace RestApi.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly DatabaseContext database;

    public GetAllUsersQueryHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await this.database.Users.ToListAsync(cancellationToken: cancellationToken);
    }
}