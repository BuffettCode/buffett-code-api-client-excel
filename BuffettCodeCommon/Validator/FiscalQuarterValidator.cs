using BuffettCodeCommon.Exception;
namespace BuffettCodeCommon.Validator
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FiscalQuarterValidator" />.
    /// </summary>
    public static class FiscalQuarterValidator
    {
        /// <summary>
        /// Defines the QUARTERS.
        /// </summary>
        private static readonly HashSet<uint> QUARTERS = new HashSet<uint> { 1, 2, 3, 4, 5 };

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="maybeFiscalQuarter">The quarter<see cref="uint"/>.</param>
        public static void Validate(uint maybeFiscalQuarter)
        {
            if (!QUARTERS.Contains(maybeFiscalQuarter))
            {
                throw new ValidationError($"{maybeFiscalQuarter} is not in {QUARTERS}");
            }
        }
    }
}