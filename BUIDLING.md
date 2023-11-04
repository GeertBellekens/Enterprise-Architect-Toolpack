# Other GitHub Repositories Required:
* https://github.com/GeertBellekens/Enterprise-Architect-Add-in-Framework
* https://github.com/GeertBellekens/UML-Tooling-Framework
* https://github.com/GeertBellekens/DDL-Parser
* https://github.com/GeertBellekens/cobol-object-mapper
* https://github.com/GeertBellekens/EARefDataSplitter
* https://github.com/GeertBellekens/TextHelper

# Required Software

## Install the .NET Framework 4.7.2 Developer pack
https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer

## Install the .NET Framework 4.8 Developer pack
https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer
(Needed by EAStripper, NewAddin) 

## Install Visual Studio

Download Visual Studio 2022 Community Edition
https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=17
On the installation dialog disable all, enable Desktop & Mobile > .NET desktop development.
Click Install
Disable "Start after installation" (as you have more to install before you can run Visual Studio)

## Install Wix v3 
https://wixtoolset.org/docs/wix3/
Download wix311.exe for the installer

## Install WiX v3 - Visual Studio 2022 Extension
https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2022Extension
Run Votive2022.vsix

# Building

Start Visual Studio 2022.
When asked to sign in click "Not now, maybe later" then choose your theme and start.
Click Open Project/Solution
Browse to Enterprise-Architect-Toolpack\Total Solution.sln and Open

Right click on the Solution and choose "Build Solution".

## Creating an Installer

Right click on the EAToolPack_Setup project and choose "Build".

This will create the installer in \Enterprise-Architect-Toolpack\EAToolPack_Setup\bin\Debug\EAToolPack_Setup.msi

### Changing installer version numbers

**This section requires more work**

Open EAToolPack_Setup\Setup.wxs

Update the Product/Version value.

Update the Product/Package/Description to match the version details.

Update the Product/Upgrade/UpgradeVersion/Minimum (where Property="NEWPRODUCTFOUND") to match the version details.

# Debugging

Click the menu Debug > Attach to Process...

In the filter enter "EA".

Select the EA.exe in the "Available processes" list and click "Attach".

In your code right click the a line and choose "Breakpoint > Insert Breakpoint".

# Trouble Shooting

## BrightIdeasSoftware can't be found even though nuGet says its installed.
Reboot seemed to fix that.

