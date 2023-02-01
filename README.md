# バフェットコード Excelアドイン

[バフェットコード](https://www.buffett-code.com/)の提供する[WebAPI](https://docs.buffett-code.com/)から上場企業の財務情報を取得し、ユーザ定義関数(UDF)によりデータの操作ができるExcelアドインです。

![Build status](https://github.com/BuffettCode/buffett-code-api-client-excel/workflows/Build/badge.svg)
![Release](https://github.com/BuffettCode/buffett-code-api-client-excel/workflows/Release/badge.svg)
![Lint](https://github.com/BuffettCode/buffett-code-api-client-excel/workflows/Lint/badge.svg)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2019-red.svg)
[![NetFramework](https://img.shields.io/badge/Language-C%23%207.0-orange.svg)](https://blogs.msdn.microsoft.com/dotnet/2016/08/24/whats-new-in-csharp-7-0/)

## Environment & Build
### Requirements
* Windows PC and Office 2010 or later
* Visual Studio 2019
* .NET framework 4.6.1 or later
* Wix Toolset 3.1.1
* Wix Toolset Visual Studio 2019 Extension
* .NET framework 3.5
* dotnet-format

### Getting Started

#### Visual Studio Community 2019 インストール

* [オフィシャルサイト](https://visualstudio.microsoft.com/ja/downloads/)からインストーラをダウンロード
* インストールオプションの選択で以下のチェックボックスにチェックをつけてインストール

```text
[Workloads]
.NET desktop development
Visual Studio extension development
Office/SharePoint development

[Individual components]
.NET framework 3.5 development tools
ClickOnce Publishing
Git for windows
GitHub extension for Visual Studio
Visual Studio Tools for Office (VSTO)
```

#### .NET Framework 3.5の有効化

* Windowsでは「Windows の機能の有効化または無効化」 (Turn Windows features on or off) から、Windows ServerではServer Managerから、.NET Framework 3.5を有効化する必要がある
* (WiX Toolset が.NET Framework 3.5に依存するため必要)

#### Wix v3 Toolset 3.1.1 インストール

* [オフィシャルサイト](https://github.com/wixtoolset/wix3/releases/tag/wix3111rtm)からインストーラ(exeファイル)をダウンロードしてインストール

#### Wix v3 Toolset Visual Studio 2019 Extension インストール

* [オフィシャルサイト](https://wixtoolset.org/docs/wix3/#recommended-build)から"Visual Studio 2019 Extension"用インストーラをダウンロードしてインストール

#### git clone

* Visual Studio 2019を起動
* メニューから `Clone or check out code` を選択
* `repository location`に以下のURLを指定してClone

[https://github.com/BuffettCode/buffett-code-api-client-excel.git](https://github.com/BuffettCode/buffett-code-api-client-excel.git)

#### デジタル署名の登録

バフェットコードのExcelアドインの一部（`BuffettCodeAddinRibbon`）はVSTO(Visual Studio Tools for Office)を利用しており、ClickOnceのマニフェストにデジタル署名が必要です。`BuffettCode` プロジェクトにデジタル署名の設定がされている必要があります。リポジトリにコミットされたテスト証明書([BuffettCodeTest.pfx](./Certificates/BuffettCodeTest.pfx))を使う場合は以下のように設定します。

* ソリューションエクスプローラから `BuffettCodeAddinRibbon` のプロパティを開く
* Signingタブで `Sign the ClickOnce manifests` のチェックを付ける
* "Select from File..." より "BuffettCodeTest.pfx" を選択する


なお、ルート証明書は[Sectigo](https://sectigo.com/knowledge-base/detail/AAA-Certificate-Services-Root-2028/kA03l00000117cL)から取得、[Certificates/AAACertificateServices.crt](./Certificates/AAACertificateServices.crt)に設置済みです。

#### ビルド

* メニューから Tools -> NuGet Package Manager -> Package Manager Console でコンソールを開いて、以下のコマンドを実行

```powershell
Update-Package -reinstall
```

* メニューから Build -> Rebuild Solution を実行

### Excelとつないで開発する
Visual StudioとExcelを使って、実際にアドインをExcelから動かしながら開発することができます。

#### Ribbon (tokenの設定やCSVダウンロード) の動作確認

1. `BuffettCodeAddinRibbon` を選択し、 Start Debugging を選択する
2. Excelが立ち上がるので、新規のBookを作成して動作確認を行う

新規のBookは開発中のアドインをインストール済みの状態で作成されます。

注意点。すでにエクセルに BuffettCode エクセルアドインがインストールされていると、
デバッグを開始してもエラー画面が出てデバッグできません。
この場合はコントロールパネルを開いて BuffettCode エクセルアドインをアンインストールしましょう。


#### UDF (BCODE関数) の動作確認

1. ソリューションエクスプローラから `BuffettCodeExcelFunctions` を右クリックする
2. 右クリックメニューの Debug -> Start New Instance を選択する
3. Excelが立ち上がるので、新規のBookを作成して動作確認を行う

こちらも、新規のBookは開発中のUDFをインストール済みの状態で作成されます。

### Code Format
`dotnet-format` が `.editorconfig` を参照して CI で lint をしています。
[how-to-install](https://github.com/dotnet/format#how-to-install) を参考に dotnet format をinstallし

```powershell
dotnet-format
```

してください。

### Registryに保存された設定を編集する
[レジストリエディタ](https://support.microsoft.com/ja-jp/windows/windows-10-%E3%81%A7%E3%83%AC%E3%82%B8%E3%82%B9%E3%83%88%E3%83%AA-%E3%82%A8%E3%83%87%E3%82%A3%E3%82%BF%E3%83%BC%E3%82%92%E9%96%8B%E3%81%8F%E6%96%B9%E6%B3%95-deab38e6-91d6-e0aa-4b7c-8878d9e07b11)を利用してください。

下記2つの領域に設定が保存されています

- アドオンの設定: `HKEY_CURRENT_USER\SOFTWARE\BuffettCode\ExcelAddin`
- 開発用の設定: `HKEY_CURRENT_USER\SOFTWARE\BuffettCode\ExcelAddinDev`
- UnitTestで利用: `HKEY_CURRENT_USER\SOFTWARE\BuffettCode\ExcelAddinDevTest`

## Release
バフェットコードの開発チームがリリースのハンドリングをしています。
リリース時は下記のファイルの `ProductVersion` を適切に設定してください。 
- [BuffettCodeExcelAddin32Installer/Bundle.wxs](./BuffettCodeExcelAddin32Installer/Bundle.wxs)
- [SetupAddinRibbon32/Product.wxs](./SetupAddinRibbon32/Product.wxs)
- [SetupExcelFunctions32/Product.wxs](./SetupExcelFunctions32/Product.wxs)

GitHubに release tag を作成することで、[GithubAction (release)](./.github/workflows/release.yml)がトリガーされ、Installerが更新されます。

## ライセンス

Apache Lisence 2.0

## コンタクト

バグレポートはissueを作成いただくか、[Shu Suzuki (@shoe116)](https://twitter.com/shoe116)までご連絡ください。Pull Requestも歓迎します。
