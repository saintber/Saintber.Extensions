using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Saintber.Extensions.DependencyInjection
{
    public static class ServiceCollectionServiceExtensions
    {
        /// <summary>
        /// 註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static IServiceCollection AddTransientAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.AddTransient<TService>(provider => provider.GetRequiredService<TImplementation>());

        /// <summary>
        /// 嘗試註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static void TryAddTransientAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.TryAddTransient<TService>(provider => provider.GetRequiredService<TImplementation>());

        /// <summary>
        /// 註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static IServiceCollection AddScopedAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.AddScoped<TService>(provider => provider.GetRequiredService<TImplementation>());

        /// <summary>
        /// 嘗試註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static void TryAddScopedAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.TryAddScoped<TService>(provider => provider.GetRequiredService<TImplementation>());

        /// <summary>
        /// 註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static IServiceCollection AddSingletonAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.AddSingleton<TService>(provider => provider.GetRequiredService<TImplementation>());

        /// <summary>
        /// 嘗試註冊一個已存在的具體實作 <typeparamref name="TImplementation"/> 的別名 <typeparamref name="TService"/>。
        /// </summary>
        /// <typeparam name="TService">要註冊的服務介面型別。</typeparam>
        /// <typeparam name="TImplementation">已註冊的具體實作型別。</typeparam>
        /// <param name="services">註冊服務的集合。</param>
        /// <returns>註冊服務的集合。</returns>
        public static void TryAddSingletonAlias<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : TService
            => services.TryAddSingleton<TService>(provider => provider.GetRequiredService<TImplementation>());
    }
}
