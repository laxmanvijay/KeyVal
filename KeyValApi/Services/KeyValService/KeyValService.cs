using System.Text.Json;
using KeyValApi.Dto;
using KeyValApi.Exceptions;
using KeyValApi.Helpers;
using KeyValApi.Models;
using MongoDB.Driver;

namespace KeyValApi.Services;

public class KeyValService : IKeyValService
{
    private readonly IMongoCollection<KeyValModel> _collection;

    private readonly ILogger<KeyValService> _logger;

    private readonly KeyValHelpers _helpers;

    public KeyValService(IMongoCollection<KeyValModel> collection, ILogger<KeyValService> logger, KeyValHelpers helpers)
    {
        _collection = collection;
        _logger = logger;
        _helpers = helpers;
    }

    public async Task<KeyValDto> CreateKeyVal(KeyValDto request)
    {
        var keyVal = await _collection.Find(kv => kv.Key == request.Key).FirstOrDefaultAsync();
        if (keyVal != null) 
        {
            throw new DuplicateKeyException($"Key-value with key {request.Key} already present");
        }

        var keyValModel = new KeyValModel
        {
            Key = request.Key,
            Value = _helpers.SerializeJsonValue(request.Value)
        };

        await _collection.InsertOneAsync(keyValModel);

        _logger.LogInformation($"Creating key-val with key {request.Key} and id {keyValModel.Id}");

        return new KeyValDto
        {
            Key = keyValModel.Key,
            Value = request.Value
        };
    }

    public async Task<KeyValDto> GetKeyVal(string key)
    {
        var keyValModel = await _collection.Find(kv => kv.Key == key).FirstOrDefaultAsync();

        if (keyValModel == null) 
        {
            throw new ResourceNotFoundException($"Key-value with key {key} not found");
        }

        _logger.LogInformation($"Getting key-val with key {key} and id {keyValModel.Id}");

        return new KeyValDto
        {
            Key = keyValModel.Key,
            Value = _helpers.DeserializeJsonValue(keyValModel.Value)
        };
    }

    public async Task<KeyValDto> UpdateKeyVal(string key, KeyValUpdateRequestDto request)
    {
        var keyValModel = await _collection.Find(kv => kv.Key == key).FirstOrDefaultAsync();

        if (keyValModel == null) 
        {
            throw new ResourceNotFoundException($"Key-value with key {key} not found");
        }

        await _collection.ReplaceOneAsync(kv => kv.Id == keyValModel.Id, new KeyValModel
        {
            Id = keyValModel.Id,
            Key = keyValModel.Key,
            Value = _helpers.SerializeJsonValue(request.Value)
        });

        _logger.LogInformation($"Updating key-val with key {key} and id {keyValModel.Id}");

        return new KeyValDto
        {
            Key = keyValModel.Key,
            Value = request.Value
        };
    }

    public async Task<bool> DeleteKeyVal(string key)
    {
        var keyValModel = await _collection.Find(kv => kv.Key == key).FirstOrDefaultAsync();

        if (keyValModel == null) 
        {
            throw new ResourceNotFoundException($"Key-value with key {key} not found");
        }

        _logger.LogInformation($"Deleting key-val with key {key} and id {keyValModel.Id}");

        await _collection.DeleteOneAsync(kv => kv.Key == keyValModel.Id);

        return true;
    }
}