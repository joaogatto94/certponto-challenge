namespace back.Repositories.Interfaces
{
    public interface IVoteRepository
    {
        Task<List<Vote>> GetVotesByDate(int year, int month, int day);
        Task<Vote?> GetVotesByDateAndIdentifier(int year, int month, int day, string identifier);
        Task<List<Vote>> GetVotesByWeek(int weekNumber);
        Task Vote(int year, int month, int day, int weekNumber, string identifier, int restaurantId);
    }
}