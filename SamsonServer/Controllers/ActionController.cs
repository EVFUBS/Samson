using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SamsonActionModel;
using SamsonCommon.Models;
using SamsonServer.Helpers;


namespace SamsonServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        IPredEngineHelper _predEngineHelper {  get; set; }
        public ActionController(IPredEngineHelper predEngineHelper)
        {
            _predEngineHelper = predEngineHelper;
        }

        [HttpGet]
        public ActionResult<SamsonAction> Get(string summary)
        {
            var predEngine = _predEngineHelper.GetPredEngine<SamsonActionClassification.ModelInput, SamsonActionClassification.ModelOutput>();
            var prediction = predEngine.Predict(new SamsonActionClassification.ModelInput
            {
                Text = @summary,
            });

            return Ok(prediction);
        }
    }
}
