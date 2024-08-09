namespace TodoList.Core.Entities;

public class ToDoItemList
{
    public ToDoItemList()
    {
        ToDoItems = new List<ToDoItem>();
    }
    public ToDoItemList(string title) : this() 
    {
        Tittle = title;
    }
    public string Id { get; set; }
    public string Tittle { get; set; }
    public string UserId { get; set; }

    public List<ToDoItem> ToDoItems { get; set; }
}
