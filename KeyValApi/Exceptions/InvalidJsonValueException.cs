namespace KeyValApi.Exceptions;

[Serializable]
public class InvalidJsonValueException: Exception
{
    public InvalidJsonValueException(string msg): base(msg) { }
}