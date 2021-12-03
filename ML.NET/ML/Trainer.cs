using Microsoft.ML;
using ML.NET.ML.Base;
using ML.NET.ML.Objects;
using Shared.Internal;

namespace ML.NET.ML;

internal class Trainer : BaseMl
{
    // lets build a simple trainer class for sentiment analysis of a sentence
    // the method should take in the dataset

    // start with a method which loads the dataset into a IDataView
    // this method should return the IDataView
    private IDataView LoadData(string dataPath)
    {
        //check if dataPath is a valid data path
        if (string.IsNullOrEmpty(dataPath))
        {
            throw new Exception("Data path is not valid");
        }

        // check if the dataPath is a valid file
        if (!File.Exists(dataPath))
        {
            throw new Exception("Data file does not exist");
        }

        var data = MlContext.Data.LoadFromTextFile<SentimentData>(dataPath);
        return data;
    }

    // Lets write a method to split the data into training and test data
    // this method should return the training and test data
    private DataOperationsCatalog.TrainTestData SplitData(IDataView data)
    {
        // check if the data is valid
        if (data == null)
        {
            throw new Exception("Data is not valid");
        }

        // split the data into training and test data
        var splitData = MlContext.Data.TrainTestSplit(data, testFraction: 0.2);
        return splitData;
    }

    // lets create a method for creating the text pipeline
    // this method should return the text pipeline
    private IEstimator<ITransformer> CreateTextPipeline()
    {
        // create the text pipeline
        var textPipeline = TextCatalog.FeaturizeText(MlContext.Transforms.Text, outputColumnName: "Features",
            inputColumnName: nameof(SentimentData.SentimentText));
        return textPipeline;
    }

    // lets create a method for creating the trainer
    // this method should return the trainer
    private IEstimator<ITransformer> CreateTrainer()
    {
        // create the trainer
        var trainer = StandardTrainersCatalog.SdcaLogisticRegression(MlContext.BinaryClassification.Trainers,
            labelColumnName: nameof(SentimentData.Sentiment), featureColumnName: "Features");
        return trainer;
    }

    // Append the text pipeline and trainer to the pipeline
    // this method should return the pipeline
    private IEstimator<ITransformer> AppendPipeline(IEstimator<ITransformer> textPipeline,
        IEstimator<ITransformer> trainer)
    {
        // check if the text pipeline and trainer are valid
        if (textPipeline is null)
        {
            throw new Exception("Text pipeline is not valid");
        }

        // append the text pipeline and trainer to the pipeline
        var pipeline = textPipeline.Append(trainer);
        return pipeline;
    }

    // lets create a method for training the pipeline
    // this method should return the trained pipeline
    // it should take the pipeline and the training data
    private ITransformer TrainPipeline(IEstimator<ITransformer> pipeline, DataOperationsCatalog.TrainTestData splitData)
    {
        // check if the pipeline
        if (pipeline is null)
        {
            throw new Exception("Pipeline is not valid");
        }

        //check if splitData Test and Training Data are not null
        if (splitData is {TrainSet: null} or {TestSet: null})
        {
            throw new Exception("Split data is not valid");
        }

        // train the pipeline
        var trainedPipeline = pipeline.Fit(splitData.TrainSet);
        return trainedPipeline;
    }

    // create a method for saving the model
    // this method should take in the trained pipeline and the datasplit 
    // it should save the model to a file
    private void SaveModel(ITransformer trainedPipeline, DataOperationsCatalog.TrainTestData splitData)
    {
        // check if the trained pipeline and split data are valid
        if (trainedPipeline is null)
        {
            throw new Exception("Trained pipeline is not valid");
        }

        //check if splitData Test and Training Data are not null
        if (splitData is {TrainSet: null} or {TestSet: null})
        {
            throw new Exception("Split data is not valid");
        }

        // save the model to a file
        MlContext.Model.Save(trainedPipeline, splitData.TrainSet.Schema, ModelPath);
    }

    // Prepare Test Data
    // It should take in our dataSplit Testset and return the test data
    private IDataView PrepareTestData(ITransformer trainedModel, DataOperationsCatalog.TrainTestData splitData)
    {
        // check if the split data is valid
        if (splitData is {TrainSet: null} or {TestSet: null})
        {
            throw new Exception("Split data is not valid");
        }

        // transform testData and return the test data
        var testData = trainedModel.Transform(splitData.TestSet);
        return testData;
    }

    // Evaluate the model
    // It should take our test data and evaluate the model
    private TrainingEvaluation EvaluateModel(IDataView testData)
    {
        // check if the test data is valid
        if (testData is null)
        {
            throw new Exception("Test data is not valid");
        }

        // evaluate the model
        var modelMetrics = MlContext.BinaryClassification.Evaluate(testData,
            labelColumnName: nameof(SentimentData.Sentiment), scoreColumnName: nameof(SentimentPrediction.Score));

        var result = new TrainingEvaluation
        {
            Accuracy = modelMetrics.Accuracy,
            F1Score = modelMetrics.F1Score,
            AreaUnderRocCurve = modelMetrics.AreaUnderRocCurve,
            PositiveRecall = modelMetrics.PositiveRecall,
            NegativeRecall = modelMetrics.NegativeRecall,
            AreaUnderPrecisionRecallCurve = modelMetrics.AreaUnderPrecisionRecallCurve
        };

        return result;
    }

    // let's create the public train method which takes in the filepath
    // and return nothing
    public TrainingEvaluation Train(string filePath)
    {
        // check if the filepath is valid
        if (string.IsNullOrEmpty(filePath))
        {
            throw new Exception("File path is not valid");
        }

        // create the data
        var data = LoadData(filePath);

        //Split the data
        var splitData = SplitData(data);

        // create the pipeline
        var pipeline = CreateTextPipeline();

        // Create the trainer
        var trainer = CreateTrainer();

        var appendPipeline = AppendPipeline(pipeline, trainer);

        // Train the pipeline
        var trainedModel = TrainPipeline(appendPipeline, splitData);

        // save the model
        SaveModel(trainedModel, splitData);

        // evaluate the model
        var testData = PrepareTestData(trainedModel, splitData);
        return EvaluateModel(testData);
    }
}