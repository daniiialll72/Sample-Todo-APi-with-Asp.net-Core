using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    private readonly IMongoCollection<Todo> _todos;

    public TodoService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("TodoDb"));
        var database = client.GetDatabase("TodoDb");

        _todos = database.GetCollection<Todo>("Todos");
    }

    public List<Todo> Get()
    {
        return _todos.Find(todo => true).ToList();
    }
    
    public Todo Get(string id)
    {
        return _todos.Find(todo => todo.Id == id).FirstOrDefault();
    }
    
    public void Create(Todo todo)
    {
         _todos.InsertOne(todo);
    }

    public void Update(string id, Todo newTodo)
    {
        _todos.ReplaceOne(todo => todo.Id == id, newTodo);
    }

    public void Remove(string id)
    {
        _todos.DeleteOne(todo => todo.Id == id);
    }
}