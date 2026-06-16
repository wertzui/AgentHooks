# SubagentStart Hook Script
# This script is executed when a subagent is spawned.
# Use it to inject context or initialize resources for the subagent.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [SubagentStartInput]::FromJson($json)
$output = [SubagentStartOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

$output = [SubagentStartOutput]@{
    AdditionalContext = "Subagent '$($inputJson.AgentType)' initiated."
}

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
