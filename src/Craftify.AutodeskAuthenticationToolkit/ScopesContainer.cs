using Autodesk.Forge;

namespace Craftify.AutodeskAuthenticationToolkit;

public static class ScopesContainer
{
    public static Scope[] Public => new[]
    {
        Scope.ViewablesRead
    };
    public static Scope[] Default => new[]
    {
        Scope.BucketCreate,
        Scope.BucketRead,
        Scope.DataRead,
        Scope.DataWrite,
        Scope.DataCreate
    };
    public static Scope[] ModelDerivative => new[]
    {
        Scope.BucketCreate,
        Scope.BucketRead,
        Scope.BucketDelete,
        Scope.DataRead,
        Scope.DataWrite,
        Scope.DataCreate,
        Scope.CodeAll
    };
}