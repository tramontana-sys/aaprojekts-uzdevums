using MediatR;

namespace RestApi.Commands;

public class DeleteUserCommand(int id) : IRequest<Unit>
{
    public int Id { get; set; }
}