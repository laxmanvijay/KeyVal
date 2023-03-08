using KeyValApi.Dto;
using KeyValApi.Exceptions;
using KeyValApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyValApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyValController : ControllerBase
{

    private readonly ILogger<KeyValController> _logger;

    private readonly IKeyValService _keyValService;

    public KeyValController(ILogger<KeyValController> logger, IKeyValService keyValService)
    {
        _logger = logger;
        _keyValService = keyValService;
    }

    [HttpGet]
    [Route("/ping")]
    public ActionResult GetPing()
    {
        return Ok("pong");
    }

    [HttpPost]
    [Route("/data")]
    public async Task<ActionResult> CreateKeyValue(KeyValDto request)
    {
        _logger.LogInformation($"Invoking key-value service for creating the key-value with key {request.Key}");

        if (this.Request.Headers.ContentLength > 100000)
        {
            return BadRequest("input too large");
        }

        try
        {
            var response = await _keyValService.CreateKeyVal(request);
            return Ok(response);
        }
        catch (DuplicateKeyException)
        {
            _logger.LogError($"Key already present {request.Key}");
            return BadRequest("Key already present");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "An internal error occurred");
        }
    }

    [HttpGet]
    [Route("/data/{key}")]
    public async Task<ActionResult> GetKeyValue(string key)
    {
        _logger.LogInformation($"Invoking key-value service for getting the key-value with key {key}");

        try
        {
            var response = await _keyValService.GetKeyVal(key);
            return Ok(response);
        }
        catch (ResourceNotFoundException)
        {
            _logger.LogError($"Key not present {key}");
            return BadRequest("Key not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "An internal error occurred");
        }
    }

    [HttpPut]
    [Route("/data/{key}")]
    public async Task<ActionResult> UpdateKeyValue(string key, KeyValUpdateRequestDto request)
    {
        _logger.LogInformation($"Invoking key-value service for updating the key-value with key {key}");

        if (this.Request.Headers.ContentLength > 100000)
        {
            return BadRequest("input too large");
        }

        try
        {
            var response = await _keyValService.UpdateKeyVal(key, request);
            return Ok(response);
        }
        catch (ResourceNotFoundException)
        {
            _logger.LogError($"Key not present {key}");
            return BadRequest("Key not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "An internal error occurred");
        }
    }

    [HttpDelete]
    [Route("/data/{key}")]
    public async Task<ActionResult> DeleteKeyValue(string key)
    {
        _logger.LogInformation($"Invoking key-value service for deleting the key-value with key {key}");

        try
        {
            var response = await _keyValService.DeleteKeyVal(key);
            return NoContent();
        }
        catch (ResourceNotFoundException)
        {
            _logger.LogError($"Key not present {key}");
            return BadRequest("Key not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "An internal error occurred");
        }
    }
}
