using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public ActionResult<List<Todo>> Get()
    {
        return _todoService.Get();
    }

    [HttpGet("{id:length(24)}", Name = "GetTodo")]
    public ActionResult<Todo> Get(string id)
    {
        var todo = _todoService.Get(id);
        return todo;
    }

    [HttpPost]
    public ActionResult<Todo> Create(Todo todo)
    {
        _todoService.Create(todo);

        return CreatedAtRoute("GetTodo", new { id = todo.Id.ToString() }, todo);
    }

    [HttpPut("id")]
    public IActionResult Update(string id, Todo newTodo)
    {
        var todo = _todoService.Get(id);
        if (todo == null)
            throw new Exception("not found");

        _todoService.Update(id, todo);

        return Ok();
    }
    
    [HttpDelete("id")]
    public IActionResult Delete(string id)
    {
        var todo = _todoService.Get(id);
        if (todo == null)
            throw new Exception("not found");
        
        _todoService.Remove(id);
        
        return Ok();
    }
}