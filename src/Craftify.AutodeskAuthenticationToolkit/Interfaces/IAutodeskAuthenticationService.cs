namespace Craftify.AutodeskAuthenticationToolkit.Interfaces;

public interface IAutodeskAuthenticationService
{
    Task<Token> GetTwoLeggedToken(Action<TokenOptions>? configTokenOptions = null);
}