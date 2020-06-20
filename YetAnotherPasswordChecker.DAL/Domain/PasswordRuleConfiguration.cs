namespace YetAnotherPasswordChecker.DAL.Domain
{
    public class PasswordRuleConfiguration
    {
        public int Id { get; set; }
        public PasswordRule Rule { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}