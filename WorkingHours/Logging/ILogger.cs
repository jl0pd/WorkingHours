namespace WorkingHours.Logging
{
    interface ILogger
    {
        void Trace<T>(string template, T value);
        
        void Debug<T>(string template, T value);
        
        void Info<T>(string template, T value);
        
        void Warn<T>(string template, T value);
     
        void Error<T>(string template, T value);
    
        void Fatal<T>(string template, T value);
    }
}
