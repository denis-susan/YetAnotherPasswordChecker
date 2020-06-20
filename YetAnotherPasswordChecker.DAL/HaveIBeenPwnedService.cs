using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestSharp;

namespace YetAnotherPasswordChecker.DAL
{
    #region Interface
    public interface IHaveIBeenPwnedService
    {
        /// <summary>
        /// Calls the haveibeenpwned.com api with the first 5 digits of a sha1 hashed password
        /// and retrieves a list of hashes.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        List<string> SearchByRange(string hash);
    }
    #endregion

    #region Implementation

    public class HaveIBeenPwnedService : IHaveIBeenPwnedService
    {
        public List<string> SearchByRange(string hash)
        {
            var baseUri = new Uri("https://api.pwnedpasswords.com");

            var client = new RestClient(baseUri);

            var request = new RestRequest(new Uri(baseUri, $"/range/{hash}"), Method.GET);

            var result = client.Get(request);

            return result.IsSuccessful ? result.Content.Split(Environment.NewLine).ToList() : new List<string>();

        }
    }

    #endregion
}