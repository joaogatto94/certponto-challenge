using System.Globalization;
using back.Repositories.Interfaces;
using back.Services.Interfaces;

namespace back.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository voteRepository;


        public VoteService(IVoteRepository voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        public async Task<bool> HasVoted(string identifier)
        {
            var date = GetVoteDate();
            var vote = await voteRepository.GetVotesByDateAndIdentifier(date.Year, date.Month, date.Day, identifier);
            return vote != null;
        }

        public async Task<bool> IsWeekWinner(int restaurantId)
        {
            var winners = await GetWeekWinners();
            return winners.Any(x => x == restaurantId);
        }
        
        public async Task Vote(string identifier, int restaurantId)
        {
            var date = GetVoteDate();

            await voteRepository.Vote(date.Year, date.Month, date.Day, GetWeekNumber(date), identifier, restaurantId);
        }

        public async Task<List<Vote>> GetTodayVotes()
        {
            var date = DateTime.Now;

            return await voteRepository.GetVotesByDate(date.Year, date.Month, date.Day);
        }

        public async Task<List<int>> GetWeekWinners()
        {
            var date = GetVoteDate();
            var weekVotes = await voteRepository.GetVotesByWeek(GetWeekNumber(date));

            var daysGrouped = weekVotes.Where(x => x.Day != date.Day).GroupBy(x => x.Day);
            var winners = new List<int>();

            foreach (var day in daysGrouped)
            {
                var restaurants = day.GroupBy(x => x.RestaurantId).Select(restaurant => new {
                    restaurant.Key,
                    Votes = restaurant.Count(),
                });
                
                var max = restaurants.Max(x => x.Votes);

                var restaurantsWithMaxVotes = restaurants.Where(x => x.Votes == max);
                var winnerId = restaurantsWithMaxVotes.First().Key;

                if (restaurantsWithMaxVotes.Count() > 1)
                {
                    var vote = day.First();
                    await voteRepository.Vote(vote.Year, vote.Month, vote.Day, vote.WeekNumber, "SysTieBreaker", winnerId);
                }

                winners.Add(winnerId);
            }

            return winners;
        }

        public DateTime GetVoteDate()
        {
            var date = DateTime.Now;

            if (date.Hour > 11)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        private int GetWeekNumber(DateTime date)
        {
            Calendar calendar = new CultureInfo("en-US").Calendar;
            
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}