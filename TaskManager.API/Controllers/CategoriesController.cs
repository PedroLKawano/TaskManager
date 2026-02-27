using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands;
using TaskManager.Application.Handlers.Categories;
using TaskManager.Application.Queries;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(GetAllCategoriesQueryHandler getAllHandler,
                                  GetCategoryByIdQueryHandler getByIdHandler,
                                  CreateCategoryHandler createHandler,
                                  UpdateCategoryHandler updateCategory,
                                  DeleteCategoryHandler deleteHandler) : ControllerBase
{
    private readonly GetAllCategoriesQueryHandler _getAllHandler = getAllHandler;
    private readonly GetCategoryByIdQueryHandler _getByIdHandler = getByIdHandler;
    private readonly CreateCategoryHandler _createHandler = createHandler;
    private readonly UpdateCategoryHandler _updateCategory = updateCategory;
    private readonly DeleteCategoryHandler _deleteHandler = deleteHandler;

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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,
                                            UpdateCategoryCommand command,
                                            CancellationToken cancellationToken)
    {
        await _updateCategory.HandleAsync(id, command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _deleteHandler.HandleAsync(id, cancellationToken);
        return NoContent();
    }
}
