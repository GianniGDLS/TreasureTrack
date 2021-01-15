using System.Threading.Tasks;

namespace TreasureTrack.Data.Managers.Interfaces
{
    public interface ILogManager
    {
        Task WriteLogAsync(string info, int? userId = null);
    }
}