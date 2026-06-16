# Stop Hook Script
# This script is executed when the agent session ends.
# Return Decision = Block to prevent the agent from stopping (e.g., to run tests first).
# Always check StopHookActive to prevent the agent from running indefinitely.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [StopInput]::FromJson($json)
$output = [StopOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

# Example logic: Allow the agent to stop unconditionally.
# To block (only when not already looping):
# if (-not $inputJson.StopHookActive) {
#     $output = [StopOutput]@{ Decision = [DecisionType]::Block; Reason = "Run the test suite first." }
# }

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
