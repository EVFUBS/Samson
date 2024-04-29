using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SamsonActionModel;
using SamsonCommon.Enums;
using SamsonCommon.Helpers;
using SamsonCommon.Models;
using SamsonConsoleApp.Helpers;
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

            var action = prediction.PredictedLabel.ToEnum<Actions>();

            return Ok(new SamsonAction
            {
                Action = action,
                Catergory = CatergoryHelper.GetCatergory(action),
                // Will be filled out by ner model later
                Parameters = new ActionParameters { }
            });
        }
    }
}
