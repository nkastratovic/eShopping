﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controller
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
    }
}