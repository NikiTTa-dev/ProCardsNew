using Amazon.S3;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCardsNew.Api.Controllers.Common;
using ProCardsNew.Application.Service.Images.Commands.AddCardImage;
using ProCardsNew.Application.Service.Images.Commands.DeleteCardImage;
using ProCardsNew.Application.Service.Images.Queries.CardImage;
using ProCardsNew.Contracts.Common;
using ProCardsNew.Contracts.Service;

namespace ProCardsNew.Api.Controllers.Service;

[Route("images")]
public class ImageController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ImageController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> CardImage([FromQuery] CardImageRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        
        var command = _mapper.Map<CardImageQuery>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result =>
            {
                var response = _mapper.Map<CardImageResponse>(result);
                return File(response.Data, response.FileExtension);
            },
            errors => Problem(errors));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCardImage(
        [FromQuery]
        AddCardImageRequest request,
        IFormFile file)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;

        var command = new AddCardImageCommand(
            UserId: request.UserId,
            CardId: request.CardId,
            Side: request.Side,
            Data: file.OpenReadStream(),
            Name: file.Name,
            FileExtension: file.ContentType);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCardImage(DeleteCardImageRequest request)
    {
        if (request.UserId.ToString() != ClaimUserId)
            return AccessDenied;
        var command = _mapper.Map<DeleteCardImageCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }
}