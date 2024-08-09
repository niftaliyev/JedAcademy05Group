namespace TodoList.Core.Exceptions;

public class ToDoItemValidationException : ArgumentException
{
    public ToDoItemValidationException(string message, string paramName) : base( message, paramName)
    {
    }
}
