using Microsoft.Extensions.ML;

namespace SamsonServer.Helpers
{
    public class PredEngineHelper : IPredEngineHelper
    {
        IServiceProvider _services { get; set; }

        public PredEngineHelper(IServiceProvider services)
        {
            _services = services;
        }

        public PredictionEnginePool<TData, TPrediction> GetPredEngine<TData, TPrediction>() where TData : class where TPrediction : class, new()
        {
            var predEngine = _services.GetRequiredService<PredictionEnginePool<TData, TPrediction>>();
            return predEngine;
        }
    }
}
