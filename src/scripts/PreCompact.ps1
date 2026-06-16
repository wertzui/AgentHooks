# PreCompact Hook Script
# This script is executed before the conversation context is compacted.
# Use it to export important context or save state before truncation.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [PreCompactInput]::FromJson($json)
$output = [CommonOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

# Example logic: Allow compaction to proceed.
# To block: $output = [CommonOutput]@{ Continue = $false; StopReason = "Export not complete." }

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
