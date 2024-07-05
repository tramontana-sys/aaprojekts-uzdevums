using MediatR;
using Microsoft.EntityFrameworkCore;
using RestApi.Database;
using RestApi.Models;
using RestApi.Queries;

namespace RestApi.Handlers;

public class GetUsersByPropQueryHandler : IRequestHandler<GetUsersByPropQuery, List<User>>
{
    private readonly DatabaseContext database;

    public GetUsersByPropQueryHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<List<User>> Handle(GetUsersByPropQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> query = this.database.Users;

        switch (request.Property.ToLower())
        {
            case "name":
                query = query.Where(u => u.Name == request.Value);
                break;
            case "email":
                query = query.Where(u => u.Email == request.Value);
                break;
            case "verified":
                query = query.Where(u => u.Verified == bool.Parse(request.Value));
                break;
            default:
                return await query.ToListAsync(cancellationToken);
        }

        return await query.ToListAsync(cancellationToken);
    }
}