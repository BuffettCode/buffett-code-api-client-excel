namespace BuffettCodeCommon.Validator
{
    using System;

    /// <summary>
    /// Exception class for Validation Error.
    /// </summary>
    public class ValidationError : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        public ValidationError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ValidationError(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="inner">The inner<see cref="Exception"/>.</param>
        public ValidationError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
