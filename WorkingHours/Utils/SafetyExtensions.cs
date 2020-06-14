using System;

namespace WorkingHours.Utils
{
    public static class SafetyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static T ThrowIfNull<T>(this T? variable, string? variableName = null) where T : class
            => variable is null
                ? throw new ArgumentNullException(variableName ?? "Argument was null")
                : variable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static T ThrowIfNull<T>(this T? variable, string? variableName = null) where T : struct
            => variable is null
                ? throw new ArgumentNullException(variableName ?? "Argument was null")
                : variable.Value;
    }
}
