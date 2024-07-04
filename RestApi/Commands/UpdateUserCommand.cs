using MediatR;

namespace RestApi.Commands;

public class UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public bool Verified { get; set; }
}