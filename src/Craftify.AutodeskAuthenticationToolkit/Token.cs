using Autodesk.Forge;

namespace Craftify.AutodeskAuthenticationToolkit;

public record Token(string AccessToken, DateTime ExpiresAt, Scope[] Scopes);