namespace YetAnotherPasswordChecker.BLL.Interfaces
{
    public interface IPasswordRule
    {
        /// <summary>
        /// This will check the <see cref="input"/> against a password pattern and determine if the <see cref="input"/> passes.
        /// </summary>
        /// <param name="input">The string to check against</param>
        /// <returns>true if rule passed, false if not</returns>
        bool Check(string input);

        /// <summary>
        /// The level of participation in the final score
        /// </summary>
        decimal Importance { get; set; }
    }
}