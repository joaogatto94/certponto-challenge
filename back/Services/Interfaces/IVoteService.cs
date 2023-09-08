namespace back.Services.Interfaces
{
    public interface IVoteService
    {
        Task<bool> HasVoted(string identifier);
        Task Vote(string identifier, int restaurantId);
        Task<List<Vote>> GetTodayVotes();
        DateTime GetVoteDate();
        Task<bool> IsWeekWinner(int restaurantId);
        Task<List<int>> GetWeekWinners();
    }
}