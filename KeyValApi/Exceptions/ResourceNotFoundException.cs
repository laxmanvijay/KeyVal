namespace KeyValApi.Exceptions;

[Serializable]
public class ResourceNotFoundException: Exception
{
    public ResourceNotFoundException(string msg): base(msg) { }
}