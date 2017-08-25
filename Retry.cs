/* 

Code by Chris Rock (chrisrock2@gmail.com)
a retry pattern for c# that uses Func<T> or Action

// to improve, you can catch a better exception 
 */

using System;
using System.Globalization;

namespace RetryPattern
{
    public static class Retry
    {
        public static T Do<T>(int numberOfRetries, Func<T> operation, int pauseBetweenExecutions = 500)
        {
            var currentTries = 0;

            do
            {
                try
                {
                    currentTries++;
                    return operation();
                }
                catch (Exception)
                {
                    if (currentTries == numberOfRetries)
                        throw;

                    System.Threading.Thread.Sleep(pauseBetweenExecutions);
                }
            } while (currentTries < numberOfRetries);

            throw new RetryException(string.Format(CultureInfo.InvariantCulture, "Retried operation '{0}' times without an exception", numberOfRetries));
        }

        public static void Do(int numberOfRetries, Action operation, int pauseBetweenExecutions = 500)
        {
            var currentTries = 0;

            do
            {
                try
                {
                    currentTries++;
                    operation();
                    return;
                }
                catch (Exception)
                {
                    if (currentTries == numberOfRetries)
                        throw;

                    System.Threading.Thread.Sleep(pauseBetweenExecutions);
                }
            } while (currentTries < numberOfRetries);

            throw new RetryException(string.Format(CultureInfo.InvariantCulture, "Retried operation '{0}' times without an exception", numberOfRetries));
        }
    }

    public class RetryException : Exception
    {
        public RetryException(string message) : base(message) { }
    }
}
