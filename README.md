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
```text
QUERY
  deckId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  cardId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  side=string

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
```text
{
  query
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  cardId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  side=string
  
  IFormFile
  Image
}
```

## AddCardImageResponse
```json
{
  "result": "string"
}
```

## DeleteCardImageRequest
DELETE host/images
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Side": "string"
}
```

## DeleteCardImageResponse
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

## RefreshTokenRequest
POST host/account/refresh
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
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
  "login": "string"
}
```

## PasswordRecoveryCodeRequest
POST host/account/recovery/code
```json
{
  "login": "string",
  "code": "string"
}
```

## PasswordRecoveryNewPasswordRequest
POST host/account/recovery/newpass
```json
{
  "code": "string",
  "password": "string",
  "login": "string"
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
```text
QUERY
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6

```

## UserProfilePreviewResponse
```json
{
  "login": "string",
  "location": "string",
  "cardsViewed": 0,
  "hours": 0,
  "cardsCreated": 0,
  "score": 0,
  "avatarNumber": 0
}
```

## UserProfileRequest
GET host/users/profile
```text
QUERY
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6

```

## UserProfileEditResponse
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "location": "string",
  "avatarNumber": 0
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
  "location": "string",
  "avatarNumber": 0
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
GET host/editing/decks
```text
QUERY
userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
searchQuery=string
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

## AddCardRequest
POST host/editing/decks/addcard
```json
{
  "userId": "c5e39a9b-a4fc-42db-b6f9-82451275db1c",
  "deckId": "d9e227fc-8d98-43df-a6f7-d84593386f2f",
  "cardId": "cd9bb02e-9c4e-4770-8418-fef94a5a6e88"
}
```

## AddCardResponse
```json
{
  "result": "string"
}
```

## RemoveCardRequest
DELETE host/editing/decks/removecard
```json
{
  "userId": "c5e39a9b-a4fc-42db-b6f9-82451275db1c",
  "deckId": "d9e227fc-8d98-43df-a6f7-d84593386f2f",
  "cardId": "cd9bb02e-9c4e-4770-8418-fef94a5a6e88"
}
```

## RemoveCardResponse
```json
{
  "result": "string"
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
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "isPrivate": true,
  "password": "string"
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
  "description": "string"
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

## UserCardsRequest
GET host/editing/cards
```text
userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
searchQuery=string
```

## UserCardsResponse
```json
{
  "cards": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "frontSide": "string",
      "backSide": "string",
      "hasFrontImage": true,
      "hasBackImage": false
    }
  ]
}
```

## DeckCardsRequest
GET host/editing/cards/fromdeck
```text
userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
deckId=3fa85f64-5717-4562-b3fc-2c963f66afa6
searchQuery=string
```

## DeckCardsResponse
```json
{
  "DeckName": "string",
  "cards": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "frontSide": "string",
      "backSide": "string",
      "hasFrontImage": true,
      "hasBackImage": false
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
  "deckId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "frontSide": "string",
  "backSide": "string"
}
```

## CreateCardResponse
```json
{
  "cardId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
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
```text
QUERY
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  searchQuery=string
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
```text
QUERY
  deckId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6
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
  "deckName": "string",
  "description": "string",
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "ownerLogin": "string",
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
```text
QUERY
  deckId=3fa85f64-5717-4562-b3fc-2c963f66afa6
  userId=3fa85f64-5717-4562-b3fc-2c963f66afa6

```

## StudyCardsResponse
```json
{
  "deckName": "string",
  "cards": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "frontSide": "string",
      "backSide": "string",
      "hasFrontImage": true,
      "hasBackImage": false
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
