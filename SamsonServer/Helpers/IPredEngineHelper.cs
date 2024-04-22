using Microsoft.Extensions.ML;

namespace SamsonServer.Helpers
{
    public interface IPredEngineHelper
    {
        PredictionEnginePool<TData, TPrediction> GetPredEngine<TData, TPrediction>()
            where TData : class
            where TPrediction : class, new();
    }
}