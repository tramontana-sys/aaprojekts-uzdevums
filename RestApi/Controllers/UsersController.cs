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

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of all users.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return this.Ok(await this.mediator.Send(new GetAllUsersQuery()));
    }

    /// <summary>
    /// Retrieves users filtered by a User parameter.
    /// </summary>
    /// <param name="property">Property name of User object to filter by.</param>
    /// <param name="value">Value of the property to filter by.</param>
    /// <returns>A list of users filtered by the specified property value.</returns>
    [HttpGet("GetByProp")]
    public async Task<IActionResult> GetByProp([FromQuery] string property, [FromQuery] string value)
    {
        var query = new GetUsersByPropQuery { Property = property, Value = value };
        var result = await this.mediator.Send(query);
        return Ok(result);
    }


    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="command">Information of the new user.</param>
    /// <returns>The created user.</returns>
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

    /// <summary>
    /// Updates the user with the specified ID.
    /// </summary>
    /// <param name="id">ID of the user to update.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest();
        await this.mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes the user with the specified ID.
    /// </summary>
    /// <param name="id">ID of the user to delete.</param>
    /// <returns>Ok.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await this.mediator.Send(new DeleteUserCommand(id));
        return Ok();
    }

    /// <summary>
    /// Deletes users based on a dynamic property and value.
    /// </summary>
    /// <param name="property">Property name of User object to filter by.</param>
    /// <param name="value">Value of the property to filter by.</param>
    /// <returns>No content if successfully deleted.</returns>
    [HttpDelete("deleteUsersByProp")]
    public async Task<IActionResult> DeleteUsersByProp([FromQuery] string property, [FromQuery] string value)
    {
        var command = new DeleteUserByPropCommand { Property = property, Value = value };
        await this.mediator.Send(command);
        return NoContent();
    }
}