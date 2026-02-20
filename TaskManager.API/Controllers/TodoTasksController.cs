using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands;
using TaskManager.Application.Handlers;
using TaskManager.Application.Queries;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController(GetAllTodoTasksQueryHandler getAllHandler,
                                GetTodoTaskByIdQueryHandler getByIdHandler,
                                CreateTodoTaskHandler createHandler) : ControllerBase
{
    private readonly GetAllTodoTasksQueryHandler _getAllHandler = getAllHandler;
    private readonly GetTodoTaskByIdQueryHandler _getByIdHandler = getByIdHandler;
    private readonly CreateTodoTaskHandler _createHandler = createHandler;

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
    public async Task<IActionResult> Create(CreateTodoTaskCommand command,
                                            CancellationToken cancellationToken)
    {
        var id = await _createHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
}
