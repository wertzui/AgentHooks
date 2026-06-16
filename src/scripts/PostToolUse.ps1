# PostToolUse Hook Script
# This script is executed by the VS Code Agent system after any tool completes successfully.
# Use it to run formatters, log results, or inject additional context into the conversation.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [PostToolUseInput]::FromJson($json)
$output = [PostToolUseOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

# Example logic: Block further processing if the tool returned an error.
if ($inputJson.ToolResponse -match "error") {
    $output = [PostToolUseOutput]@{
        Decision          = [DecisionType]::Block
        Reason            = "Tool execution failed."
        AdditionalContext = "The tool returned an error: $($inputJson.ToolResponse)"
    }
}

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
