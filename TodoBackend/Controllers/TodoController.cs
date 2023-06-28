using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace TodoBackend.Controllers;

[ApiController]
[Route("[controller]")]

public class TodoController : Controller
{
    DB db = new DB();
    // GET
    [HttpGet(Name = "GetTodos")]
    public IEnumerable<TodoItem> Get()
    {
        MySqlConnection con = db.MakeConnection();
        List<TodoItem> items = new List<TodoItem>();
        var command = new MySqlCommand("SELECT * FROM todos", con);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            items.Add(new TodoItem(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
        }
        reader.Close();
        db.CloseConnection();
        return items;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByID(int id)
    {
        MySqlConnection con = db.MakeConnection();
        var command = new MySqlCommand("SELECT * FROM todos WHERE TodoId = @id", con);
        command.Parameters.AddWithValue("id", id);
        var reader = command.ExecuteReader();
        if (reader.Read() == false)
        {
            reader.Close();
            db.CloseConnection();
            return NotFound();
        }
        TodoItem item = new TodoItem(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
        reader.Close();
        db.CloseConnection();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddTodoItem(TodoEditor todoEditor)
    {
        MySqlConnection con = db.MakeConnection();
        var command = new MySqlCommand("INSERT INTO todos (Text, Done) VALUES (@Text, @Done)", con);
        command.Parameters.AddWithValue("Text", todoEditor.Text);
        command.Parameters.AddWithValue("Done", todoEditor.Done);
        if (command.ExecuteNonQuery() > 0)
        {
            db.CloseConnection();
            return Ok();
        }
        db.CloseConnection();
        return StatusCode(500);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult EditTodoItem(TodoEditor todoEditor, int id)
    {
        MySqlConnection con = db.MakeConnection();
        var command = new MySqlCommand("UPDATE todos SET `Text` = @text, `Done` = @done WHERE `TodoId` = @id", con);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("text", todoEditor.Text);
        command.Parameters.AddWithValue("done", todoEditor.Done);

        if (command.ExecuteNonQuery() > 0)
        {
            db.CloseConnection();
            return Ok();
        }
        db.CloseConnection();
        return StatusCode(404);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTodoItem(int id)
    {
        MySqlConnection con = db.MakeConnection();
        var command = new MySqlCommand("DELETE FROM todos where `TodoId` = @id", con);
        command.Parameters.AddWithValue("id", id);
        
        if (command.ExecuteNonQuery() > 0)
        {
            db.CloseConnection();
            return Ok();
        }
        db.CloseConnection();
        return StatusCode(404);
    }
}