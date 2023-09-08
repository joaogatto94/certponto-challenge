using back.Dtos;
using back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers;

[ApiController]
[Route("[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVoteService voteService;

    public VotesController(IVoteService voteService)
    {
        this.voteService = voteService;
    }

    [HttpGet("today")]
    public async Task<ActionResult<List<Vote>>> GetVotesToday()
    {
        return Ok(await voteService.GetTodayVotes());
    }
    
    [HttpGet("week-winners")]
    public async Task<ActionResult<List<int>>> GetWeekWinners()
    {
        return Ok(await voteService.GetWeekWinners());
    }
    
    [HttpGet("vote-date")]
    public ActionResult<DateTime> GetVoteDate()
    {
        return Ok(voteService.GetVoteDate());
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ActionResult> Vote(VotePost model)
    {
        if (await voteService.HasVoted(model.Identifier))
        {
            return BadRequest("HasVoted");
        }

        if (await voteService.IsWeekWinner(model.RestaurantId))
        {
            return BadRequest("IsWinner");
        }

        await voteService.Vote(model.Identifier, model.RestaurantId);
        return Ok();
    }
}
