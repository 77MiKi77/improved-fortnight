$ErrorActionPreference='Stop'
dotnet publish .\PetRose.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ..\dist\RoseDeskPet-win-x64
Write-Host 'Double-click dist\RoseDeskPet-win-x64\RoseDeskPet.exe to start.'
