using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Controllers.Home;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    [HttpGet]
    public ObjectResult Index()
    {
        var Response = new
        {
            Name = "Bonsai API",
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        };
        return new ObjectResult(Response);
    }
}

