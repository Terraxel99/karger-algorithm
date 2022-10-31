namespace Utilities.Exceptions
{
    /// <summary>
    /// Defines an invalid graph structure.
    /// </summary>
    internal class InvalidGraphDefinitionException : Exception
    {
        /// <summary>
        /// Creates an instance of the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidGraphDefinitionException(string? message, Exception? innerException) 
            : base(message, innerException) { }
    }
}
