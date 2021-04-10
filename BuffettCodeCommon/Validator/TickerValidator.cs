namespace BuffettCodeCommon.Validator
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="JpTickerValidator" />.
    /// </summary>
    public static class JpTickerValidator
    {
        // jp ticker: 1000 - 9999
        /// <summary>
        /// Defines the TICKER_PATTTERN.
        /// </summary>
        private readonly static Regex TICKER_PATTTERN = new Regex("^[1-9][0-9]{3}$", RegexOptions.Compiled);

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="maybeTicker">The maybeTicker<see cref="string"/>.</param>
        public static void Validate(string maybeTicker)
        {
            if (!TICKER_PATTTERN.IsMatch(maybeTicker))
            {
                throw new ValidationError($"ticker must be '1000' - '9999', but {maybeTicker} is given");
            }
        }
    }
}