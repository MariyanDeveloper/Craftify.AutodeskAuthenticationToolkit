using Autodesk.Forge;

namespace Craftify.AutodeskAuthenticationToolkit;

public class TokenOptions
{
    public Scope[] Scopes { get; set; } = ScopesContainer.Default;
}