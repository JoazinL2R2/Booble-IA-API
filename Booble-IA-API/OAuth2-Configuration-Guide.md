# Booble Identity Server - Client Configuration Guide

## Overview
This Identity Server setup supports both web applications and React Native mobile apps using OAuth2 with PKCE (Proof Key for Code Exchange).

## Client Configurations

### Web Application (React, Angular, Vue, etc.)
```javascript
// OAuth2 Configuration
const oauthConfig = {
  authority: 'https://localhost:7000',
  client_id: 'booble_web',
  redirect_uri: 'http://localhost:3000/callback',
  post_logout_redirect_uri: 'http://localhost:3000',
  response_type: 'code',
  scope: 'openid profile email booble_api',
  filterProtocolClaims: true,
  loadUserInfo: true,
  pkce: true
};
```

### React Native Configuration
```javascript
// Install: npm install react-native-app-auth

import {authorize, refresh, revoke} from 'react-native-app-auth';

const config = {
  issuer: 'https://localhost:7000',
  clientId: 'booble_mobile',
  redirectUrl: 'com.booble.app://callback',
  scopes: ['openid', 'profile', 'email', 'booble_api'],
  additionalParameters: {},
  customHeaders: {},
  pkce: true,
  useNonce: true,
  usePKCE: true,
  skipIssuerHttpsCheck: true, // Only for development
};

// Authentication Flow
export const authenticateUser = async () => {
  try {
    const result = await authorize(config);
    return {
      accessToken: result.accessToken,
      refreshToken: result.refreshToken,
      expirationDate: result.accessTokenExpirationDate,
      userInfo: await getUserInfo(result.accessToken)
    };
  } catch (error) {
    throw new Error(`Authentication failed: ${error.message}`);
  }
};

// Get User Information
export const getUserInfo = async (accessToken) => {
  const response = await fetch('https://localhost:7000/api/oauth/userinfo', {
    headers: {
      'Authorization': `Bearer ${accessToken}`,
      'Content-Type': 'application/json'
    }
  });
  return await response.json();
};

// Refresh Token
export const refreshAccessToken = async (refreshToken) => {
  try {
    const result = await refresh(config, {
      refreshToken: refreshToken,
    });
    return result;
  } catch (error) {
    throw new Error(`Token refresh failed: ${error.message}`);
  }
};
```

### Expo Configuration (expo-auth-session)
```javascript
// Install: expo install expo-auth-session expo-crypto

import * as AuthSession from 'expo-auth-session';
import * as Crypto from 'expo-crypto';

const useProxy = true;
const redirectUri = AuthSession.makeRedirectUri({ useProxy });

export const authenticateWithOAuth = async () => {
  // Create PKCE challenge
  const codeChallenge = await Crypto.digestStringAsync(
    Crypto.CryptoDigestAlgorithm.SHA256,
    Math.random().toString(),
    { encoding: Crypto.CryptoEncoding.BASE64URL }
  );

  const authUrl = AuthSession.buildAuthUrl({
    responseType: AuthSession.ResponseType.Code,
    clientId: 'booble_mobile',
    redirectUri,
    scopes: ['openid', 'profile', 'email', 'booble_api'],
    state: Math.random().toString(),
    codeChallenge,
    codeChallengeMethod: 'S256',
    prompt: AuthSession.Prompt.Login,
    issuer: 'https://localhost:7000',
  });

  const result = await AuthSession.startAsync({ authUrl });
  
  if (result.type === 'success') {
    // Exchange code for tokens
    return await exchangeCodeForTokens(result.params.code, codeChallenge);
  }
  
  throw new Error('Authentication cancelled');
};

const exchangeCodeForTokens = async (code, codeVerifier) => {
  const response = await fetch('https://localhost:7000/connect/token', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
    },
    body: new URLSearchParams({
      grant_type: 'authorization_code',
      client_id: 'booble_mobile',
      code,
      redirect_uri: redirectUri,
      code_verifier: codeVerifier,
    }).toString(),
  });

  return await response.json();
};
```

## API Usage Examples

### Legacy Login (Backward Compatibility)
```javascript
// This still works for existing clients
const loginResponse = await fetch('/api/Usuario/Login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    Des_Email: 'user@example.com',
    Senha: 'password123'
  })
});

const { Authtoken } = await loginResponse.json();
```

### OAuth2 Token Endpoint
```javascript
// Resource Owner Password Grant (for legacy compatibility)
const tokenResponse = await fetch('/api/oauth/token', {
  method: 'POST',
  headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
  body: new URLSearchParams({
    grant_type: 'password',
    username: 'user@example.com',
    password: 'password123',
    client_id: 'booble_legacy',
    scope: 'booble_api'
  })
});

const tokens = await tokenResponse.json();
```

### Making Authenticated API Calls
```javascript
// Using any of the obtained access tokens
const apiResponse = await fetch('/api/Usuario/PerfilUsuario', {
  headers: {
    'Authorization': `Bearer ${accessToken}`,
    'Content-Type': 'application/json'
  }
});

const userProfile = await apiResponse.json();
```

## Security Notes

1. **PKCE**: Always use PKCE for mobile and SPA applications
2. **HTTPS**: In production, ensure all endpoints use HTTPS
3. **Redirect URIs**: Register exact redirect URIs in production
4. **Secrets**: Keep client secrets secure and use environment variables
5. **Token Storage**: Store tokens securely (Keychain/Keystore for mobile)

## Endpoints Summary

- **Authorization**: `GET /connect/authorize`
- **Token**: `POST /connect/token`
- **UserInfo**: `GET /api/oauth/userinfo`
- **Revocation**: `POST /connect/revocation`
- **Discovery**: `GET /.well-known/openid-configuration`

## Production Configuration

1. Replace `AddDeveloperSigningCredential()` with a proper certificate
2. Update redirect URIs to production URLs
3. Enable HTTPS metadata validation
4. Configure proper CORS origins
5. Implement proper refresh token handling
6. Add rate limiting and security headers