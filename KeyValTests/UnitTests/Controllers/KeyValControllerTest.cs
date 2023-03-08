using KeyValApi.Controllers;
using KeyValApi.Dto;
using KeyValApi.Exceptions;
using KeyValApi.Services;
using KeyValTests.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace KeyValTests.UnitTests;

public class KeyValControllerTests
{
    private readonly KeyValController _controller;
    private readonly Mock<IKeyValService> _keyValService;

    private readonly Mock<ILogger<KeyValController>> _keyValLogger;

    public KeyValControllerTests()
    {
        _keyValService = new Mock<IKeyValService>();
        _keyValLogger = new Mock<ILogger<KeyValController>>();
        _controller = new KeyValController(_keyValLogger.Object, _keyValService.Object);
    }

    [Fact]
    public async void CreateKeyValue_ShouldReturn200_OnValidInput()
    {
        var resp = await _controller.CreateKeyValue(KeyValMock.KeyValDto_ProperInput);

        Assert.Equal((resp as OkObjectResult)!.StatusCode, 200);
    }

    [Fact]
    public async void CreateKeyValue_ShouldReturn400_OnDuplicateKey()
    {
        _keyValService.Setup(x => x.CreateKeyVal(It.IsAny<KeyValDto>())).ThrowsAsync(new DuplicateKeyException("duplicate key"));
        
        var resp = await _controller.CreateKeyValue(KeyValMock.KeyValDto_ProperInput);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<BadRequestObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 400);
    }

    [Fact]
    public async void CreateKeyValue_ShouldReturn500_OnException()
    {
        _keyValService.Setup(x => x.CreateKeyVal(It.IsAny<KeyValDto>())).ThrowsAsync(new Exception("internal error"));
        
        var resp = await _controller.CreateKeyValue(KeyValMock.KeyValDto_ProperInput);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<ObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 500);
    }

    [Fact]
    public async void GetKeyValue_ShouldReturn200_OnValidInput()
    {
        var resp = await _controller.GetKeyValue(KeyValMock.MockKey);

        Assert.Equal((resp as OkObjectResult)!.StatusCode, 200);
    }

    [Fact]
    public async void GetKeyValue_ShouldReturn400_OnKeyNotFound()
    {
        _keyValService.Setup(x => x.GetKeyVal(It.IsAny<string>())).ThrowsAsync(new ResourceNotFoundException("key not found"));
        
        var resp = await _controller.GetKeyValue(KeyValMock.MockKey);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<BadRequestObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 400);
    }

    [Fact]
    public async void GetKeyValue_ShouldReturn500_OnException()
    {
        _keyValService.Setup(x => x.GetKeyVal(It.IsAny<string>())).ThrowsAsync(new Exception("internal error"));
        
        var resp = await _controller.GetKeyValue(KeyValMock.MockKey);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<ObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 500);
    }

    [Fact]
    public async void UpdateKeyValue_ShouldReturn200_OnValidInput()
    {
        var resp = await _controller.UpdateKeyValue(KeyValMock.MockKey, KeyValMock.KeyValUpdateRequestDto_ProperInput);

        Assert.Equal((resp as OkObjectResult)!.StatusCode, 200);
    }

    [Fact]
    public async void UpdateKeyValue_ShouldReturn400_OnKeyNotFound()
    {
        _keyValService.Setup(x => x.UpdateKeyVal(It.IsAny<string>(), It.IsAny<KeyValUpdateRequestDto>())).ThrowsAsync(new ResourceNotFoundException("key not found"));
        
        var resp = await _controller.UpdateKeyValue(KeyValMock.MockKey, KeyValMock.KeyValUpdateRequestDto_ProperInput);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<BadRequestObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 400);
    }

    [Fact]
    public async void UpdateKeyValue_ShouldReturn500_OnException()
    {
        _keyValService.Setup(x => x.UpdateKeyVal(It.IsAny<string>(), It.IsAny<KeyValUpdateRequestDto>())).ThrowsAsync(new Exception("internal error"));
        
        var resp = await _controller.UpdateKeyValue(KeyValMock.MockKey, KeyValMock.KeyValUpdateRequestDto_ProperInput);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<ObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 500);
    }

    [Fact]
    public async void DeleteKeyValue_ShouldReturn200_OnValidInput()
    {
        var resp = await _controller.DeleteKeyValue(KeyValMock.MockKey);

        var objectResponse = Assert.IsType<NoContentResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 204);
    }

    [Fact]
    public async void DeleteKeyValue_ShouldReturn400_OnKeyNotFound()
    {
        _keyValService.Setup(x => x.DeleteKeyVal(It.IsAny<string>())).ThrowsAsync(new ResourceNotFoundException("key not found"));
        
        var resp = await _controller.DeleteKeyValue(KeyValMock.MockKey);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<BadRequestObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 400);
    }

    [Fact]
    public async void DeleteKeyValue_ShouldReturn500_OnException()
    {
        _keyValService.Setup(x => x.DeleteKeyVal(It.IsAny<string>())).ThrowsAsync(new Exception("internal error"));
        
        var resp = await _controller.DeleteKeyValue(KeyValMock.MockKey);
        Console.WriteLine(resp);

        var objectResponse = Assert.IsType<ObjectResult>(resp); 

        Assert.Equal(objectResponse.StatusCode, 500);
    }
}