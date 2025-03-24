using Azure.AI.OpenAI;
using Azure.AI.TextToSpeech;
using System.Threading.Tasks;

public class Auctioneer
{
    private readonly OpenAIClient _aiClient;
    private readonly TextToSpeechClient _ttsClient;

    public Auctioneer(string openAiKey, string speechKey)
    {
        _aiClient = new OpenAIClient(openAiKey);
        _ttsClient = new TextToSpeechClient(speechKey);
    }

    public async Task<string> GenerateAuctionSpeech(string auctionTitle, decimal currentBid)
    {
        var response = await _aiClient.Completions.CreateCompletionAsync(
            new OpenAICompletionsOptions
            {
                Prompt = $"You are a fast-paced auctioneer. The item is {auctionTitle}. The current bid is ${currentBid}. Announce the next bid in an engaging way.",
                MaxTokens = 50
            });

        return response.Choices[0].Text;
    }

    public async Task GenerateSpeechAudio(string text)
    {
        await _ttsClient.SynthesizeSpeechToFileAsync(text, "auctioneer_speech.mp3");
    }
}
