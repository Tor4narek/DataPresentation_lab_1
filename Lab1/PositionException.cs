namespace Lab1;

public class PositionException : Exception
{
    public PositionException(string message, Exception? innerException=null) 
        : base(message, innerException) { }
}