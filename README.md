# バフェットコード Excelアドイン

[バフェットコード](https://www.buffett-code.com/)の提供する[WebAPI](https://docs.buffett-code.com/)から上場企業の財務情報を取得し、ユーザ定義関数(UDF)によりデータの操作ができるExcelアドインです。

[![Build status](https://ci.appveyor.com/api/projects/status/po221rdjm7i6r77x?svg=true)](https://ci.appveyor.com/project/shinsuke/buffett-code-api-client-excel) ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2019-red.svg)  [![NetFramework](https://img.shields.io/badge/Language-C%23%207.0-orange.svg)](https://blogs.msdn.microsoft.com/dotnet/2016/08/24/whats-new-in-csharp-7-0/) 

## Environment & Build

### Requirements

* Visual Studio 2019
* Office 2010 or later
* Wix Toolset 3.1.1
* Wix Toolset Visual Studio 2019 Extension
* .NET framework 4.6.1 or later

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

#### デジタル署名の証明書配置

バフェットコードのExcelアドインはVSTO(Visual Studio Tools for Office)を利用しており、ClickOnceのマニフェストにデジタル署名が必要です。そのための証明書はレポジトリにコミットされておらず、開発者自身で用意してください。

`BuffettCode` プロジェクトにデジタル署名の設定がされている必要があります。Visual Studioで作成できる自己証明書(テスト証明書)を使う場合は以下のように設定します。

* ソリューションエクスプローラから `BuffettCode` プロジェクトのプロパティを開く
* Signingタブで `Sign the ClickOnce manifests` のチェックを付ける
* `Create Test Certificate...` ボタンを押す

#### ビルド

* メニューから Tools -> NuGet Package Manager -> Package Manager Console でコンソールを開いて、以下のコマンドを実行

```
Update-Package -reinstall
```

* メニューから Build -> Rebuild Solution を実行

## ライセンス

Apache Lisence 2.0

## コンタクト

バグレポートはissueを作成いただくか、[駄犬 (@daken_in_market)](https://twitter.com/daken_in_market)までご連絡ください。Pull Requestも歓迎します。
