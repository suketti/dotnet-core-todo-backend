namespace TodoBackend;

public class TodoEditor
{
    public string Text { get; set; }
    public bool Done { get; set; }
    
    public TodoEditor(string text, bool done)
    {
        Text = text;
        Done = done;
    }
}