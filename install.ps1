<#
.SYNOPSIS
    Installs the agent hooks to a specified directory.

.DESCRIPTION
    This script copies the contents of the ./src folder to the target directory. 
    By default, it installs to ~/.copilot/hooks.

.PARAMETER target
    The destination path where the hooks will be installed. Defaults to "~/.copilot/hooks".
#>
param (
    [Parameter(Mandatory=$false)]
    [string]$target = "~/.copilot/hooks"
)

$dest = New-Item -Path $target -ItemType Directory -Force | ForEach-Object { $_.FullName }
Copy-Item -Path "./src/*" -Destination $dest -Recurse -Force
Write-Host "Installed hooks to: $dest"
