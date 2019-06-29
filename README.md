# バフェットコード Excelアドイン

バフェットコードの提供するWebAPIから上場企業の財務情報を取得し、ユーザ定義関数(UDF)によりデータの操作ができるExcelアドインです。

## Environment & Build

### Required Sortwares

* Windows 8 or later
* Office 2010 or later
* Visual Studio 2019
* Wix Toolset 3.1.1
* Wix Toolset Visual Studio 2019 Extension

### Build

1. Clone this repositry
1. Open `BuffettCode.sln` using Visual Studio 2019
1. Place your certificate file(.pfx) or setting code signing to `BuffettCode` project
1. Reinstall NuGet packages
```
Update-Package -reinstall
```
5. Build Solusion

## ライセンス

Apache Lisence 2.0

## 問い合わせ

バグレポートはissueを作成いただくか、[駄犬 (@daken_in_market)](https://twitter.com/daken_in_market)までご連絡ください。
