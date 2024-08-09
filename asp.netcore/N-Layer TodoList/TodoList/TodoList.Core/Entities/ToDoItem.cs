using TodoList.Core.Enums;
using TodoList.Core.Exceptions;

namespace TodoList.Core.Entities;

public class ToDoItem
{
    private DateTime? deadline;

    public ToDoItem(string title,
                    DateTime? deadline = null,
                    string description = "",
                    Priority priority = Priority.Low)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Done = false;
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
    public Priority Priority { get; set; }

    public DateTime? Deadline
	{
		get { return deadline; }
		set 
		{
            if (value.HasValue && value.Value <= DateTime.Now)            
                throw new ToDoItemValidationException("Date can't be less than now date",nameof(Deadline));
            
			deadline = value; 
		}
	}

    public void SetCompleted()
    {
        Done = true;
    }

    public string ToDoListId { get; set; }
    public ToDoItemList ToDoList { get; set; }
}
