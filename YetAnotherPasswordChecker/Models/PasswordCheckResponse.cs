namespace YetAnotherPasswordChecker.Models
{
    public class PasswordCheckResponse
    {
        public int PwdStrength { get; set; }
        public string PwdStrengthDescription { get; set; }
        public bool IsPwned { get; set; }
    }
}