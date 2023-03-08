namespace KeyValApi.Exceptions;

[Serializable]
public class DuplicateKeyException: Exception
{
    public DuplicateKeyException(string msg): base(msg) { }
}