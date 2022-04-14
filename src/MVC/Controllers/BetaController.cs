using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace MVC.Controllers;

[ApiController]
[FeatureGate(FeatureFlags.Beta)]
public class BetaController : ControllerBase
{
    [HttpGet("[controller]")]
    public IActionResult Get() => Ok("Hi Beta user!");
}
