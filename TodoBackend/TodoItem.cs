using System.Net.Mime;

namespace TodoBackend;

public class TodoItem
{
    public int TodoId { get; set; }
    public string Text { get; set; }

    public bool Done { get; set; }
    public TodoItem(int todoId, string text, bool done)
    {
        TodoId = todoId;
        Text = text;
        Done = done;
    }
}
