# ProCardsNew
ProCards project but new

---

# Global

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
## GlobalStatisticRequest
GET host/statistic

## GlobalStatisticResponse
```json
{
  "statistics": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "login": "string",
      "score": 0
    }
  ]
}
```

# Authentication

## RegisterRequest
POST host/account/register
```json
{
  "login": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string",
  "password": "string"
}
```

## LoginRequest
POST host/account/login
 ```json
{
  "login": "string",
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

## PasswordRecoveryRequest
POST host/account/recovery
```json
{
  "email": "string"
}
```

## PasswordRecoveryCodeRequest
POST host/account/recovery/code
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
POST host/account/recovery/newpass
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

# Profile

## UserProfilePreviewRequest
GET host/users/preview
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## UserProfilePreviewResponse
```json
{
  "login": "string",
  "location": "string",
  "cardsViewed": 0,
  "hours": 0,
  "cardsCreatedCount": 0,
  "score": 0
}
```

## UserProfileRequest
GET host/users/profile
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## UserProfileEditResponse
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "location": "string"
}
```

## EditProfileRequest
PATCH host/users/profile
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "location": "string"
}
```

## EditProfileResponse
```json
{
  "result": "string"
}
```

## EditPasswordRequest
PATCH host/users/password
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "oldPassword": "string",
  "newPassword": "string"
}
```

## EditPasswordResponse
```json
{
  "result": "string"
}
```

# Creating

TODO

# Learning

## UserDecksRequest
GET host/decks
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

## DeckRequest
GET host/decks/deck
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## AddDeckRequest
POST host/decks/add
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "password": "string"
}
```

TODO: check if user already have this deck
## DeckResponse
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "description": "string",
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardsCount": 0,
  "statistics": [
    {
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "login": "string",
      "score": 0
    }
  ]
}
```

## RemoveDeckRequest
POST host/decks/remove
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## RemoveDeckResponse
```json
{
  "result": "string"
}
```

## StudyCardsRequest
GET host/cards
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## StudyCardsResponse
```json
{
  "cards": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "frontSide": "string",
      "backSide": "string",
      "frontSideImageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "backSideImageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ]
}
```

## GradeCardRequest
POST host/cards/grade
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "grade": 0
}
```

## GradeCardResponse
```json
{
  "result": "string"
}
```

## CardImageRequest
DELETE host/cards/image
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "side": "string"
}
```

## CardImageResponse
```json
{
  "result": "string",
  "image": "string"
}
```
