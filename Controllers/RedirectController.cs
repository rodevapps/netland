using Microsoft.AspNetCore.Mvc;

namespace Netland.Controllers;

[ApiController]
[Route("")]
[Route("api")]
public class RedirectController : Controller
{
    public ActionResult Index()
    {
        return RedirectPermanent("/api/clients");
    }
}
