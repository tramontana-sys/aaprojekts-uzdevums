using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApi.Commands;
using RestApi.Queries;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    public UsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return this.Ok(await this.mediator.Send(new GetAllUsersQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var itemId = await this.mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { Id = itemId }, command);
    }
}