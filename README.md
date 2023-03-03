## Error
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "Invalid credentials.",
  "status": 409,
  "traceId": "00-2cd079513ad7748a9de4c102e411e1e0-4646f79d85d4a417-00",
  "errorCodes": [
    "Auth.InvalidCredentials"
  ]
}
```

## RegisterRequest
HOST/account/register
```json
{
  "login": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string",
  "password": "string"
}
```

## AuthenticationResponse
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "login": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string",
  "token": "string"
}
```

## LoginRequest
HOST/account/login
 ```json
{
  "login": "string",
  "password": "string"
}
```

## PasswordRecoveryRequest
HOST/account/recovery
```json
{
  "email": "string"
}
```

## PasswordRecoveryCodeRequest
HOST/account/recovery/code
```json
{
  "email": "string",
  "code": "string"
}
```

## PasswordRecoveryResponse
```json
{
  "result": "string"
}
```

## PasswordRecoveryNewPasswordRequest
HOST/account/recovery/newpass
```json
{
  "code": "string",
  "password": "string",
  "email": "string"
}
```

## PasswordRecoveryNewPasswordResponse
```json
{
  "result": "string"
}
```

## UserDecksRequest
HOST/decks
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "searchQuery": "string",
  "page": 0
}
```

## UserDecksResponse
```json
{
  "decks": [
    {
      "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "owner": true
    }
  ],
  "pagesCount": 0
}
```

## GetDeckRequest
HOST/decks/get
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## GetDeckResponse
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "description": "string",
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardsCount": 0,
  "users": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "score": 0
    }
  ]
}
```

## AddDeckRequest
HOST/decks/add
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "password": "string"
}
```

## AddDeckResponse
TODO: check if user already have this deck
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "description": "string",
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardsCount": 0,
  "users": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "score": 0
    }
  ]
}
```
