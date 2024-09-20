namespace ColorPickerGame.Models
{
    public class Bet
    {
        public string SelectedColor { get; set; }
        public decimal BetAmount { get; set; }
        public decimal Balance { get; set; } = 1000; // Initial balance
    }
}
