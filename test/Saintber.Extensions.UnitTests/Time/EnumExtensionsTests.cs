using Saintber.Extensions.EnumUtilities;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Saintber.Extensions.UnitTests.EnumUtilities
{
    [TestClass]
    public class EnumExtensionsTests
    {
        #region == TryParseEnum ==
        [TestMethod]
        public void TryParseEnum_FromSingleName_ShouldSucceed()
        {
            var success = "A".TryParseEnum(out SampleEnum result);
            Assert.IsTrue(success);
            Assert.AreEqual(SampleEnum.A, result);
        }

        [TestMethod]
        public void TryParseEnum_FromMultipleFlags_ShouldSucceed()
        {
            var success = "A,B".TryParseEnum(out SampleEnum result);
            Assert.IsTrue(success);
            Assert.AreEqual(SampleEnum.A | SampleEnum.B, result);
        }

        [TestMethod]
        public void TryParseEnum_FromNumber_ShouldSucceed()
        {
            var success = "3".TryParseEnum(out SampleEnum result);
            Assert.IsTrue(success);
            Assert.AreEqual(SampleEnum.A | SampleEnum.B, result);
        }

        [TestMethod]
        public void TryParseEnum_InvalidName_ShouldFail()
        {
            var success = "Invalid".TryParseEnum(out SampleEnum result);
            Assert.IsFalse(success);
            Assert.AreEqual(SampleEnum.None, result);
        }

        [TestMethod]
        public void TryParseEnum_EmptyInput_ShouldReturnDefault()
        {
            var success = default(string).TryParseEnum(out SampleEnum result);
            Assert.AreEqual(SampleEnum.None, result);
        }
        #endregion

        #region == ToDescription ==
        public const string SampleEnumADescription = "測試1";

        [TestMethod]
        public void ToDescription_DescriptionExists_ReturnsDescription()
        {
            var value = SampleEnum.A;
            var desc = value.ToDescription();

            Assert.AreEqual(SampleEnumADescription, desc);
        }

        [TestMethod]
        public void ToDescription_DescriptionMissing_ReturnsEnumName()
        {
            var value = SampleEnum.C;
            var desc = value.ToDescription();

            Assert.AreEqual(nameof(SampleEnum.C), desc);
        }

        [TestMethod]
        public void ToDescription_MultipleFlags_IgnoresCombined() // optional behavior notice
        {
            var value = SampleEnum.A | SampleEnum.B;
            var desc = value.ToDescription();

            // 預設 .NET 不支援 Flags 組合描述，會得到 value 的數值
            Assert.AreEqual(Convert.ToInt32(value).ToString(), desc);
        }
        #endregion
    }

    public enum SampleEnum
    {
        [Description("無資料")]
        None = 0,

        [Description(EnumExtensionsTests.SampleEnumADescription)]
        A = 1 << 0,

        [Description("測試2")]
        B = 1 << 1,

        C = 1 << 2
    }
}
