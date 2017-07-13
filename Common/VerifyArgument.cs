using System;
using System.Diagnostics;

namespace SSM.Common
{/// <summary>
    /// From http://kigg.codeplex.com/SourceControl/changeset/view/18277#346723
    /// </summary>
    public class Verify
    {
        internal Verify()
        {
        }

        public class Argument
        {
            internal Argument()
            {
            }

            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                    throw new ArgumentException(argumentName + " cannot be empty guid.", argumentName);
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                    throw new ArgumentException(argumentName + " cannot be empty.", argumentName);
            }

            [DebuggerStepThrough]
            public static void IsWithinLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                    throw new ArgumentException(argumentName + " cannot be more than " + length + " characters", argumentName);
            }

            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                    throw new ArgumentNullException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(int argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }


            [DebuggerStepThrough]
            public static void IsPositive(int argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }


            [DebuggerStepThrough]
            public static void IsPositiveOrZero(long argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositive(long argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsPositive(float argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(decimal argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositive(decimal argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

        }
    }
}