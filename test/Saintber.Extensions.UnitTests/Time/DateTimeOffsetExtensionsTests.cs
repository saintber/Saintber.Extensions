using Microsoft.AspNetCore.Http;
using Saintber.Extensions.Time;

namespace Saintber.Extensions.UnitTests.Time
{
    [TestClass]
    public class DateTimeOffsetExtensionsTests
    {
        private static readonly DateTimeOffset SampleTime = new DateTimeOffset(2025, 5, 5, 12, 0, 0, TimeSpan.Zero);

        [DataTestMethod]
        [DataRow("Asia/Taipei", 8)]
        [DataRow("UTC", 0)]
        [DataRow("Europe/London", 1)] // 5 月有夏令時間（+1）
        public void ToTimeZone_Should_ConvertOffsetCorrectly(string timeZoneId, int expectedOffsetHours)
        {
            var result = SampleTime.ToTimeZone(timeZoneId);

            Assert.AreEqual(expectedOffsetHours, result.Offset.Hours);
            Assert.AreEqual(SampleTime.UtcDateTime, result.UtcDateTime);
        }

        [TestMethod]
        public void ToTimeZone_InvalidTimeZoneId_Should_ReturnOriginal()
        {
            var result = SampleTime.ToTimeZone("Invalid/Zone");

            Assert.AreEqual(SampleTime, result);
        }

        [TestMethod]
        public void ToTimeZone_NullOrEmpty_Should_ReturnOriginal()
        {
            Assert.AreEqual(SampleTime, SampleTime.ToTimeZone((string?)null));
            Assert.AreEqual(SampleTime, SampleTime.ToTimeZone(""));
            Assert.AreEqual(SampleTime, SampleTime.ToTimeZone("   "));
        }

        [TestMethod]
        public void ToTimeZone_WithFallback_Should_UseDefaultIfMissing()
        {
            var result = SampleTime.ToTimeZone(default(string), "Asia/Tokyo");

            Assert.AreEqual(9, result.Offset.Hours);
        }

        [TestMethod]
        public void ToTimeZone_FromHttpRequest_Should_ParseHeaderCorrectly()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["X-TimeZone"] = "Asia/Taipei";

            var result = SampleTime.ToTimeZone(context.Request, "X-TimeZone");

            Assert.AreEqual(8, result.Offset.Hours);
        }

        [TestMethod]
        public void ToTimeZone_FromHttpContextAccessor_Should_UseCorrectHeader()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["TimeZone"] = "Asia/Tokyo";

            var accessor = new HttpContextAccessor
            {
                HttpContext = context
            };

            var result = SampleTime.ToTimeZone(accessor, "TimeZone");

            Assert.AreEqual(9, result.Offset.Hours);
        }
    }
}
