![.NET](https://img.shields.io/badge/.NET-6%2F8%2F9-blue)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
![NuGet](https://img.shields.io/nuget/v/Saintber.Extensions.svg)

> English | [中文說明](#中文說明)

---

# 📘 English

## Overview

**Saintber.Extensions** is a collection of commonly used C# extension methods designed for the Saintber system architecture. It includes the following namespaces:

- DependencyInjection: Extension methods for dependency injection
- Linq: Utility extensions for LINQ operations
- Time: Extensions for time and timezone handling
- Transactions: Helpers for managing transactional workflows

## Installation

```bash
dotnet add package Saintber.Extensions
```

## 📝 License

This package is licensed under the [MIT License](LICENSE).

## 🌐 Repository

https://github.com/saintber/Saintber.Abstractions

## Extension Library Overview
### DependencyInjection
- `ServiceCollectionExtensions` - Provides extension methods for `IServiceCollection`
	- Register existing instances as specified interfaces (named registration)

### Linq
- `LinqExtensions` - Provides extension methods for LINQ and Lambda operations
	- Asynchronous version of `Select`
	- `ForEach` supporting Lambda expressions
	- Asynchronous version of `ForEach` supporting Lambda expressions

### Time
- `DateTimeOffsetExtensions` - Provides timezone conversion extension methods for `DateTimeOffset`
	- Convert `DateTimeOffset` to specified timezone
	- Support extracting user timezone from HTTP headers

### Transactions
- `TransactionExtensions` - Provides extension methods for creating and controlling `TransactionScope`
	- Create standard transaction scopes with specified timeout and auto-commit behavior (supports async)
---

# 📙 中文說明

## 簡介


**Saintber.Extensions** 是一組常用的 C# 擴充方法集合，設計於 Saintber 系統架構中，涵蓋以下命名空間：

- `DependencyInjection`：相依性注入相關擴充方法  
- `Linq`：LINQ 操作的輔助擴充方法  
- `Time`：時間與時區相關的擴充方法  
- `Transactions`：交易流程輔助擴充方法

## 安裝方式

```bash
dotnet add package Saintber.Extensions
```

## 📝 許可條款

本套件採用 [MIT 許可條款](LICENSE)。

## 🌐 原始碼庫

https://github.com/saintber/Saintber.Extensions

## 擴充函式庫介紹
### DependencyInjection
- `ServiceCollectionExtensions` - 提供 IServiceCollection 的擴充方法
	- 註冊現有實例為指定介面（具名註冊）

### Linq
- `LinqExtensions` - 提供 LINQ 與 Lambda 操作的擴充方法
	- 非同步版 `Select`
	- 支援 Lambda 表達式的 `ForEach`
	- 支援 Lambda 表達式的非同步版 `ForEach`

### Time 命名空間
- `DateTimeOffsetExtensions` - 提供 `DateTimeOffset` 相關的時區轉換擴充方法
	- 將 DateTimeOffset 轉換為指定的時區時間
	- 支援從 HTTP Header 擷取使用者時區

### Transactions 命名空間
- `TransactionExtensions` - 提供 `TransactionScope` 建立與控制的擴充方法
	- 建立具指定逾時與自動 Commit 行為的標準交易範圍（支援非同步）