﻿using MediatR;
using RestApi.Database;
using RestApi.Models;

namespace RestApi.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly DatabaseContext database;

    public CreateUserCommandHandler(DatabaseContext database)
    {
        this.database = database;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = CreateUser(request);

        this.database.Users.Add(user);
        await this.database.SaveChangesAsync(cancellationToken);

        return user;
    }

    private static User CreateUser(CreateUserCommand request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email,
            Verified = request.Verified
        };
    }
}