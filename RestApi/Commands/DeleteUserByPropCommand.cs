using MediatR;

namespace RestApi.Commands;

public class DeleteUserByPropCommand : IRequest<bool>
{
    public string Property { get; set; }
    public string Value { get; set; }
}