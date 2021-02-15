# CI settings
## Build / Release Flow using github actions
1. Run CI by pushed or tagged for github
    - Branches except `master` trigger [Build](../.github/workflows/build.yml)
    - Tags named *v.X.Y.Z* and `master` branch trigger [Release](../.github/workflows/release.yml)
2. Run `MSBuild` and `VSTest` on Windows 2019 image on github action
3. Upload Installer to `s3` at BuffettCode AWS Account.

## Note
- For normal build, use a **TEST** code signing certificate on this repo, [BuffettCodeTest.pfx](../BuffettCode/BuffettCodeTest.pfx)
- For release build, use a Buffett Code signing certificate using github secrets variable.
- Latest Master Branch's installer is here: [beta/latest](https://buffett-code-public-tools-ap-northeast-1.s3-ap-northeast-1.amazonaws.com/buffett-code-excel-addin/beta/latest/BuffettCodeExcelAddinInstaller.msi)  
- Tagged release build is here [release/*$tag*](https://buffett-code-public-tools-ap-northeast-1.s3-ap-northeast-1.amazonaws.com/buffett-code-excel-addin/release/$tag/BuffettCodeExcelAddinInstaller.msi)
