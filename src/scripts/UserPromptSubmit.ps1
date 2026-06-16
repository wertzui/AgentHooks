# UserPromptSubmit Hook Script
# This script is executed by the VS Code Agent system when the user submits a prompt.
# Use it to audit user requests, inject system context, or block disallowed prompts.

Add-Type -Path (Join-Path $PSScriptRoot "Types.cs") -ReferencedAssemblies @(
    [System.Text.Json.JsonSerializer].Assembly.Location
)

$json = [Console]::In.ReadToEnd()
$inputJson = [UserPromptSubmitInput]::FromJson($json)
$output = [CommonOutput]::new()

# =========================================
# DO NOT MODIFY ANYTHING ABOVE THIS LINE.
# YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
# =========================================

# Example logic: Block prompts that request destructive operations.
# if ($inputJson.Prompt -match "drop table|rm -rf") {
#     $output = [CommonOutput]@{
#         Continue   = $false
#         StopReason = "Disallowed prompt content detected."
#     }
# }

# =========================================
# DO NOT MODIFY ANYTHING BELOW THIS LINE.
# YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
# =========================================

$output.ToJson() | Write-Output
