namespace KeyValApi.Dto;

public class KeyValDto
{
    public string Key { get; set; } = string.Empty;
    public object Value { get; set; } = string.Empty;
}

public class KeyValUpdateRequestDto
{
    public object Value { get; set; } = string.Empty;
}