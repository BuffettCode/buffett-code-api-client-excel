# Set up your Windows Environment
## Install .Net Freamwork and Wix
- [install .Netfreamwork 3.5](https://dotnet.microsoft.com/download/dotnet-framework/net35-sp1)
- [install Wix](https://github.com/wixtoolset/wix3/releases/tag/wix3112rtm)

## Install Visual Studio 2019 and extentions
- [install Visual Studio 2019 with MsBuild](https://visualstudio.microsoft.com/downloads/)
- [Wix Toolset Visual Studio 2019 Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019Extension)
- [install Invoke MsBuild](https://www.powershellgallery.com/packages/Invoke-MsBuild/)

## Install scoop and build tools
- [install scoop](https://scoop.sh)
- [install ssh using scoop](https://github.com/lukesampson/scoop/wiki/SSH-on-Windows)
- install git using scoop `scoop install git`
- install nuget using scoop `scoop install nuget`
- install wixtoolset using scoop `scoop install wixtoolset`

# Install dependencies and build 
Execute following powershell cmd in a cloned folder.

```powershell
# install dependencies
.\scripts\install_dependencies.ps1

# Run Build Command
Invoke-MsBuild -Path .\BuffettCode.sln
```
