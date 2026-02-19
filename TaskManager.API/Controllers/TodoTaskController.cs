using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Handlers;
using TaskManager.Domain.Commands;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController(CreateTodoTaskHandler createHandler,
                                StartTodoTaskHandler startHandler,
                                CompleteTodoTaskHandler completeHandler,
                                CancelTodoTaskHandler cancelHandler) : ControllerBase
{
    private readonly CreateTodoTaskHandler _createHandler = createHandler;
    private readonly StartTodoTaskHandler _startHandler = startHandler;
    private readonly CompleteTodoTaskHandler _completeHandler = completeHandler;
    private readonly CancelTodoTaskHandler _cancelHandler = cancelHandler;

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoTaskCommand command,
                                            CancellationToken cancellationToken)
    {
        var id = await _createHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid id, CancellationToken cancellationToken)
    {
        await _startHandler.HandleAsync(new StartTodoTaskCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        await _completeHandler.HandleAsync(new CompleteTodoTaskCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        await _cancelHandler.HandleAsync(new CancelTodoTaskCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}
