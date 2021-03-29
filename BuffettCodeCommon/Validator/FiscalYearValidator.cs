namespace BuffettCodeCommon.Validator
{
    /// <summary>
    /// Defines the <see cref="FiscalYearValidator" />.
    /// </summary>
    public class FiscalYearValidator
    {
        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="maybeFiscalYear">The fiscalYear<see cref="uint"/>.</param>
        public static void Validate(uint maybeFiscalYear)
        {
            if (maybeFiscalYear < 1900)
            {
                throw new ValidationError($"fiscalYear={maybeFiscalYear} is too small.");
            }
            else if (maybeFiscalYear > 2100)
            {
                throw new ValidationError($"fiscalYear={maybeFiscalYear} is too large.");
            }
        }
    }
}
