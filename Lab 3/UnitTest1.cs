using System;
using Xunit;

namespace IIG.PasswordHashingUtils
{
    [Collection("Sequential")]
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

    [Collection("Sequential")]
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

    [Collection("Sequential")]
    public class Main_Tests
    {
        [Fact]
        public void Route_0_1()
        {
            var hash = PasswordHasher.GetHash("password", "");
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4()
        {
            var hash = PasswordHasher.GetHash("password", "salt");
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("password", dp);
            Assert.NotNull(hash);
        }
        [Fact]
        public void Route_0_1_5()
        {
            var hash = PasswordHasher.GetHash("password", "", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_1_6()
        {
            var hash = PasswordHasher.GetHash("password", "", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_5()
        {
            var hash = PasswordHasher.GetHash("password", "salt", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_5()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("password", dp, 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_6()
        {
            var hash = PasswordHasher.GetHash("password", "salt", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_6()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("password", dp, 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_1_5_10_11()
        {
            var hash = PasswordHasher.GetHash(null, "", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_1_6_10_11()
        {
            var hash = PasswordHasher.GetHash(null, "", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_5_10_11()
        {
            var hash = PasswordHasher.GetHash(null, "salt", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_5_10_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash(null, dp, 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_6_10_11()
        {
            var hash = PasswordHasher.GetHash(null, "salt", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_6_10_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash(null, dp, 9310);
            Assert.NotNull(hash);
        }

  
        [Fact]
        public void Route_0_1_5_7_8_9_11()
        {
            var hash = PasswordHasher.GetHash(null, "", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_1_6_7_8_9_11()
        {
            var hash = PasswordHasher.GetHash("password", "", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_5_7_8_9_11()
        {
            var hash = PasswordHasher.GetHash("password", "salt", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_5_7_8_9_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("password", dp, 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_6_7_8_9_11()
        {
            var hash = PasswordHasher.GetHash("password", "salt", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_6_7_8_9_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("password", dp, 9310);
            Assert.NotNull(hash);
        }


        [Fact]
        public void Route_0_1_5_7_9_11()
        {
            var hash = PasswordHasher.GetHash(null, "", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_1_6_7_9_11()
        {
            var hash = PasswordHasher.GetHash("î", "", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_5_7_9_11()
        {
            var hash = PasswordHasher.GetHash("î", "salt", 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_5_7_9_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("î", dp, 0);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_3_4_6_7_9_11()
        {
            var hash = PasswordHasher.GetHash("î", "salt", 9310);
            Assert.NotNull(hash);
        }

        [Fact]
        public void Route_0_2_4_6_7_9_11()
        {
            string dp = "î";
            var hash = PasswordHasher.GetHash("î", dp, 9310);
            Assert.NotNull(hash);
        }

    }
}
