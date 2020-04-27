namespace ParquetRoller
{
    /// <summary>A value indicating success or the nature of the failure.</summary>
    /// <remarks>Returned when the application terminates to indicate results of the process.</remarks>
    /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes"/>
    public enum ExitCode : int
    {
        /// <summary>The operation completed successfully.</summary>
        Success = 0,
        /// <summary>An invalid function was specified.</summary>
        FileNotFound = 1,
        /// <summary>Access or permission was denied.</summary>
        AccessDenied = 5,
        /// <summary>Invalid data was given.</summary>
        InvalidData = 13,
        /// <summary>An unsupported request was made.</summary>
        NotSupported = 50,
        /// <summary>One or more arguments were incorrect.</summary>
        BadArguments = 160,
    }
}
