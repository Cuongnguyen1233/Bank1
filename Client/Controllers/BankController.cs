using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class BankController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:7001"; // Bank1 API URL

        public BankController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string accountName, int pincode)
        {
            try
            {
                var loginRequest = new { accountName, pincode };
                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/login", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var userData = JsonSerializer.Deserialize<JsonElement>(result);
                    
                    // Store user data in session
                    HttpContext.Session.SetString("CurrentUser", result);
                    return RedirectToAction("Transaction");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorData = JsonSerializer.Deserialize<JsonElement>(errorContent);
                    ViewBag.ErrorMessage = errorData.GetProperty("message").GetString();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Connection error. Please try again.";
            }

            return View();
        }

        public IActionResult Transaction()
        {
            var userData = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(userData))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTransaction(decimal amount, string transactionType)
        {
            try
            {
                var userData = HttpContext.Session.GetString("CurrentUser");
                if (string.IsNullOrEmpty(userData))
                {
                    return RedirectToAction("Login");
                }

                var user = JsonSerializer.Deserialize<JsonElement>(userData);
                var accountName = user.GetProperty("accountName").GetString();

                var transactionRequest = new { accountName, amount, transactionType };
                var json = JsonSerializer.Serialize(transactionRequest);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/transaction", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var transactionResult = JsonSerializer.Deserialize<JsonElement>(result);
                    
                    ViewBag.SuccessMessage = $"Transaction successful! New balance: {transactionResult.GetProperty("newBalance").GetDecimal():N0} USD";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorData = JsonSerializer.Deserialize<JsonElement>(errorContent);
                    ViewBag.ErrorMessage = errorData.GetProperty("message").GetString();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Connection error. Please try again.";
            }

            return View("Transaction");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            try
            {
                var userData = HttpContext.Session.GetString("CurrentUser");
                if (string.IsNullOrEmpty(userData))
                {
                    return Json(new { balance = 0 });
                }

                var user = JsonSerializer.Deserialize<JsonElement>(userData);
                var accountName = user.GetProperty("accountName").GetString();

                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/transactions/{accountName}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var transactions = JsonSerializer.Deserialize<JsonElement[]>(result);
                    
                    if (transactions.Length > 0)
                    {
                        var latestTransaction = transactions[transactions.Length - 1];
                        var balance = latestTransaction.GetProperty("balance").GetDecimal();
                        return Json(new { balance });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }

            return Json(new { balance = 0 });
        }
    }
}
