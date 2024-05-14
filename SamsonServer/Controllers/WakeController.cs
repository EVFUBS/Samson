using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SamsonActionModel;
using SamsonActionModel.SamsonWake;
using SamsonCommon.Models;
using SamsonServer.Helpers;
using System.Drawing.Imaging;

namespace SamsonServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeController(IPredEngineHelper predEngineHelper) : ControllerBase
    {
        [HttpGet]
        public ActionResult<SamsonWake> Get(Stream data)
        {
            var predEngine = predEngineHelper.GetPredEngine<SamsonWakeClassification.ModelInput, SamsonWakeClassification.ModelOutput>();
            var melSpectogram = new SamsonWakePreprocess().CreateMelSpectogramFromStream(data);
            using (var stream = new MemoryStream())
            {
                melSpectogram.Save(stream, ImageFormat.Png);
                var melSpectogramByteArray = stream.ToArray();

                var prediction = predEngine.Predict(new SamsonWakeClassification.ModelInput
                {
                    ImageSource = melSpectogramByteArray,
                });

                if(prediction.PredictedLabel == "Wake") 
                {
                    return Ok(new SamsonWake
                    {
                        IsWake = true
                    });
                }
                else
                {
                    return Ok(new SamsonWake
                    {
                        IsWake = false
                    });
                }
            }
        }
    }
}
