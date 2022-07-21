namespace Microsoft.Bot.Builder.TemplateManager;

/// <summary>
/// Map of Template Ids-> Template Function().
/// </summary>
public class TemplateIdMap : Dictionary<string, Func<dynamic, object>>
{
}
