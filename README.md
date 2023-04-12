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

## CardImageRequest
GET host/images
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
  "image": "???"
}
```

## AddCardImageRequest
POST host/images
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "side": "string",
  "image": "string"
}
```

## AddCardImageResponse
```json
{
  "result": "string"
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
  "password": "string",
  "location": "string"
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

## PasswordRecoveryNewPasswordRequest
POST host/account/recovery/newpass
```json
{
  "code": "string",
  "password": "string",
  "email": "string"
}
```

## PasswordRecoveryResponse
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

## UserDecksToEditRequest
POST host/editing/decks/get
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "searchQuery": "string"
}
```

## UserDecksToEditResponse
```json
{
  "decks": [
    {
      "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "description": "string",
      "isPrivate": true
    }
  ]
}
```

## EditDeckRequest
PATCH host/editing/decks
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "description": "string"
}
```

## EditDeckResponse
```json
{
  "result": "string"
}
```

## EditDeckPasswordRequest
PATCH host/editing/decks/password
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "newPassword": "string"
}
```

## EditDeckPasswordResponse
```json
{
  "result": "string"
}
```

## CreateDeckRequest
POST host/editing/decks
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "description": "string",
  "isPrivate": true,
  "password": "string"
}
```

## CreateDeckResponse
```json
{
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "result": "string"
}
```

## DeleteDeckRequest
DELETE host/editing/decks
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## DeleteDeckResponse
```json
{
  "result": "string"
}
```

## DeckCardsRequest
GET host/editing/cards
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "searchQuery": "string"
}
```

## DeckCardsResponse
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

## EditCardRequest
PATCH host/editing/cards
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "frontSide": "string",
  "backSide": "string"
}
```

## EditCardResponse
```json
{
  "result": "string"
}
```

## CreateCardRequest
POST host/editing/cards
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "frontSide": "string",
  "backSide": "string"
}
```

## CreateCardResponse
```json
{
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "result": "string"
}
```

## DeleteCardRequest
DELETE host/editing/cards
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## DeleteCardResponse
```json
{
  "result": "string"
}
```

# Learning

## UserDecksRequest
GET host/decks
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "searchQuery": "string"
}
```

## UserDecksResponse
```json
{
  "decks": [
    {
      "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "isOwner": true
    }
  ]
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

## RemoveDeckFromLatestRequest
POST host/decks/remove
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

## RemoveDeckFromLatestResponse
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
      "hasFrontImage": true,
      "HasBackImage": false
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
  "grade": 0,
  "timeInSeconds": 0
}
```

## GradeCardResponse
```json
{
  "result": "string"
}
```
