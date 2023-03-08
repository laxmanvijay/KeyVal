using KeyValApi.Dto;

namespace KeyValApi.Services;

public interface IKeyValService
{
    Task<KeyValDto> CreateKeyVal(KeyValDto request);
    Task<KeyValDto> GetKeyVal(string key);
    Task<KeyValDto> UpdateKeyVal(string key, KeyValUpdateRequestDto request);
    Task<bool> DeleteKeyVal(string key);
}