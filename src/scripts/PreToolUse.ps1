# PreToolUse Hook Script
# This script is executed by the VS Code Agent system before any tool is invoked.
# It allows for intercepting, validating, or modifying the tool's input parameters 
# (e.g., checking for dangerous commands or providing additional context).

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [PreToolUseInput]::FromJson($json)
$output = [PreToolUseOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

# Example logic: Check if the tool is 'editFiles' and block if it contains "rm" or "delete" in the input.
if ($inputJson.ToolName -eq "editFiles") {
    if ($inputJson.ToolInput["content"] -match "rm|delete|drop") {
        $output = [PreToolUseOutput]@{
            PermissionDecision       = [PermissionDecisionType]::Deny
            PermissionDecisionReason = "Potential destructive command detected in content."
        }
    }
    else {
        $output = [PreToolUseOutput]@{
            PermissionDecision = [PermissionDecisionType]::Allow
        }
    }
}

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
