using Microsoft.Extensions.ML;

namespace SamsonServer.Helpers
{
    public class PredEngineHelper(IServiceProvider services) : IPredEngineHelper
    {
        public PredictionEnginePool<TData, TPrediction> GetPredEngine<TData, TPrediction>() where TData : class where TPrediction : class, new()
        {
            var predEngine = services.GetRequiredService<PredictionEnginePool<TData, TPrediction>>();
            return predEngine;
        }
    }
}
