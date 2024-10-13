using battleshipAPI.Common;
using battleshipAPI.Service.Interfaces;

using BattleshipAPI.Application;
using BattleshipAPI.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace BattleshipAPI.Controllers;
[ApiController]
[Route("/api/[controller]")]

public class BattleController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IBotService _botService;
    private readonly IFireService _fireService;

    public BattleController(IPlayerService playerService, IBotService botService, IFireService fireService)
    {
        _playerService = playerService;
        _botService = botService;
        _fireService = fireService;
    }

    [HttpPost]
    [Route("player")]
    public async Task<IActionResult> Init([FromBody] Coordinates coordinates)
    {
        var response = await _playerService.SetPlayerCoordinates(coordinates);
        await _botService.SetBotCoordinates();
        return Ok(response);
    }

    [HttpPost]
    [Route("fire")]
    public async Task<IActionResult> Post([FromBody] List<int> firingPointCoordinate)
    {
        var fireOnBotResponse = await _fireService.FireOnBot(firingPointCoordinate);
        var fireOnPlayerResponse = await _fireService.FireOnPlayer();

        var response = new BaseResponse()
        {
            Data = new List<BaseResponse>()
            {
                new() { Message="PlayerResponse", Data = fireOnBotResponse },
                new() { Message="BotResponse", Data = fireOnPlayerResponse },
            }
        };

        return Ok(response);
    }
}
