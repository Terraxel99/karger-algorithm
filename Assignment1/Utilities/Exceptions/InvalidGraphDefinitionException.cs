namespace Utilities.Exceptions
{
    internal class InvalidGraphDefinitionException : Exception
    {
        public InvalidGraphDefinitionException(string? message, Exception? innerException) 
            : base(message, innerException) { }
    }
}
