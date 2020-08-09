# CoinMarketCapNet
[![AppVeyor](https://img.shields.io/appveyor/build/MohammedBoukhlouf/coinmarketcapnet?style=flat-square)](https://ci.appveyor.com/project/MohammedBoukhlouf/coinmarketcapnet)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/M-Boukhlouf/CoinMarketCapNet?style=flat-square)](https://github.com/M-Boukhlouf/CoinMarketCapNet/releases/latest) 
[![Nuget](https://img.shields.io/nuget/v/CoinMarketCapNet?style=flat-square)](https://www.nuget.org/packages/CoinMarketCapNet) 
[![GitHub](https://img.shields.io/github/license/M-Boukhlouf/CoinMarketCapNet?style=flat-square)](https://github.com/M-Boukhlouf/CoinMarketCapNet/blob/master/LICENSE)

A wrapper in .NET for CoinMarketCap API

## Installation
Via Nuget:
```
Install-Package CoinMarketCapNet
```

## Example

```csharp
var client = new CoinMarketCapClient("<api-key>");

// Get mapping of all cryptocurrencies
var cryptocurrencies = await client.GetCoinMarketCapIdMapAsync();

// Get mapping of the first 10 of cryptocurrencies
var cryptocurrenciesPage1 = await client.GetCoinMarketCapIdMapAsync(new { start = 1, limit = 10 });

// Get latest listings
var listingsLatest = await client.GetListingsLatest();
```
