namespace Shared;

public class AnalyzeRequest
{
    // Properties for holding a sentence, the requestor, the created date
    public string Sentence { get; set; }
    public string Requestor { get; set; }
    public DateTime CreatedDate { get; set; }
}