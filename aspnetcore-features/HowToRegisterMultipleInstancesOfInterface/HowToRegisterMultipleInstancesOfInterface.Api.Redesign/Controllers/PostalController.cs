using HowToRegisterMultipleInstancesOfInterface.Api.Redesign.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HowToRegisterMultipleInstancesOfInterface.Api.Redesign.Controllers;

[ApiController]
[Route("[controller]")]
public class PostalController : ControllerBase
{
    private readonly IFulfillTickets _processor;

    public PostalController(IFulfillPostalTickets processor)
    {
        _processor = processor;
    }

    [HttpPost()]
    public string Post([FromBody] string requestId)
    {
        return _processor.Fulfill(requestId);
    }
}