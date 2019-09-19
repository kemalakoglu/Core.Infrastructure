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
        /// AddRefType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RefType/AddRefType")]
        [HttpPost]
        public IActionResult AddRefType([FromBody]AddRefTypeRequestDTO request)
        {
            return Ok(this.appService.AddRefType(request));
        }

        /// <summary>
        /// Update Ref Type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RefType/UpdateRefType")]
        [HttpPost]
        public IActionResult UpdateRefType([FromBody]RefTypeDTO request)
        {
            return Ok(this.appService.UpdateRefType(request));
        }

        /// <summary>
        /// GetRefTypesByParent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RefType/GetRefTypesByParent")]
        [HttpGet]
        public IActionResult GetRefTypesByParent(long parentId)
        {
            return Ok(this.appService.GetRefTypesByParent(parentId));
        }

        /// <summary>
        /// GetRefTypesByParent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RefType/DeleteRefType")]
        [HttpGet]
        public IActionResult DeleteRefType(long id)
        {
            return Ok(this.appService.DeleteRefType(id));
        }
    }
}
