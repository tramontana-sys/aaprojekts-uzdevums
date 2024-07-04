using MediatR;
using RestApi.Models;

namespace RestApi.Queries;

public class GetAllUsersQuery : IRequest<List<User>>
{
}