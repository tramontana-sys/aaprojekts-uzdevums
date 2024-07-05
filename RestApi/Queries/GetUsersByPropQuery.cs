using MediatR;
using RestApi.Models;

namespace RestApi.Queries;

public class GetUsersByPropQuery : IRequest<List<User>>
{
    public string Property { get; set; }
    public string Value { get; set; }
}