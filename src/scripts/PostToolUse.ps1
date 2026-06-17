# PostToolUse Hook Script
# This script is executed by the VS Code Agent system after any tool completes successfully.
# Use it to run formatters, log results, or inject additional context into the conversation.

Add-Type -Path (Join-Path $PSScriptRoot "Types.dll")

$ErrorActionPreference = "Stop"

try {
    $json = $input | Out-String
    $in = [PostToolUseInput]::FromJson($json)
    $out = [PostToolUseOutput]::new()

    # =========================================
    # DO NOT MODIFY ANYTHING ABOVE THIS LINE.
    # YOUR CUSTOM LOGIC GOES BELOW THIS LINE.
    # =========================================

    $logFile = Join-Path $PSScriptRoot "hooks.log"
    $logEntry = "[$(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')] [$($in.HookEventName)]`nraw`n$($json)`ndeserialized`n$($in | ConvertTo-Json -Depth 10 -Compress)"
    Add-Content -Path $logFile -Value $logEntry

    # =========================================
    # DO NOT MODIFY ANYTHING BELOW THIS LINE.
    # YOUR CUSTOM LOGIC GOES ABOVE THIS LINE.
    # =========================================

    $out.ToJson() | Write-Output
}
catch {
    [Console]::Error.WriteLine($_.Exception.Message)
    exit 2
}
