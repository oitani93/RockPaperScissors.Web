using System;
using System.IO;
using System.Threading.Tasks;
using RockPaperScissors.Services.Interface;

namespace RockPaperScissors.Services
{
    public class RockPaperScissorsService : IRockPaperScissorsService
    {
        public async Task SaveUserScoreSheetAsync(string name, string email, string mobile)
        {
            var text = $"Name: {name}{Environment.NewLine}Email: {email}{Environment.NewLine}Mobile: {mobile}{Environment.NewLine}Date: {DateTime.Now.ToShortDateString()}";
            var mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "UserScoreSheet.txt"), true))
            {
                await outputFile.WriteLineAsync(Environment.NewLine + text);
            }            
        }

    }
}
