using System.Collections.Generic;

namespace Testing
{
    public class AClass
    {
        public enum Time
        {
            Morning,
            Afternoon,
            Evening
        }

        public List<int> Levels { get; } = new List<int>() { -10, -5, 0, 1, 30 };

        public (string m, double d) SomeCode(bool b, Time t, string u, int l, int i, int j)
        {
            string message;
            double tax;

            message = "fff";
            tax = 50.4;

            return (m: message, d: tax);
        }

        public static class University
        {
            public static readonly string CaliforniaBerkeley = "University of California–Berkeley";
            public static readonly string Cambridge = "University of Cambridge";
            public static readonly string Harvard = "Harvard University";
        }
    }
}