using System;

public class DominationException : Exception
{
    public DominationException() { }
    public DominationException(string message) : base(message) { }
    public DominationException(string message, Exception inner) : base(message, inner) { }
} 