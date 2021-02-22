# Overview
## Packages
`BuffettCode` ソリューションは下記の package を含みます

1. BuffettCodeAddinRibbon and Test
2. BuffettCodeAPIAdapter and Test 
3. BuffettCodeExcelFunctions and Test
4. BuffettCodeInstaller

ここでは、各 package の役割について簡単に説明します

### 1. BuffettCodeAddinRibbon
バフェット・コードアドインの、Excel の「リボン」部分の実装です。
- CSV出力やTokenの入力フォームを提供します
- VSTO(Visual Studio Tools for Office)を利用しており、リリースビルドにはデジタル署名が必要です

### 2. BuffettCodeIO
- [BuffettCode API](https://docs.buffett-code.com/)のclient, Formatter などデータの入出力を管理します
- Registryの読み書きもここにあります

### 3. BuffetCodeExcelFunctions
- [Excel-DNA](https://excel-dna.net/) を利用して、ExcelのUDFを作成しています
- [Excel-XLL](https://docs.microsoft.com/ja-jp/office/client-developer/excel/developing-excel-xlls) が Build されます

### BuffettCodeInstaller
BuffettCode Excel Addin は、上記の通り
- VSTOで作られたExcelリボン
- Excel-DNAで作られたUDF

という、2つの異なる2つの成果物で成り立っています。
インストーラは、この2つをそれぞれRegistryに配置します。