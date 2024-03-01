﻿using System.Net;
using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketController : ApiController
{
  private readonly IMediator _mediator;
  private readonly DiscountGrpcService _discountGrpcService;

  public BasketController(IMediator mediator, DiscountGrpcService discountGrpcService)
  {
      _mediator = mediator;
      _discountGrpcService = discountGrpcService;
  }

    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var basket = await _mediator.Send(query);
        return Ok(basket);
    }

    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
    {
    
         foreach (var item in createShoppingCartCommand.Items)
         {
             var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
             item.Price -= coupon.Amount;
         }
         var basket = await _mediator.Send(createShoppingCartCommand);
         return Ok(basket);
    }

  [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        return Ok(await _mediator.Send(command));
    }
}
