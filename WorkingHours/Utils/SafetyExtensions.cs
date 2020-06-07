using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

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
        public static T ThrowIfNull<T>(this T? variable, string? variableName = null) where T : class =>
            variable switch
            {
                null => throw new ArgumentNullException(variableName ?? "Argument was null"),
                _ => variable
            };
    }
}
