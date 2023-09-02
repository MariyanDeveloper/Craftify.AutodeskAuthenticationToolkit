using Autodesk.Forge;
using Craftify.AutodeskAuthenticationToolkit.Interfaces;

namespace Craftify.AutodeskAuthenticationToolkit;

public class AutodeskAuthenticationService : IAutodeskAuthenticationService
{
    private readonly ITwoLeggedApi _twoLeggedApi;
    private readonly AuthCredentials _authCredentials;
    private readonly string _grantType = "client_credentials";
    private Lazy<Token>? _lazyToken;

    public AutodeskAuthenticationService(
        ITwoLeggedApi twoLeggedApi,
        AuthCredentials authCredentials)
    {
        _twoLeggedApi = twoLeggedApi ?? throw new ArgumentNullException(nameof(twoLeggedApi));
        _authCredentials = authCredentials ?? throw new ArgumentNullException(nameof(authCredentials));
    }
    public async Task<Token> GetTwoLeggedToken(Action<TokenOptions>? configTokenOptions = null)
    {
        var tokenOptions = CreateTokenOptions(configTokenOptions);
        if (_lazyToken is null || IsTokenRefreshRequired(_lazyToken.Value, tokenOptions.Scopes))
        {
            await RefreshToken(tokenOptions.Scopes);
        }
        return _lazyToken.Value;
    }
    private async Task RefreshToken(Scope[] scopes)
    {
        var token = await GetTokenInternal(scopes);
        _lazyToken = new Lazy<Token>(() => token);
    }
    private async Task<Token> GetTokenInternal(Scope[] scopes)
    {
        var authenticationResponse = await _twoLeggedApi
            .AuthenticateAsync(
                _authCredentials.ClientId,
                _authCredentials.ClientSecret,
                _grantType,
                scopes);
        return new Token(
            authenticationResponse.access_token,
            DateTime.UtcNow.AddSeconds(authenticationResponse.expires_in),
            scopes);
    }
    private TokenOptions CreateTokenOptions(Action<TokenOptions>? configTokenOptions)
    {
        var tokenOptions = new TokenOptions();
        configTokenOptions?.Invoke(tokenOptions);
        return tokenOptions;
    }
    private bool IsTokenRefreshRequired(Token? token, Scope[] requiredScopes)
    {
        if (token is null || token.ExpiresAt < DateTime.UtcNow)
        {
            return true; 
        }
        return token.Scopes.All(requiredScopes.Contains) is false;
    }

}