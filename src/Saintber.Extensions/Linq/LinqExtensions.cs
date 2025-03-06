namespace Saintber.Extensions.Linq
{
    /// <summary>
    /// LINQ/Lambda 擴充方法。
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 將序列的每個元素規劃成一個新的表單。
        /// </summary>
        /// <typeparam name="TSource">元素型別。</typeparam>
        /// <typeparam name="TResult">新元素型別。</typeparam>
        /// <param name="source">待叫用轉換函式的元素序列。</param>
        /// <param name="selector">要套用至每個元素的轉換函式。</param>
        /// <returns><see cref="IEnumerable{T}"/> 其項目是叫用每個項目的傳換函式的結果 <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException">方法 <paramref name="source"/> 或 <paramref name="selector"/> 為 <c>null</c>。</exception>
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            List<TResult> list = new List<TResult>();
            foreach (var item in source)
            {
                var result = await selector(item);
                list.Add(result);
            }
            return list;
        }

        /// <summary>
        /// 對序列的每個元素進行處理後再將序列回傳。
        /// </summary>
        /// <typeparam name="TSource">元素型別。</typeparam>
        /// <param name="source">待叫用處理函式的元素序列。</param>
        /// <param name="activator">要套用至每個元素的處理函式。</param>
        /// <returns>處理過的元素序列。</returns>
        /// <exception cref="ArgumentNullException">方法 <paramref name="source"/> 或 <paramref name="activator"/> 為 <c>null</c>。</exception>
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source
            , Action<TSource> activator)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            foreach (var item in source)
            {
                activator(item);
            }
            return source;
        }

        /// <summary>
        /// 對序列的每個元素進行處理後再將序列回傳。
        /// </summary>
        /// <typeparam name="TSource">元素型別。</typeparam>
        /// <param name="source">待叫用處理函式的元素序列。</param>
        /// <param name="activator">要套用至每個元素的處理函式。</param>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>處理過的元素序列。</returns>
        /// <exception cref="ArgumentNullException">方法 <paramref name="source"/> 或 <paramref name="activator"/> 為 <c>null</c>。</exception>
        public static async Task<IEnumerable<TSource>> ForEachAsync<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Task> activator, CancellationToken cancellationToken = default)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            foreach (var item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await activator(item);
            }
            return source;
        }
    }
}
