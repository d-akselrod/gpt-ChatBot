public class Message
{
    public string message;
    public DateTime time;

    public Message(string message, DateTime time)
    {
        this.message = message;
        this.time = time;
    }

    public string GetMessage()
    {
        return message;
    }

    public DateTime GetTime()
    {
        return time;
    }
}