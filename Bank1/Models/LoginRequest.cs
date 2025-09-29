namespace Bank1.Models
{
    public class LoginRequest
    {
        public string AccountName { get; set; } = string.Empty;
        public int Pincode { get; set; }
    }
}
