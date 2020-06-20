namespace YetAnotherPasswordChecker.Models
{
    public class PasswordErrorResponse 
    {
        public string Error { get; set; }
        public string Description { get; set; }

        public PasswordErrorResponse(string error, string description)
        {
            Error = error;
            Description = description;
        }
    }
}