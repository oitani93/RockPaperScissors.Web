using System.Threading.Tasks;

namespace RockPaperScissors.Services.Interface
{
    public interface IRockPaperScissorsService
    {
        Task SaveUserScoreSheetAsync(string name, string email, string mobile);
    }
}
