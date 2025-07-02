using System.ComponentModel;
using System.Reflection;

namespace Saintber.Extensions.EnumUtilities
{
    /// <summary>
    /// 提供列舉型別（Enum）的解析擴充方法。
    /// 支援從字串解析為 enum 名稱，或整數值對應的 enum 成員。
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 嘗試將指定字串解析為列舉型別成員。
        /// 支援解析 enum 名稱（大小寫不敏感）或整數字串（包含 Flags 組合值）。
        /// </summary>
        /// <typeparam name="TEnum">目標列舉型別。</typeparam>
        /// <param name="input">輸入字串，可能為 enum 名稱或對應數值。</param>
        /// <param name="obj">若解析成功，傳回對應的 enum 成員；否則為預設值。</param>
        /// <returns>若成功解析為指定 enum 型別則為 true，否則為 false。</returns>
        public static bool TryParseEnum<TEnum>(this string? input, out TEnum obj)
            where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                obj = default;
                return false;
            }

            // 若為整數字串，轉換為 enum 成員
            if (long.TryParse(input, out var num))
            {
                obj = (TEnum)Enum.ToObject(typeof(TEnum), num);
                return true;
            }

            // 否則嘗試解析 enum 名稱（含 Flags）
            if (Enum.TryParse<TEnum>(input, ignoreCase: true, out var result))
            {
                obj = result;
                return true;
            }

            obj = default;
            return false;
        }

        /// <summary>
        /// 取得列舉成員上標註的 <see cref="DescriptionAttribute"/> 描述文字。
        /// 若未標註 Description，則回傳列舉成員名稱。
        /// </summary>
        /// <param name="value">列舉成員。</param>
        /// <returns>對應的描述文字，或列舉成員名稱。</returns>
        public static string ToDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
