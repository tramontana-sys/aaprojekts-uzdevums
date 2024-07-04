using MediatR;
using RestApi.Models;

namespace RestApi.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Name { get; set; }
    
    public string Email { get; set; }

    public bool Verified { get; set; }
}