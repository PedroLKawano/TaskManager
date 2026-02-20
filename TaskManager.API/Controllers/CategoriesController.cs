using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Handlers;
using TaskManager.Application.Queries;
using TaskManager.Domain.Commands;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(GetAllCategoriesQueryHandler getAllHandler,
                                  GetCategoryByIdQueryHandler getByIdHandler,
                                  CreateCategoryHandler createHandler) : ControllerBase
{
    private readonly GetAllCategoriesQueryHandler _getAllHandler = getAllHandler;
    private readonly GetCategoryByIdQueryHandler _getByIdHandler = getByIdHandler;
    private readonly CreateCategoryHandler _createHandler = createHandler;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _getAllHandler.HandleAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getByIdHandler.HandleAsync(id, cancellationToken);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command,
                                            CancellationToken cancellationToken)
    {
        var id = await _createHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}
