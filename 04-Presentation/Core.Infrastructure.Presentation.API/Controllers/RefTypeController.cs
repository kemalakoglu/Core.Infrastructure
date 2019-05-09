using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Application.Contract.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Infrastructure.Presentation.API.Controllers
{
    public class RefTypeController : Controller
    {
        private readonly ICoreApplicationService appService;

        public RefTypeController(ICoreApplicationService appService)
        {
            this.appService = appService;
        }

        /// <summary>
        ///     User Registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RefType/AddRefType")]
        [HttpPost]
        public IActionResult AddRefType([FromBody]RefTypeDTO request)
        {
            return Ok(this.appService.AddRefType(request));
        }
    }
}
