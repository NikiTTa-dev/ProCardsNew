# ProCardsNew
ProCards project but new

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
HOST/account//recovery/code
```json
{
  "email": "string",
  "code": "string"
}
```

## PasswordRecoveryResponse
```json
{
  "status": {
    "statusCode": "string",
    "message": "string",
    "errors": [
      "string"
    ]
  }
}
```

## PasswordRecoveryNewRequest
HOST/account/recovery/newpass
```json
{
  "code": "string",
  "password": "string",
  "email": "string"
}
```

## PasswordRecoveryNewResponse
```json
{
  "status": {
    "statusCode": "string",
    "message": "string",
    "errors": [
      "string"
    ]
  }
}
```
