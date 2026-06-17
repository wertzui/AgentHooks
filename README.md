# AgentHooks

A common logic layer for parsing and processing hooks from AI agents in PowerShell.

## Overview

`AgentHooks` provides a standardized framework for intercepting and modifying agent behavior at various lifecycle points. It uses a combination of JSON configuration and PowerShell scripts to define how the system should react to different events.

## Supported Hooks

The following hook types are supported:

- **PreToolUse**: Executed before a tool is called. Can be used to modify tool input or request user approval.
- **PostToolUse**: Executed after a tool has been run. Can be used to inject additional context into the conversation.
- **UserPromptSubmit**: Triggered when a user submits a prompt.
- **SessionStart**: Executed at the beginning of a new session.
- **Stop**: Used to terminate the current execution flow.
- **SubagentStart / SubagentStop**: Manage the lifecycle of sub-agents.
- **PreCompact**: A specialized hook for processing before context compaction.

## Configuration

Hooks are configured in `hooks.json`. Each entry defines:

- The type of event.
- The path to the PowerShell script to execute.
- The working directory (`cwd`) where the script should run.

## Installation

To set up the hooks on your system, run the installation script:

```powershell
.\install.ps1
```

This will configure the environment and link the scripts to the appropriate paths.

## Development

The core logic and data structures are defined in `src/scripts/Types.cs`. These types ensure that the communication between the agent and the hook scripts remains consistent.
