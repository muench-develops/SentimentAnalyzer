namespace Shared.Internal;

public class TrainingEvaluation
{
    public double AreaUnderRocCurve { get; set; }
    public double AreaUnderPrecisionRecallCurve { get; set; }
    public double Accuracy { get; set; }
    public double PositiveRecall { get; set; }
    public double NegativeRecall { get; set; }
    public double F1Score { get; set; }
}