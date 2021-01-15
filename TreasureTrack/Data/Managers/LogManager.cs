using System.Threading.Tasks;
using TreasureTrack.Data.Entities;
using TreasureTrack.Data.Managers.Interfaces;

namespace TreasureTrack.Data.Managers
{
    public class LogManager : ILogManager
    {
        private readonly ProjectDbContext _context;

        public LogManager(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task WriteLogAsync(string info, int? userId = null)
        {
            await _context.Logs.AddAsync(new Log(info, userId));
            await _context.SaveChangesAsync();
        }
    }
}