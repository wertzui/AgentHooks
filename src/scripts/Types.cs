using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the common fields present in every hook's input.
/// </summary>
public record CommonInput
{
    /// <summary>
    /// ISO 8601 timestamp when the hook fired.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Working directory for the agent session.
    /// </summary>
    public string? Cwd { get; init; }

    /// <summary>
    /// Unique identifier for the current agent session.
    /// </summary>
    public string? SessionId { get; init; }

    /// <summary>
    /// Name of the hook event (e.g., PreToolUse).
    /// </summary>
    public required string HookEventName { get; init; }

    /// <summary>
    /// Absolute path to a file containing the session conversation transcript.
    /// </summary>
    /// <remarks>
    /// transcript_path is provided for convenience — for example, logging, auditing, or lightweight checks such as whether a file was read during the session.
    /// The transcript file format is not a stable hook API and may change in future VS Code releases.
    /// Prefer the documented hook input fields (tool_name, tool_input, prompt, and so on) whenever possible.
    /// </remarks>
    public string? TranscriptPath { get; init; }

    /// <summary>Deserializes a <see cref="CommonInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="CommonInput"/> instance.</returns>
    public static CommonInput FromJson(string json) =>
        JsonSerializer.Deserialize<CommonInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents the common fields in any hook's output.
/// </summary>
public record CommonOutput
{
    /// <summary>
    /// Set to false to stop processing (default: true).
    /// </summary>
    public bool Continue { get; init; } = true;

    /// <summary>
    /// Reason for stopping, shown to the user when continue is false.
    /// </summary>
    public string? StopReason { get; init; }

    /// <summary>
    /// Warning message displayed to the user in the chat.
    /// </summary>
    public string? SystemMessage { get; init; }

    /// <summary>Deserializes a <see cref="CommonOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="CommonOutput"/> instance.</returns>
    public static CommonOutput FromJson(string json) =>
        JsonSerializer.Deserialize<CommonOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents specific output for a PreToolUse hook.
/// </summary>
public record PreToolUseOutput
{
    /// <summary>
    /// The decision to allow, deny, or ask for tool approval.
    /// </summary>
    public PermissionDecisionType PermissionDecision { get; init; } = PermissionDecisionType.Allow;

    /// <summary>
    /// Reason shown to user for the permission decision.
    /// </summary>
    public string? PermissionDecisionReason { get; init; }

    /// <summary>
    /// Modified tool input (optional).
    /// </summary>
    public object? UpdatedInput { get; init; }

    /// <summary>
    /// Extra context for the model.
    /// </summary>
    public string? AdditionalContext { get; init; }

    /// <summary>Deserializes a <see cref="PreToolUseOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="PreToolUseOutput"/> instance.</returns>
    public static PreToolUseOutput FromJson(string json) =>
        JsonSerializer.Deserialize<PreToolUseOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents specific output for a PostToolUse hook.
/// </summary>
public record PostToolUseOutput
{
    /// <summary>
    /// Block further processing (optional).
    /// </summary>
    public DecisionType? Decision { get; init; }

    /// <summary>
    /// Reason for blocking (shown to the model).
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// Extra context injected into the conversation.
    /// </summary>
    public string? AdditionalContext { get; init; }

    /// <summary>Deserializes a <see cref="PostToolUseOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="PostToolUseOutput"/> instance.</returns>
    public static PostToolUseOutput FromJson(string json) =>
        JsonSerializer.Deserialize<PostToolUseOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents output for a SessionStart hook.
/// </summary>
public record SessionStartOutput : CommonOutput
{
    /// <summary>
    /// Context injected into the agent's conversation at the start of the session.
    /// </summary>
    public string? AdditionalContext { get; init; }

    /// <summary>Deserializes a <see cref="SessionStartOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="SessionStartOutput"/> instance.</returns>
    public static SessionStartOutput FromJson(string json) =>
        JsonSerializer.Deserialize<SessionStartOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public new string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents output for a SubagentStart hook.
/// </summary>
public record SubagentStartOutput : CommonOutput
{
    /// <summary>
    /// Context injected into the subagent's conversation.
    /// </summary>
    public string? AdditionalContext { get; init; }

    /// <summary>Deserializes a <see cref="SubagentStartOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="SubagentStartOutput"/> instance.</returns>
    public static SubagentStartOutput FromJson(string json) =>
        JsonSerializer.Deserialize<SubagentStartOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public new string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents output for a Stop hook.
/// </summary>
public record StopOutput : CommonOutput
{
    /// <summary>
    /// Set to <see cref="DecisionType.Block"/> to prevent the agent from stopping.
    /// Always check <see cref="StopInput.StopHookActive"/> before blocking to avoid infinite loops.
    /// </summary>
    public DecisionType? Decision { get; init; }

    /// <summary>
    /// Required when <see cref="Decision"/> is <see cref="DecisionType.Block"/>. Tells the agent why it should continue.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>Deserializes a <see cref="StopOutput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="StopOutput"/> instance.</returns>
    public static StopOutput FromJson(string json) =>
        JsonSerializer.Deserialize<StopOutput>(json, HookJsonOptions.Default)!;

    /// <summary>Serializes this instance to a JSON string using camelCase property names.</summary>
    /// <returns>A JSON string representation of this instance.</returns>
    public new string ToJson() => JsonSerializer.Serialize(this, HookJsonOptions.Output);
}

/// <summary>
/// Represents input for a PreToolUse hook.
/// </summary>
public record PreToolUseInput : CommonInput
{
    /// <summary>
    /// Name of the tool being used.
    /// </summary>
    public string ToolName { get; init; } = string.Empty;

    /// <summary>
    /// The input provided to the tool.
    /// </summary>
    public object? ToolInput { get; init; }

    /// <summary>
    /// Unique identifier for the tool use.
    /// </summary>
    public string ToolUseId { get; init; } = string.Empty;

    /// <summary>Deserializes a <see cref="PreToolUseInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="PreToolUseInput"/> instance.</returns>
    public static PreToolUseInput FromJson(string json) =>
        JsonSerializer.Deserialize<PreToolUseInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a PostToolUse hook.
/// </summary>
public record PostToolUseInput : CommonInput
{
    /// <summary>
    /// Name of the tool that just completed.
    /// </summary>
    public string ToolName { get; init; } = string.Empty;

    /// <summary>
    /// The input provided to the tool as a property bag. Keys match the original JSON property names.
    /// </summary>
    public Dictionary<string, object?>? ToolInput { get; init; }

    /// <summary>
    /// Unique identifier for the tool use.
    /// </summary>
    public string ToolUseId { get; init; } = string.Empty;

    /// <summary>
    /// The response received from the tool.
    /// </summary>
    public string ToolResponse { get; init; } = string.Empty;

    /// <summary>Deserializes a <see cref="PostToolUseInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="PostToolUseInput"/> instance.</returns>
    public static PostToolUseInput FromJson(string json) =>
        JsonSerializer.Deserialize<PostToolUseInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a UserPromptSubmit hook.
/// </summary>
public record UserPromptSubmitInput : CommonInput
{
    /// <summary>
    /// The text of the prompt submitted by the user.
    /// </summary>
    public string Prompt { get; init; } = string.Empty;

    /// <summary>Deserializes a <see cref="UserPromptSubmitInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="UserPromptSubmitInput"/> instance.</returns>
    public static UserPromptSubmitInput FromJson(string json) =>
        JsonSerializer.Deserialize<UserPromptSubmitInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a SessionStart hook.
/// </summary>
public record SessionStartInput : CommonInput
{
    /// <summary>
    /// How the session was started (e.g., "new").
    /// </summary>
    public string Source { get; init; } = "new";

    /// <summary>Deserializes a <see cref="SessionStartInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="SessionStartInput"/> instance.</returns>
    public static SessionStartInput FromJson(string json) =>
        JsonSerializer.Deserialize<SessionStartInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a Stop hook.
/// </summary>
public record StopInput : CommonInput
{
    /// <summary>
    /// True when the agent is already continuing as a result of a previous stop hook.
    /// </summary>
    public bool StopHookActive { get; init; }

    /// <summary>Deserializes a <see cref="StopInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="StopInput"/> instance.</returns>
    public static StopInput FromJson(string json) =>
        JsonSerializer.Deserialize<StopInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a SubagentStart hook.
/// </summary>
public record SubagentStartInput : CommonInput
{
    /// <summary>
    /// Unique identifier for the subagent.
    /// </summary>
    public string AgentId { get; init; } = string.Empty;

    /// <summary>
    /// The type of agent (e.g., "Plan").
    /// </summary>
    public string AgentType { get; init; } = string.Empty;

    /// <summary>Deserializes a <see cref="SubagentStartInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="SubagentStartInput"/> instance.</returns>
    public static SubagentStartInput FromJson(string json) =>
        JsonSerializer.Deserialize<SubagentStartInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a SubagentStop hook.
/// </summary>
public record SubagentStopInput : CommonInput
{
    /// <summary>
    /// Unique identifier for the subagent.
    /// </summary>
    public string AgentId { get; init; } = string.Empty;

    /// <summary>
    /// The type of agent (e.g., "Plan").
    /// </summary>
    public string AgentType { get; init; } = string.Empty;

    /// <summary>
    /// True when the agent is already continuing as a result of a previous stop hook.
    /// </summary>
    public bool StopHookActive { get; init; }

    /// <summary>Deserializes a <see cref="SubagentStopInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="SubagentStopInput"/> instance.</returns>
    public static SubagentStopInput FromJson(string json) =>
        JsonSerializer.Deserialize<SubagentStopInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Represents input for a PreCompact hook.
/// </summary>
public record PreCompactInput : CommonInput
{
    /// <summary>Deserializes a <see cref="PreCompactInput"/> from a JSON string.</summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>The deserialized <see cref="PreCompactInput"/> instance.</returns>
    public static PreCompactInput FromJson(string json) =>
        JsonSerializer.Deserialize<PreCompactInput>(json, HookJsonOptions.Default)!;
}

/// <summary>
/// Provides shared <see cref="JsonSerializerOptions"/> for hook JSON serialization and deserialization.
/// </summary>
internal static class HookJsonOptions
{
    /// <summary>
    /// Default options configured with snake_case property names and lowercase enum values, used for deserializing hook inputs.
    /// </summary>
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower), new JsonDictionaryConverter() }
    };

    /// <summary>
    /// Options configured with camelCase property names and lowercase enum values, used for serializing hook outputs.
    /// </summary>
    public static readonly JsonSerializerOptions Output = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
}

/// <summary>
/// Converts JSON objects to <see cref="Dictionary{TKey,TValue}"/> for ergonomic property-bag access.
/// Nested objects become nested dictionaries; arrays become <see cref="List{T}"/>.
/// </summary>
internal sealed class JsonDictionaryConverter : JsonConverter<Dictionary<string, object?>>
{
    /// <inheritdoc/>
    public override Dictionary<string, object?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        return ConvertObject(doc.RootElement);
    }

    private static Dictionary<string, object?> ConvertObject(JsonElement element) =>
        element.EnumerateObject().ToDictionary(p => p.Name, p => ConvertValue(p.Value));

    private static object? ConvertValue(JsonElement element) => element.ValueKind switch
    {
        JsonValueKind.Object => ConvertObject(element),
        JsonValueKind.Array  => element.EnumerateArray().Select(ConvertValue).ToList<object?>(),
        JsonValueKind.String => element.GetString(),
        JsonValueKind.Number => element.TryGetInt64(out var l) ? l : element.GetDouble(),
        JsonValueKind.True   => true,
        JsonValueKind.False  => false,
        _                    => null
    };

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Dictionary<string, object?> value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, (IDictionary<string, object?>)value, options);
}

/// <summary>
/// Represents the types of decisions for tool approval in a PreToolUse hook.
/// </summary>
public enum PermissionDecisionType
{
    /// <summary>
    /// Automatically allow the tool execution.
    /// </summary>
    Allow,
    /// <summary>
    /// Block the tool execution.
    /// </summary>
    Deny,
    /// <summary>
    /// Prompt the user for confirmation before proceeding.
    /// </summary>
    Ask
}

/// <summary>
/// Represents the decision to block or continue processing in a PostToolUse hook.
/// </summary>
public enum DecisionType
{
    /// <summary>
    /// Continue processing as normal.
    /// </summary>
    Continue,
    /// <summary>
    /// Block further processing.
    /// </summary>
    Block
}

/// <summary>
/// Enum representing the types of hooks supported by VS Code.
/// </summary>
public enum HookType
{
    PreToolUse,
    PostToolUse,
    UserPromptSubmit,
    SessionStart,
    Stop,
    SubagentStart,
    SubagentStop,
    PreCompact
}
