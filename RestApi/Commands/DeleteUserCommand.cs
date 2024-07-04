using MediatR;

namespace RestApi.Commands;

public class DeleteUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
}