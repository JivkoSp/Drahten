import requests
from jose import jwt


def authorize(token):

    # The result.
    decoded_token = None

    # Retrieve the JWKS from Keycloak
    jwks_url = "http://keycloak_auth_service:8080/realms/drahten/protocol/openid-connect/certs"
    jwks_response = requests.get(jwks_url)
    # Raise exception for non-200 status codes
    jwks_response.raise_for_status()  
    jwks = jwks_response.json()

    # Extract the key ID (kid) from the JWT header
    headers = jwt.get_unverified_header(token)
    kid = headers["kid"]
                
    print(f"The key ID from the JWT header: {kid}\n\n")
                
    public_key = None

    # Find the corresponding public key in the JWKS based on the kid
    for key in jwks["keys"]:
        if key["kid"] == kid:
            public_key = key
            break
                
    print(f"The PUBLIC KEY: {public_key}\n\n")

    if public_key:
        # Verify the JWT token using the public key
        # *** IMPORTANT ***
        # The audience is NOT verifyed. For DEVELOPMENT ONLY!
        decoded_token = jwt.decode(token, public_key, algorithms=["RS256"], options={"verify_aud": False})
    else:
        raise ValueError("No matching key found in JWKS")
    
    return decoded_token
