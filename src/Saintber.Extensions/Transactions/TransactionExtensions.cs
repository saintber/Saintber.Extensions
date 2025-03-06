using System.Transactions;

namespace Saintber.Extensions.Transactions
{
    public static class TransactionExtensions
    {
        /// <summary>
        /// 啟動交易以執行函數。
        /// </summary>
        /// <typeparam name="T">函數回傳型別。</typeparam>
        /// <param name="func">執行函數。</param>
        /// <param name="timeout">交易逾時時間。</param>
        /// <returns>非同步作業。</returns>
        public static async Task<T> TransactionAsync<T>(this TimeSpan timeout, Func<Task<T>> func)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead, Timeout = timeout }
                , TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await func();
                ts.Complete();
                return response;
            }
        }

        /// <summary>
        /// 啟動交易以執行函數。
        /// </summary>
        /// <param name="func">執行函數。</param>
        /// <param name="timeout">交易逾時時間。</param>
        /// <returns>非同步作業。</returns>
        public static async Task TransactionAsync(this TimeSpan timeout, Func<Task> func)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead, Timeout = timeout }
                , TransactionScopeAsyncFlowOption.Enabled))
            {
                await func();
                ts.Complete();
            }
        }
    }
}
