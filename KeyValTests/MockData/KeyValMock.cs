using System.Collections.Generic;
using System.Text.Json;
using KeyValApi.Dto;
using KeyValApi.Models;

namespace KeyValTests.MockData;

public static class KeyValMock
{
    public static KeyValDto KeyValDto_ProperInput = new KeyValDto
    {
        Key = "test",
        Value = new { name = "test" }
    };

    public static KeyValUpdateRequestDto KeyValUpdateRequestDto_ProperInput = new KeyValUpdateRequestDto
    {
        Value = new { name = "test" }
    };

    public static string MockKey = "test";

    public static List<KeyValModel> ListOfKeyvals = new List<KeyValModel>() {
        new KeyValModel 
        {
            Key = KeyValMock.KeyValDto_ProperInput.Key,
            Value = JsonSerializer.Serialize(new { name = "test" })
        }
    };
}