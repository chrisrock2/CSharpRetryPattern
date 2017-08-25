/*

Code by Chris Rock (chrisrock2@gmail.com)
a retry pattern for c# that uses Func<T> or Action

*/
using System.IO;
using NUnit.Framework;

namespace RetryPattern.Tests
{
    [TestFixture]
    public class RetryTests
    {
        // these tests are conclusive
        // there are more to show you how to use the retry pattern
        [Test]
        public void TestRetryGetName()
        {
            var name = Retry.Do(3, () => GetName());
            Assert.That(name, Is.EqualTo("Chris"));
        }

        [Test]
        public void TestRetryFunctionWithException()
        {
            Assert.Throws<FileNotFoundException>(() => Retry.Do(3, ExecuteWithException));
        }
        
        [Test]
        public void TestReturnAction()
        {
            Assert.DoesNotThrow(() => Retry.Do(3, Execute));
        }
        
        [Test]
        public void TestGetNameWithException()
        {
            Assert.Throws<FileNotFoundException>(() => Retry.Do(3, () => GetNameWithException()));
        }

        public void Execute()
        {
            // do something
        }

        public void ExecuteWithException()
        {
            throw new FileNotFoundException();
        }
        
        public string GetName()
        {
            return "Chris";
        }

        public string GetNameWithException()
        {
            throw new FileNotFoundException();
        }
    }
}
