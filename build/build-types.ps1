$folder = $PSScriptRoot + "\..\src\scripts"
dotnet publish (Join-Path $folder "Types.cs") -c Release -r win-x64 -o $folder