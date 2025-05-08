using Microsoft.AspNetCore.Http;

namespace Saintber.Extensions.Time
{
    /// <summary>
    /// 擴充 <see cref="DateTimeOffset"/> 的方法，提供將 UTC 時間轉換為指定時區的本地時間表示的功能。
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// 將 <see cref="DateTimeOffset"/> 表示的絕對時間轉換為指定時區的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="timeZone">目標時區物件，若為 <c>null</c> 則返回原始時間。</param>
        /// <returns>轉換為指定時區的 <see cref="DateTimeOffset"/>。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, TimeZoneInfo? timeZone)
            => timeZone == null ? dateTime : dateTime.ToOffset(timeZone.GetUtcOffset(dateTime.UtcDateTime));

        /// <summary>
        /// 將 <see cref="DateTimeOffset"/> 表示的絕對時間轉換為指定時區的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="timeZoneId">IANA 或 Windows 的時區名稱，例如 "Asia/Taipei"。</param>
        /// <returns>轉換為指定時區的 <see cref="DateTimeOffset"/>。若找不到指定時區，則返回原始時間。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, string? timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                return dateTime;

            if (timeZoneId.Trim().Equals("UTC", StringComparison.OrdinalIgnoreCase))
                return dateTime.ToUniversalTime();

            if (timeZoneId.Trim().Equals("Local", StringComparison.OrdinalIgnoreCase))
                return dateTime.ToLocalTime();

            try
            {
                return dateTime.ToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            }
            catch (TimeZoneNotFoundException) { return dateTime; }
            catch (InvalidTimeZoneException) { return dateTime; }
        }

        /// <summary>
        /// 將 <see cref="DateTimeOffset"/> 表示的絕對時間轉換為指定時區的本地時間表示，若未指定則使用預設時區。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="timeZoneId">IANA 或 Windows 的時區名稱。</param>
        /// <param name="defaultTimeZoneId">若 <paramref name="timeZoneId"/> 為空則使用此預設時區。</param>
        /// <returns>轉換後的 <see cref="DateTimeOffset"/>。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, string? timeZoneId, string defaultTimeZoneId)
            => string.IsNullOrWhiteSpace(timeZoneId) && !string.IsNullOrWhiteSpace(defaultTimeZoneId) ? dateTime.ToTimeZone(defaultTimeZoneId)
                : dateTime.ToTimeZone(timeZoneId);

        /// <summary>
        /// 從 <see cref="HttpRequest"/> 的 Header 中取得時區名稱，將 <see cref="DateTimeOffset"/> 表示的絕對時間轉換為對應的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="httpRequest">HttpRequest 實例，若為 <c>null</c> 則返回原始時間。</param>
        /// <param name="headerName">Header 名稱，內容應為時區 ID（例如 "Asia/Taipei"）。</param>
        /// <returns>轉換後的 <see cref="DateTimeOffset"/>。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, HttpRequest? httpRequest, string headerName)
            => dateTime.ToTimeZone(httpRequest?.Headers[headerName].ToString());

        /// <summary>
        /// 從 <see cref="HttpRequest"/> 的 Header 中取得時區名稱，若無指定則使用預設值，將 <see cref="DateTimeOffset"/> 轉換為對應的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="httpRequest">HttpRequest 實例。</param>
        /// <param name="headerName">Header 名稱，應包含時區 ID。</param>
        /// <param name="defaultTimeZoneId">Header 未提供或無效時使用的預設時區。</param>
        /// <returns>轉換後的 <see cref="DateTimeOffset"/>。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, HttpRequest? httpRequest, string headerName, string defaultTimeZoneId)
            => dateTime.ToTimeZone(httpRequest?.Headers[headerName].ToString(), defaultTimeZoneId);

        /// <summary>
        /// 從 <see cref="IHttpContextAccessor"/> 中的 <see cref="HttpRequest"/> Header 取得時區，並將 <see cref="DateTimeOffset"/> 轉換為對應的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="httpContextAccessor">HttpContextAccessor 實例。</param>
        /// <param name="headerName">Header 名稱，應包含時區 ID。</param>
        /// <returns>轉換後的 <see cref="DateTimeOffset"/>。若 <see cref="HttpContext"/> 為 <c>null</c> 則返回原始時間。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, IHttpContextAccessor httpContextAccessor, string headerName)
            => dateTime.ToTimeZone(httpContextAccessor.HttpContext?.Request, headerName);

        /// <summary>
        /// 從 <see cref="IHttpContextAccessor"/> 的 <see cref="HttpRequest"/> Header 取得時區，若未指定則使用預設值，將 <see cref="DateTimeOffset"/> 轉換為對應的本地時間表示。
        /// </summary>
        /// <param name="dateTime">原始的 <see cref="DateTimeOffset"/> 時間（包含原始 Offset）。</param>
        /// <param name="httpContextAccessor">HttpContextAccessor 實例。</param>
        /// <param name="headerName">Header 名稱，應包含時區 ID。</param>
        /// <param name="defaultTimeZoneId">若未指定 Header 或無效時使用的預設時區。</param>
        /// <returns>轉換後的 <see cref="DateTimeOffset"/>。</returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTime, IHttpContextAccessor httpContextAccessor, string headerName, string defaultTimeZoneId)
            => dateTime.ToTimeZone(httpContextAccessor.HttpContext?.Request, headerName, defaultTimeZoneId);
    }
}
