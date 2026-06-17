# Stop Hook Script
# This script is executed when the agent session ends.
# Return Decision = Block to prevent the agent from stopping (e.g., to run tests first).
# Always check StopHookActive to prevent the agent from running indefinitely.

Add-Type -Path (Join-Path $PSScriptRoot "Types.dll")

$ErrorActionPreference = "Stop"

try {
    $json = $input | Out-String
    $in = [StopInput]::FromJson($json)
    $out = [StopOutput]::new()

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
