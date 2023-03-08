using System.Text.Json;
using KeyValApi.Exceptions;

namespace KeyValApi.Helpers;

public class KeyValHelpers
{
    public string SerializeJsonValue(object input)
    {
        string serializedObj;
        try 
        {
            serializedObj = JsonSerializer.Serialize(input);
            return serializedObj;
        }
        catch (Exception)
        {
            throw new InvalidJsonValueException("Bad json value");
        }
    }

    public object DeserializeJsonValue(string input)
    {
        object? deSerializedObj;
        try 
        {
            deSerializedObj = JsonSerializer.Deserialize<object>(input);
            
            if (deSerializedObj == null) {
                throw new InvalidJsonValueException("Bad json value");
            }
        }
        catch (Exception)
        {
            throw new InvalidJsonValueException("Bad json value");
        }

        return deSerializedObj;
    }
}