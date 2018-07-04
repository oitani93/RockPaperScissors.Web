using NUnit.Framework;
using System;
using RockPaperScissors.Web.Models;
using static RockPaperScissors.Web.Infrustructure.ApplicationConstants;
using Newtonsoft.Json;

namespace RockPaperScissors.Test
{
    [TestFixture]
    public class RockPaperScissorsTest
    {
        [Test]
        public void InvalidOperationExceptionPlayerTwoPlayerTypeNotComputerTest()
        {
            var playerOne = new PlayerViewModel { PlayerType = PlayerType.COMPUTER };
            var playerTwo = new PlayerViewModel { PlayerType = PlayerType.USER };
            var ex = Assert.Throws<InvalidOperationException>(() => new RockPaperScissorsGameViewModel(playerOne, playerTwo));
            Assert.That(ex.Message.Equals("Player Two must be a computer. Invalid Players! REMINDER: ONLY User Vs Comp"));
        }

        [Test]
        public void InvalidOperationExceptionTwoPlayersSamePlayerTypeTest()
        {
            var playerOne = new PlayerViewModel { PlayerType = PlayerType.COMPUTER };
            var playerTwo = new PlayerViewModel { PlayerType = PlayerType.COMPUTER };
            var ex = Assert.Throws<InvalidOperationException>(() => new RockPaperScissorsGameViewModel(playerOne, playerTwo));
            Assert.That(ex.Message.Equals("Player Two must be a computer. Invalid Players! REMINDER: ONLY User Vs Comp"));
        }

        [Test]
        public void PlayerTypeUserIsWinnerTest()
        {
            var playerOne = new PlayerViewModel { PlayerType = PlayerType.USER, ScoreSheet = new PlayerScoreSheetViewModel { Wins = 5 } };
            var playerTwo = new PlayerViewModel { PlayerType = PlayerType.COMPUTER, ScoreSheet = new PlayerScoreSheetViewModel { Wins = 2 } };
            var gameSession = new RockPaperScissorsGameViewModel(playerOne, playerTwo);
            gameSession.SubmitSelection((Actions)2);
            var result = gameSession.WinnerPlayer;

            Assert.AreEqual(result, PlayerType.USER);
        }

        [Test]
        public void PlayersScoreSheetsUpdatedTest()
        {
            var scoreSheetOne = new PlayerScoreSheetViewModel { Wins = 3, Losses = 2, Ties = 1 };
            var scoreSheetTwo = new PlayerScoreSheetViewModel { Wins = 2, Losses = 3, Ties = 1 };
            var originalValueScoreSheetOne = JsonConvert.SerializeObject(scoreSheetOne);
            var originalValueScoreSheetTwo = JsonConvert.SerializeObject(scoreSheetTwo);
            var playerOne = new PlayerViewModel { PlayerType = PlayerType.USER, ScoreSheet =  scoreSheetOne};
            var playerTwo = new PlayerViewModel { PlayerType = PlayerType.COMPUTER, ScoreSheet = scoreSheetTwo };
            var gameSession = new RockPaperScissorsGameViewModel(playerOne, playerTwo);
            gameSession.SubmitSelection((Actions)1);

            var updatedScoreSheetOne = gameSession.PlayerOne.ScoreSheet;
            var updatedScoreSheetTwo = gameSession.PlayerTwo.ScoreSheet;

            var result = originalValueScoreSheetOne != JsonConvert.SerializeObject(updatedScoreSheetOne)
                && originalValueScoreSheetTwo != JsonConvert.SerializeObject(updatedScoreSheetTwo);

            Assert.IsTrue(result);
        }
    }
}
