using System;
using Xunit;

namespace IIG.BinaryFlag
{
    public class FlagTests_Expect_True
    {
        [Fact]
        public void Test_GetFlag_DefaultValue_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            Assert.True(multipleBinaryFlag.GetFlag());
        }


        [Fact]
        public void Test_GetFlag_Changed_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.SetFlag(0);
            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_Changed_Twice_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.ResetFlag(0);
            multipleBinaryFlag.SetFlag(0);
            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_DefaultFalseValue_FullSet_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            multipleBinaryFlag.SetFlag(1);
            multipleBinaryFlag.SetFlag(0);
            multipleBinaryFlag.SetFlag(2);
            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_Disposed_ReAssigned_True()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            multipleBinaryFlag = new MultipleBinaryFlag(3);
            Assert.True(multipleBinaryFlag.GetFlag());
        }
    }

    public class FlagTests_Expect_False
    {

        [Fact]
        public void Test_GetFlag_DefaultFalseValue_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            Assert.False(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_DefaultTrueValue_Changed_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            multipleBinaryFlag.ResetFlag(1);
            Assert.False(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_DefaultFalseValue_Changed_False()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3, false);
            multipleBinaryFlag.SetFlag(1);
            Assert.False(multipleBinaryFlag.GetFlag());
        }

    }

    public class FlagTests_Expect_Null
    {
        [Fact]
        public void Test_GetFlag_Disposed_Null()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            Assert.Null(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void Test_GetFlag_Disposed_SetValue_Null()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            multipleBinaryFlag.Dispose();
            multipleBinaryFlag.SetFlag(0);
            multipleBinaryFlag.SetFlag(1);
            multipleBinaryFlag.SetFlag(2);
            Assert.Null(multipleBinaryFlag.GetFlag());
        }
    }

    public class FlagTests_Expect_Throw
    {
        [Fact]
        public void Test_ResetFlag_Constructor_ZeroLength_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(0); });
        }

        [Fact]
        public void Test_ResetFlag_Constructor_OutOfRangeSmaller_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(1); });
        }

        [Fact]
        public void Test_ResetFlag_Constructor_OutOfRangeBigger_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { bool? multipleBinaryFlag = new MultipleBinaryFlag(17179868705).GetFlag(); });
        }

        [Fact]
        public void Test_ResetFlag_OutOfRange_Throws()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { multipleBinaryFlag.ResetFlag(3); });
        }

        [Fact]
        public void Test_SetFlag_OutOfRange_Throws()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(3);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => { multipleBinaryFlag.SetFlag(3); });
        }
    }
}
