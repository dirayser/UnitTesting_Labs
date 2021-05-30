using System;
using Xunit;

namespace IIG.PasswordHashingUtils
{
    public class Test_Equals_ExpectTrue
    {
        [Fact]
        public void Test_samePassword_defaultSalt()
        {
            PasswordHasher.Init("some salt", 0);
            var hash = PasswordHasher.GetHash("123");
            var hash2 = PasswordHasher.GetHash("123");
            Assert.Equal(hash, hash2);
        }

        [Fact]
        public void Test_samePassword_sameSalt()
        {
            PasswordHasher.Init("some salt", 12345);
            var hash = PasswordHasher.GetHash("123");
            var hash2 = PasswordHasher.GetHash("123");
            Assert.Equal(hash, hash2);
        }

        [Fact]
        public void Test_samePassword_sameSalt_defaultAdler()
        {
            PasswordHasher.Init("some salt", 1);
            var hash = PasswordHasher.GetHash("123");
            var hash2 = PasswordHasher.GetHash("123");
            Assert.Equal(hash, hash2);
        }

        [Fact]
        public void Test_samePassword_sameSalt_sameAdler()
        {
            PasswordHasher.Init("some salt", 1234);
            var hash = PasswordHasher.GetHash("123");
            var hash2 = PasswordHasher.GetHash("123");
            Assert.Equal(hash, hash2);
        }
    }

    public class Test_Equals_ExpectFalse
    {
        [Fact]
        public void Test_samePassword_differentSalt()
        {
            PasswordHasher.Init(null, 1);
            var hash = PasswordHasher.GetHash("123", "salt1");
            var hash2 = PasswordHasher.GetHash("123", "salt2");
            Assert.NotEqual(hash, hash2);
        }

        [Fact]
        public void Test_sameSalt_differentPasswords()
        {
            PasswordHasher.Init(null, 1);
            var hash = PasswordHasher.GetHash("12345", "salt1");
            var hash2 = PasswordHasher.GetHash("123", "salt1");
            Assert.NotEqual(hash, hash2);
        }

        [Fact]
        public void Test_sameSaltwithSpace_samePasswords()
        {
            PasswordHasher.Init(null, 1);
            var hash = PasswordHasher.GetHash("12345", " salt");
            var hash2 = PasswordHasher.GetHash("12345", "salt");
            Assert.NotEqual(hash, hash2);
        }
    }
}
