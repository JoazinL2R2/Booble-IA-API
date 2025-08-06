# ?? Booble API - Security Implementation Guide

## Overview
All endpoints in the Booble API are now protected by authentication, ensuring that only authenticated users can access the API resources. The API supports both OAuth2/OpenID Connect and legacy JWT authentication.

## ??? Security Features Implemented

### 1. Global Authentication Policy
- **Default Behavior**: All endpoints require authentication by default
- **Fallback Policy**: Unauthenticated requests are automatically rejected
- **Selective Access**: Only specific endpoints are marked as `[AllowAnonymous]`

### 2. Protected Controllers

#### ?? AmizadeController (Friends Management)
- **Base Route**: `/api/Amizade`
- **Security**: All endpoints require authentication
- **User Validation**: Users can only access their own friendship data
- **Endpoints**:
  - `GET /api/Amizade/usuario?idfUsuario={id}` - Get user friendships
  - `POST /api/Amizade/aceitar?idfAmizade={id}` - Accept friendship
  - `POST /api/Amizade/recusar?idfAmizade={id}` - Decline friendship
  - `POST /api/Amizade/desfazer?idfAmizade={id}` - Remove friendship

#### ?? HabitoController (Habits Management)
- **Base Route**: `/api/Habito`
- **Security**: All endpoints require authentication
- **User Validation**: Users can only manage their own habits
- **Endpoints**:
  - `POST /api/Habito/cadastro` - Create new habit
  - `POST /api/Habito/finalizar` - Complete habit
  - `GET /api/Habito/usuario/{userId}` - Get user habits

#### ?? RankingController (Rankings)
- **Base Route**: `/api/Ranking`
- **Security**: All endpoints require authentication
- **Endpoints**:
  - `GET /api/Ranking/streak` - Get ranking by streak
  - `GET /api/Ranking/streak/posicao` - Get user's ranking position

#### ?? UsuarioController (User Management)
- **Base Route**: `/api/Usuario`
- **Mixed Security**: Public registration/login, protected profile endpoints
- **Public Endpoints** (`[AllowAnonymous]`):
  - `POST /api/Usuario/Cadastro` - User registration
  - `POST /api/Usuario/Login` - User authentication
- **Protected Endpoints** (`[Authorize]`):
  - `GET /api/Usuario/PerfilUsuario` - Get own profile
  - `GET /api/Usuario/PerfilUsuario/{id}` - Get profile by ID (own only)
  - `PUT /api/Usuario/AtualizarPerfil` - Update profile

#### ?? OAuthController (OAuth2/OpenID Connect)
- **Base Route**: `/api/OAuth`
- **Mixed Security**: Public token endpoint, protected user info
- **Public Endpoints** (`[AllowAnonymous]`):
  - `POST /api/oauth/token` - Generate access tokens
- **Protected Endpoints** (`[Authorize]`):
  - `GET /api/oauth/userinfo` - Get user information
  - `POST /api/oauth/revoke` - Revoke tokens
  - `GET /api/oauth/introspect` - Token introspection

#### ?? HealthCheckController (System Health)
- **Base Route**: `/health`
- **Mixed Security**: Basic health check public, detailed protected
- **Public Endpoints** (`[AllowAnonymous]`):
  - `GET /health` - Basic health check (for monitoring)
- **Protected Endpoints** (`[Authorize]`):
  - `GET /health/detailed` - Detailed system information

## ?? Authentication Methods

### 1. OAuth2/OpenID Connect (Recommended)
```bash
# Get authorization code (for web/mobile apps)
GET /connect/authorize?
  response_type=code&
  client_id=booble_web&
  redirect_uri=http://localhost:3000/callback&
  scope=openid profile email booble_api&
  code_challenge=<pkce_challenge>&
  code_challenge_method=S256

# Exchange code for tokens
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=authorization_code&
client_id=booble_web&
code=<authorization_code>&
redirect_uri=http://localhost:3000/callback&
code_verifier=<pkce_verifier>
```

### 2. Resource Owner Password Grant (Legacy)
```bash
# Direct authentication (less secure, for legacy clients)
POST /api/oauth/token
Content-Type: application/x-www-form-urlencoded

grant_type=password&
username=user@example.com&
password=userpassword&
client_id=booble_legacy&
scope=booble_api
```

### 3. Legacy JWT (Backward Compatibility)
```bash
# Traditional login endpoint
POST /api/Usuario/Login
Content-Type: application/json

{
  "Des_Email": "user@example.com",
  "Senha": "userpassword"
}
```

## ?? Authorization Headers

### Bearer Token (All Methods)
```bash
Authorization: Bearer <access_token>
```

## ?? Error Responses

### 401 Unauthorized
```json
{
  "message": "Token inválido ou usuário não encontrado.",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

### 403 Forbidden
```json
{
  "message": "Você só pode acessar seus próprios dados."
}
```

### 400 Bad Request (OAuth2)
```json
{
  "error": "invalid_grant",
  "error_description": "Invalid username or password"
}
```

## ??? Implementation Details

### User Identity Validation
```csharp
// Get user ID from token claims
var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                User.FindFirst("sub")?.Value;

if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
{
    return Unauthorized(new { message = "Token inválido." });
}
```

### Resource Ownership Validation
```csharp
// Ensure user can only access their own resources
if (authenticatedUserId != requestedUserId)
{
    return Forbid("Você só pode acessar seus próprios dados.");
}
```

## ?? Client Configuration Examples

### Web Application (React/Angular/Vue)
- Client ID: `booble_web`
- Grant Type: `Authorization Code + PKCE`
- Redirect URI: `http://localhost:3000/callback`

### React Native Mobile App
- Client ID: `booble_mobile`
- Grant Type: `Authorization Code + PKCE`
- Redirect URI: `com.booble.app://callback`

### Server-to-Server
- Client ID: `booble_api_client`
- Grant Type: `Client Credentials`
- Requires: Client Secret

## ?? Testing with Swagger

1. Navigate to `/swagger` endpoint
2. Click "Authorize" button
3. Choose authentication method:
   - **Bearer**: Paste JWT token directly
   - **OAuth2**: Use OAuth2 flow (recommended)
4. Test protected endpoints

## ?? Production Considerations

### Security Recommendations
1. **HTTPS Only**: Enable `RequireHttpsMetadata: true`
2. **Token Rotation**: Implement refresh token rotation
3. **Rate Limiting**: Add rate limiting middleware
4. **Logging**: Log authentication failures
5. **Certificate**: Replace developer signing credential with production certificate

### Monitoring
- Monitor failed authentication attempts
- Track token usage patterns
- Alert on suspicious activities
- Regular security audits

### Performance
- Cache user claims for frequently accessed data
- Implement token caching strategies
- Monitor authentication middleware performance

## ?? Related Documentation
- [OAuth2-Configuration-Guide.md](./OAuth2-Configuration-Guide.md) - Client integration guide
- Identity Server Documentation: https://docs.duendesoftware.com/identityserver/v7
- OAuth2 RFC: https://tools.ietf.org/html/rfc6749
- OpenID Connect: https://openid.net/connect/