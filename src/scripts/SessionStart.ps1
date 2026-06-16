# SessionStart Hook Script
# This script is executed when a new agent session begins.
# Use it to inject project-specific context into the agent's conversation.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [SessionStartInput]::FromJson($json)
$output = [SessionStartOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

$output = [SessionStartOutput]@{
    AdditionalContext = "System Environment: Windows, Session Started."
}

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
