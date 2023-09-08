using back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly LunchContext context;
        
        public VoteRepository(LunchContext context)
        {
            this.context = context;
        }

        public async Task<List<Vote>> GetVotesByDate(int year, int month, int day)
        {
            return await context.Votes.Where(v => v.Year == year && v.Month == month && v.Day == day).ToListAsync();
        }


        public async Task<Vote?> GetVotesByDateAndIdentifier(int year, int month, int day, string identifier)
        {
            return await context.Votes.FirstOrDefaultAsync(v => v.Year == year && v.Month == month && v.Day == day && v.Identifier == identifier);
        }
        
        public async Task Vote(int year, int month, int day, int weekNumber, string identifier, int restaurantId)
        {
            await context.Votes.AddAsync(new Vote {Day = day, Identifier = identifier, Month = month, RestaurantId = restaurantId, WeekNumber = weekNumber, Year = year});
            await context.SaveChangesAsync();
        }


        public async Task<List<Vote>> GetVotesByWeek(int weekNumber)
        {
            return await context.Votes.Where(v => v.WeekNumber == weekNumber).ToListAsync();
        }

    }
}