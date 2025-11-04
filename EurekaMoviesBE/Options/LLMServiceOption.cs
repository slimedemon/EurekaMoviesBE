namespace EurekaMoviesBE.Options;

public class LLMServiceOption
{
    public const string OptionName = "LLMService";
    public string LLMApiKey { get; set; }
    public string AiHost { get; set; } = "https://awd-llm.azurewebsites.net";
    public string AiNavigationPath { get; set; } = "navigate/";
    public string LlmRetrieverPath { get; set; } = "retriever/";
    public string LlmRagPath { get; set; } = "rag/";
}