using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Handlers;
using TaskManager.Domain.Commands;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(CreateCategoryHandler handler) : ControllerBase
{
    private readonly CreateCategoryHandler _handler = handler;

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command,
                                            CancellationToken cancellationToken)
    {
        var id = await _handler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}
