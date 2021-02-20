# バフェットコード Excelアドイン

[バフェットコード](https://www.buffett-code.com/)の提供する[WebAPI](https://docs.buffett-code.com/)から上場企業の財務情報を取得し、ユーザ定義関数(UDF)によりデータの操作ができるExcelアドインです。

![Build status](https://github.com/BuffettCode/buffett-code-api-client-excel/workflows/Build/badge.svg) [![Release](https://github.com/BuffettCode/buffett-code-api-client-excel/workflows/Release/badge.svg)](https://ci.appveyor.com/project/shinsuke/buffett-code-api-client-excel) ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2019-red.svg)  [![NetFramework](https://img.shields.io/badge/Language-C%23%207.0-orange.svg)](https://blogs.msdn.microsoft.com/dotnet/2016/08/24/whats-new-in-csharp-7-0/) 

## Environment & Build
### Requirements
* Windows PC and Office 2010 or later
* Visual Studio 2019
* .NET framework 4.6.1 or later
* Wix Toolset 3.1.1
* Wix Toolset Visual Studio 2019 Extension
* .NET framework 3.5

### Getting Started

#### Visual Studio Community 2019 インストール

* [オフィシャルサイト](https://visualstudio.microsoft.com/ja/downloads/)からインストーラをダウンロード
* インストールオプションの選択で以下のチェックボックスにチェックをつけてインストール

```
[Workloads]
.NET desktop development
Visual Studio extension development
Office/SharePoint development

[Individual components]
.NET framework 3.5 development tools
ClickOnce Publishing
Git for windows
GitHub extension for Visual Studio
```

#### .NET Framework 3.5の有効化 (Windows Serverのみ)

* Windows Serverでは.NET Framework 3.5をServer Managerから有効化する必要がある
* (WiX Toolsetが.NET Framework 3.5に依存するため必要)

#### Wix Toolset 3.1.1 インストール

* [オフィシャルサイト](https://github.com/wixtoolset/wix3/releases/tag/wix3111rtm)からインストーラ(exeファイル)をダウンロードしてインストール

#### Wix Toolset Visual Studio 2019 Extension インストール

* [オフィシャルサイト](https://wixtoolset.org/releases/)からインストーラをダウンロードしてインストール

#### git clone

* Visual Studio 2019を起動
* メニューから `Clone or check out code` を選択
* `repository location`に以下のURLを指定してClone

[https://github.com/BuffettCode/buffett-code-api-client-excel.git](https://github.com/BuffettCode/buffett-code-api-client-excel.git)

#### デジタル署名の登録

バフェットコードのExcelアドインはVSTO(Visual Studio Tools for Office)を利用しており、ClickOnceのマニフェストにデジタル署名が必要です。`BuffettCode` プロジェクトにデジタル署名の設定がされている必要があります。リポジトリにコミットされたテスト証明書([BuffettCodeTest.pfx](./BuffettCode/BuffettCodeTest.pfx))を使う場合は以下のように設定します。


* ソリューションエクスプローラから `BuffettCode` プロジェクトのプロパティを開く
* Signingタブで `Sign the ClickOnce manifests` のチェックを付ける
* "Select from File..." より "BuffettCodeTest.pfx" を選択する

#### ビルド

* メニューから Tools -> NuGet Package Manager -> Package Manager Console でコンソールを開いて、以下のコマンドを実行

```
Update-Package -reinstall
```

* メニューから Build -> Rebuild Solution を実行

### Excelとつないで開発する
Visual StudioとExcelを使って、実際にアドオンをExcelから動かしながら開発することができます。

1. Debug モードでビルドを行う
2. Debugしたいパッケージを選択し、Debugを開始する（BCODE関数とCSV DLは別パッケージなので注意）
3. Excelが立ち上がるので、新規のBookを作成する

作成された新規のBookは開発中のアドオンをインストール済みの状態で作成されます。


## ライセンス

Apache Lisence 2.0

## コンタクト

バグレポートはissueを作成いただくか、[Shu Suzuki (@shoe116)](https://twitter.com/shoe116)までご連絡ください。Pull Requestも歓迎します。
