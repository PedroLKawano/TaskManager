using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Queries;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController(GetAllTodoTasksQueryHandler getAllHandler,
                                GetTodoTaskByIdQueryHandler getByIdHandler) : ControllerBase
{
    private readonly GetAllTodoTasksQueryHandler _getAllHandler = getAllHandler;
    private readonly GetTodoTaskByIdQueryHandler _getByIdHandler = getByIdHandler;

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
}
