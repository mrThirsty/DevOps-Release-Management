using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Data.Model.API;
using ReleaseManagement.Framework.Interfaces;
using System.Web;

namespace ReleaseManagement.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ReleaseController : Controller
    {
        public ReleaseController(IReleaseDataService service, IComponentDataService compService)
        {
            _service = service;
            _compService = compService;
        }

        private readonly IReleaseDataService _service;
        private readonly IComponentDataService _compService;

        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<ReleaseHeader>> Get()
        {
            List<ReleaseHeader> result = new List<ReleaseHeader>();

            

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{releaseName}")]
        public async Task<ActionResult<ReleaseHeader>> Get(string releaseName)
        {
            ReleaseHeader result = new ReleaseHeader();

            var response = await _service.Find(i => i.ReleaseName.Equals(releaseName));

            if(response.OperationStatus != Framework.Enums.OperationResult.Success)
                return NotFound(releaseName);

            var found = response.Result.FirstOrDefault();

            if(found == null) return NotFound(releaseName);

            result.ReleaseName = found.ReleaseName;

            var componentsResponse = await _service.Find<ComponentApproval>(i => i.ReleaseId.Equals(found.Id));

            if(componentsResponse.OperationStatus != Framework.Enums.OperationResult.Success)
                return Problem(detail: "Not able to find any approvals for this release.", statusCode: (int)HttpStatusCode.NotFound);

            foreach(var comp in componentsResponse.Result)
            {
                var compParentResponse = await _compService.Find(comp.ComponentId);

                if(compParentResponse.OperationStatus != Framework.Enums.OperationResult.Success) continue;

                result.Components.Add(new ReleaseComponent()
                {
                    ComponentName = compParentResponse.Result.ComponentName,
                    Approved = comp.Approved,
                    ApprovedBy = comp.ApprovedBy,
                    DateApproved = comp.ApprovalDate.ToString("dd/MM/yyyy")
                });

            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> DevOpsEvent(string json)
        {
            try
            {

            }
            catch(Exception ex)
            {
                return Problem("Something went wrong");
            }

            return Ok();
        }
    }
}
