using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ColorPickerGame.Models;

namespace ColorPickerGame.Controllers
{
    public class ColorPickerController : Controller
    {
        private readonly string[] availableColors = { "Red", "Black", "Blue" };
        private readonly decimal initialBalance = 1000;

        // GET: ColorPicker
        public ActionResult Index()
        {
            List<Bet> initialBets = new List<Bet>
            {
                new Bet { Balance = initialBalance }, // User 1
                new Bet { Balance = initialBalance }, // User 2
                new Bet { Balance = initialBalance }  // User 3
            };

            ViewBag.Colors = availableColors;
            return View(initialBets);
        }

        // POST: SubmitBets
        [HttpPost]
        public ActionResult SubmitBets(List<Bet> bets)
        {
            if (bets == null || bets.Count == 0)
            {
                TempData["Result"] = "No bets were placed.";
                return RedirectToAction("Index");
            }

            Random random = new Random();
            string randomColor = availableColors[random.Next(availableColors.Length)];
            List<string> results = new List<string>();

            for (int i = 0; i < bets.Count; i++)
            {
                var bet = bets[i];

                // Only process if the user placed a valid bet (color selected and bet amount > 0)
                if (bet.BetAmount > 0 && !string.IsNullOrEmpty(bet.SelectedColor))
                {
                    string resultMessage;
                    if (bet.SelectedColor.Equals(randomColor, StringComparison.OrdinalIgnoreCase))
                    {
                        bet.Balance += bet.BetAmount;
                        resultMessage = $"User {i + 1} won! Bet: {bet.BetAmount}, Color: {bet.SelectedColor}. New Balance: {bet.Balance}";
                    }
                    else
                    {
                        bet.Balance -= bet.BetAmount;
                        resultMessage = $"User {i + 1} lost. Bet: {bet.BetAmount}, Color: {bet.SelectedColor}. New Balance: {bet.Balance}";
                    }

                    results.Add(resultMessage);
                }
                else
                {
                    // Handle when the user skips the bet (either no color or bet amount is 0)
                    results.Add($"User {i + 1} skipped the bet.");
                }
            }

            TempData["Result"] = string.Join("<br/>", results);
            return RedirectToAction("Index");
        }
    }
}
