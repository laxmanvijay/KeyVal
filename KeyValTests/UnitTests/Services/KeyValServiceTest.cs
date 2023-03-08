using System.Collections.Generic;
using KeyValApi.Exceptions;
using KeyValApi.Helpers;
using KeyValApi.Models;
using KeyValApi.Services;
using KeyValTests.MockData;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;

namespace KeyValTests.UnitTests;

public class KeyValServiceTest
{
    private readonly Mock<IMongoCollection<KeyValModel>> _collection;
    private readonly Mock<ILogger<KeyValService>> _logger;

    private readonly Mock<KeyValHelpers> _helpers;

    private readonly KeyValService _service;

    public KeyValServiceTest()
    {
        _collection = new Mock<IMongoCollection<KeyValModel>>();
        _logger = new Mock<ILogger<KeyValService>>();
        _helpers = new Mock<KeyValHelpers>();

        _service = new KeyValService(_collection.Object, _logger.Object, _helpers.Object);
    }

    [Fact]
    public async void CreateKeyVal_ShouldReturnOutput_ForProperInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();


        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);

        asyncCursor.SetReturnsDefault<object>(null);
                                
        var resp = await _service.CreateKeyVal(KeyValMock.KeyValDto_ProperInput);

        Assert.Equal(resp.Key, KeyValMock.KeyValDto_ProperInput.Key);
        Assert.Equal(KeyValMock.KeyValDto_ProperInput.Value, resp.Value);
    }

    [Fact]
    public async void CreateKeyVal_ShouldThrowError_ForDuplicateInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();

        asyncCursor.Setup(_ => _.Current).Returns(KeyValMock.ListOfKeyvals);

        asyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

        asyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);
        
                                
        await Assert.ThrowsAsync<DuplicateKeyException>(() => _service.CreateKeyVal(KeyValMock.KeyValDto_ProperInput));
    }

    [Fact]
    public async void GetKeyVal_ShouldThrowError_ForNotFoundInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();


        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);

        asyncCursor.SetReturnsDefault<object>(null);

        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _service.GetKeyVal(KeyValMock.MockKey));
    }

    [Fact]
    public async void GetKeyVal_ShouldReturnProperOutput_ForProperInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();

        asyncCursor.Setup(_ => _.Current).Returns(KeyValMock.ListOfKeyvals);

        asyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

        asyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);
        
                                
        var resp = await _service.GetKeyVal(KeyValMock.MockKey);

        Assert.Equal(resp.Key, KeyValMock.KeyValDto_ProperInput.Key);
    }

    [Fact]
    public async void UpdateKeyVal_ShouldThrowError_ForNotFoundInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();


        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);

        asyncCursor.SetReturnsDefault<object>(null);

        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _service.UpdateKeyVal(KeyValMock.MockKey, KeyValMock.KeyValUpdateRequestDto_ProperInput));
    }

    [Fact]
    public async void UpdateKeyVal_ShouldReturnProperOutput_ForProperInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();

        asyncCursor.Setup(_ => _.Current).Returns(KeyValMock.ListOfKeyvals);

        asyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

        asyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);
        
                                
        var resp = await _service.UpdateKeyVal(KeyValMock.MockKey, KeyValMock.KeyValUpdateRequestDto_ProperInput);

        Assert.Equal(resp.Key, KeyValMock.KeyValDto_ProperInput.Key);
    }

    [Fact]
    public async void DeleteKeyVal_ShouldThrowError_ForNotFoundInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();


        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);

        asyncCursor.SetReturnsDefault<object>(null);

        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _service.DeleteKeyVal(KeyValMock.MockKey));
    }

    [Fact]
    public async void DeleteKeyVal_ShouldReturnProperOutput_ForProperInput()
    {

        var asyncCursor = new Mock<IAsyncCursor<KeyValModel>>();

        asyncCursor.Setup(_ => _.Current).Returns(KeyValMock.ListOfKeyvals);

        asyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

        asyncCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

        _collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<KeyValModel>>(), It.IsAny<FindOptions<KeyValModel>>(), default))
                                .ReturnsAsync(asyncCursor.Object);
        
                                
        var resp = await _service.DeleteKeyVal(KeyValMock.MockKey);

        Assert.True(resp);
    }
}