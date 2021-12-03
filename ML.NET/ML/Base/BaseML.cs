using Microsoft.ML;

namespace ML.NET.ML.Base;

public class BaseMl
{
    protected readonly MLContext MlContext;

    protected BaseMl()
    {
        MlContext = new MLContext();
    }

    protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Common.Constants.ModelFilename);
}