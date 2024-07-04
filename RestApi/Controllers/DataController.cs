using Microsoft.AspNetCore.Mvc;
using RestApi.Data;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    public DataController(DbContext context)
    {
    }
}