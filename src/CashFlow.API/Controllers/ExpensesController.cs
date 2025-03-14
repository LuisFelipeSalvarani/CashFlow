﻿using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public ActionResult Register([FromBody] RequestRegisterExpenseJson request)
        {
            return Created();
        }
    }
}
