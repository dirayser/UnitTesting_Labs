using System;
using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using IIG.BinaryFlag;
using IIG.DatabaseConnectionUtils;

namespace Tests_Lab4
{
    [Collection("Sequential")]
    public class Hasher_Tests
    {
        private const string Server = @"DESKTOP-BFF3755";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = true;
        private const string Login = @"sa";
        private const string Password = @"7129";
        private const int ConnectionTimeout = 75;
        private AuthDatabaseUtils db = new AuthDatabaseUtils(
            Server,
            Database,
            IsTrusted,
            Login,
            Password,
            ConnectionTimeout
            );

        [Fact]
        public void Test_TypicalData()
        {
            string login = "usualLogin";
            string password = PasswordHasher.GetHash("usualPassword");
            Assert.True(db.AddCredentials(login, password));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_NumData()
        {
            string login = "12345";
            string password = PasswordHasher.GetHash("6789");
            db.AddCredentials(login, password);
            Assert.True(db.CheckCredentials(login, password));
            db.DeleteCredentials(login, password);
        }

        [Fact]
        public void Test_EmptyData()
        {
            string login = "";
            string password = PasswordHasher.GetHash("");
            Assert.False(db.AddCredentials(login, password));
            Assert.False(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_CyrillicData()
        {
            string login = "Ты рагуль";
            string password = PasswordHasher.GetHash("Да");
            Assert.True(db.AddCredentials(login, password));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_AnimeData()
        {
            string login = "日本語";
            string password = PasswordHasher.GetHash("象は鼻が長い");
            Assert.True(db.AddCredentials(login, password));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_SameData()
        {
            string login = "someData";
            string password = PasswordHasher.GetHash("someMoreData");
            Assert.True(db.AddCredentials(login, password));
            Assert.False(db.AddCredentials(login, password));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_SameLogin() // 2.1
        {
            string login = "someData";
            string password = PasswordHasher.GetHash("someMoreData");
            string password2 = PasswordHasher.GetHash("someMoreData2");
            Assert.True(db.AddCredentials(login, password));
            Assert.False(db.AddCredentials(login, password2));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
        }

        [Fact]
        public void Test_UsedLogin() // 2.2
        {
            string login = "someData";
            string login2 = "someData2";
            string password = PasswordHasher.GetHash("someMoreData");
            string password2 = PasswordHasher.GetHash("someMoreData2");
            Assert.True(db.AddCredentials(login, password));
            Assert.True(db.AddCredentials(login2, password2));
            Assert.False(db.UpdateCredentials(login, password, login2, password2));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(login, password));
            Assert.True(db.DeleteCredentials(login2, password2));
        }

        [Fact]
        public void Test_UpdateData()
        {
            string login = "old";
            string newLogin = "new";
            string password = PasswordHasher.GetHash("oldP");
            string newPassword = PasswordHasher.GetHash("newP");
            Assert.True(db.AddCredentials(login, password));
            Assert.True(db.CheckCredentials(login, password));
            Assert.True(db.UpdateCredentials(login, password, newLogin, newPassword));
            Assert.True(db.CheckCredentials(newLogin, newPassword));
            Assert.False(db.CheckCredentials(login, password));
            Assert.True(db.DeleteCredentials(newLogin, newPassword));
            Assert.False(db.CheckCredentials(newLogin, newPassword));
        }
    }

    [Collection("Sequential")]
    public class Flag_Tests_Expect_True
    {
       const string path = "C:/Users/diray/Documents/forLab.txt";

        [Fact]
        public void Test_InitTrue()
        {
            MultipleBinaryFlag testBinaryFlag = new MultipleBinaryFlag(3);
            bool? condition = testBinaryFlag.GetFlag();
            Assert.True(condition);
            BaseFileWorker.Write(condition.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
        }

        [Fact]
        public void Test_GetFlag_DefaultValue_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
            Assert.True(result);
        }


        [Fact]
        public void Test_GetFlag_Changed_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.SetFlag(0);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
            Assert.True(result);
        }

        [Fact]
        public void Test_GetFlag_Changed_Twice_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.SetFlag(0);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
            Assert.True(result);
        }

        [Fact]
        public void Test_GetFlag_DefaultFalseValue_FullSet_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            multipleBinaryFlag.SetFlag(1);
            multipleBinaryFlag.SetFlag(0);
            multipleBinaryFlag.SetFlag(2);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
            Assert.True(result);
        }

        [Fact]
        public void Test_GetFlag_Disposed_ReAssigned_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            multipleBinaryFlag = new MultipleBinaryFlag(3);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.TrueString, fromFile);
            Assert.True(result);
        }
    }

    [Collection("Sequential")]
    public class FlagTests_Expect_False
    {
       const string path = "C:/Users/diray/Documents/forLab.txt";

        [Fact]
        public void Test_GetFlag_DefaultFalseValue_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.FalseString, fromFile);
            Assert.False(result);
        }


        [Fact]
        public void Test_GetFlag_DefaultFalseValue_Changed_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            multipleBinaryFlag.SetFlag(1);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.FalseString, fromFile);
            Assert.False(result);
        }


        [Fact]
        public void Test_GetFlag_DefaultTrueValue_Changed_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.ResetFlag(1);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(Boolean.FalseString, fromFile);
            Assert.False(result);
        }
    }

    [Collection("Sequential")]
    public class FlagTests_Expect_Null
    {
        const string path = "C:/Users/diray/Documents/forLab.txt";

        [Fact]
        public void Test_GetFlag_Disposed_Null()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(string.Empty, fromFile);
            Assert.Null(result);
        }

        [Fact]
        public void Test_GetFlag_Disposed_SetValue_Null()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            multipleBinaryFlag.SetFlag(0);
            multipleBinaryFlag.SetFlag(1);
            multipleBinaryFlag.SetFlag(2);
            var result = multipleBinaryFlag.GetFlag();
            BaseFileWorker.Write(result.ToString(), path);
            string fromFile = BaseFileWorker.ReadAll(path);
            Assert.Equal(string.Empty, fromFile);
            Assert.Null(result);
        }
    }

}
