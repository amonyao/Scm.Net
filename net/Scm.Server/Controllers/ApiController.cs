using Microsoft.AspNetCore.Mvc;

namespace Com.Scm.Controllers;

/// <summary>
/// Webʹ�õĻ���Controller
/// </summary>
[ApiController]
//[Authorize("Web")]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "Scm")]
public class ApiController : ControllerBase
{
}