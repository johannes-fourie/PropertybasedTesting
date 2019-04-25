using System;
using System.Diagnostics;

namespace PropertybasedTesting
{
    public static class Test
    {
        public static void Check(
            Func<bool> when,
            Func<bool> then,
            string message)
        {
            if (when() && !then())
            {
                Debug.WriteLine(message);
                throw new CheckFailException(message);
            }
        }
    }
}