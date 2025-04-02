using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    
    [HttpGet]
    public IActionResult GetMessage()
    {
        return Ok(new { message = "Conexión exitosa con .NET!" });
    }
}
