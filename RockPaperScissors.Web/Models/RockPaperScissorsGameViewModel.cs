using System;
using static RockPaperScissors.Web.Infrustructure.ApplicationConstants;

namespace RockPaperScissors.Web.Models
{
    public class RockPaperScissorsGameViewModel
    {
        public RockPaperScissorsGameViewModel(PlayerViewModel playerOne, PlayerViewModel playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            IsValid();
            SetRowsClass();
        }

        public PlayerViewModel PlayerOne { get; protected set; }

        public PlayerViewModel PlayerTwo { get; protected set; }

        public string PlayerComputerTableRowClass { get; protected set; }

        public string PlayerUserTableRowClass { get; protected set; }

        public string SubmitButtonClass => !string.IsNullOrEmpty(WinnerPlayer) ? "hide" : string.Empty;

        public string ResetButtonClass => WinnerPlayer == PlayerType.COMPUTER ? string.Empty : "hide";

        public string SaveButtonClass => WinnerPlayer == PlayerType.USER ? string.Empty : "hide";

        public void SubmitSelection(Actions action)
        {
            SetAction(PlayerType.USER, action);
            GenerateRandomAction();
            UpdateScoreSheet();
            SetRowsClass();
            IsThereAWinner();
        }

        public string WinnerPlayer { get; set; }

        public void SetRowsClass()
        {
            if (PlayerOne.ScoreSheet.Wins > PlayerTwo.ScoreSheet.Wins)
            {
                PlayerComputerTableRowClass = "alert-danger";
                PlayerUserTableRowClass = "alert-success";
            }
            else if (PlayerOne.ScoreSheet.Wins == PlayerTwo.ScoreSheet.Wins)
            {
                PlayerComputerTableRowClass = "alert-info";
                PlayerUserTableRowClass = "alert-info";
            }
            else
            {
                PlayerComputerTableRowClass = "alert-success";
                PlayerUserTableRowClass = "alert-danger";
            }
        }

        private void IsThereAWinner()
            => WinnerPlayer = PlayerOne.ScoreSheet.Wins >= 5
            ? PlayerType.USER : PlayerTwo.ScoreSheet.Wins >= 5
            ? PlayerType.COMPUTER
            : null;

        private void SetAction(string playerType, Actions action)
        {
            if (playerType == PlayerType.USER)
            {
                PlayerOne.Action = action;
            }
            else
            {
                PlayerTwo.Action = action;
            }
        }

        private void GenerateRandomAction()
        {
            var actionsArray = Enum.GetValues(typeof(Actions));
            var action = (Actions)new Random().Next(0, actionsArray.Length);
            SetAction(PlayerType.COMPUTER, action);
        }

        private void UpdateScoreSheet()
        {
            if (PlayerOne.Action == Actions.ROCK && PlayerTwo.Action == Actions.SCISSORS
                || PlayerOne.Action == Actions.PAPER && PlayerTwo.Action == Actions.ROCK
                || PlayerOne.Action == Actions.SCISSORS && PlayerTwo.Action == Actions.PAPER)
            {
                PlayerOne.ScoreSheet.Wins++;
                PlayerTwo.ScoreSheet.Losses++;
            }
            else if (PlayerTwo.Action == Actions.ROCK && PlayerOne.Action == Actions.SCISSORS
                || PlayerTwo.Action == Actions.PAPER && PlayerOne.Action == Actions.ROCK
                || PlayerTwo.Action == Actions.SCISSORS && PlayerOne.Action == Actions.PAPER)
            {
                PlayerTwo.ScoreSheet.Wins++;
                PlayerOne.ScoreSheet.Losses++;
            }
            else
            {
                PlayerOne.ScoreSheet.Ties++;
                PlayerTwo.ScoreSheet.Ties++;
            }
        }

        private void IsValid()
        {
            if (PlayerOne.PlayerType == PlayerTwo.PlayerType
                || PlayerTwo.PlayerType != PlayerType.COMPUTER)
            {
                throw new InvalidOperationException("Player Two must be a computer. Invalid Players! REMINDER: ONLY User Vs Comp");
            }
        }

    }
}
