using Com.Scm.Utils;

public class Program
{
    private static void Main(string[] args)
    {
        var key = "sk-0d3d83607d8142308675f3e390f2b9a8";
        var url = "https://api.deepseek.com/chat/completions";

        //        var header = new Dictionary<string, string>()
        //        {
        //            ["Accept"] = "application/json",
        //            ["Authorization"] = "Bearer " + key
        //        };
        //        var body = @"{" + "\n" +
        //@"  ""messages"": [" + "\n" +
        //@"    {" + "\n" +
        //@"      ""content"": ""You are a helpful assistant""," + "\n" +
        //@"      ""role"": ""system""" + "\n" +
        //@"    }," + "\n" +
        //@"    {" + "\n" +
        //@"      ""content"": ""Hi""," + "\n" +
        //@"      ""role"": ""user""" + "\n" +
        //@"    }" + "\n" +
        //@"  ]," + "\n" +
        //@"  ""model"": ""deepseek-chat""," + "\n" +
        //@"  ""frequency_penalty"": 0," + "\n" +
        //@"  ""max_tokens"": 2048," + "\n" +
        //@"  ""presence_penalty"": 0," + "\n" +
        //@"  ""response_format"": {" + "\n" +
        //@"    ""type"": ""text""" + "\n" +
        //@"  }," + "\n" +
        //@"  ""stop"": null," + "\n" +
        //@"  ""stream"": false," + "\n" +
        //@"  ""stream_options"": null," + "\n" +
        //@"  ""temperature"": 1," + "\n" +
        //@"  ""top_p"": 1," + "\n" +
        //@"  ""tools"": null," + "\n" +
        //@"  ""tool_choice"": ""none""," + "\n" +
        //@"  ""logprobs"": false," + "\n" +
        //@"  ""top_logprobs"": null" + "\n" +
        //@"}";

        //        var result = HttpUtils.PostJsonString(url, body, header);
        //        Console.WriteLine(result);
        var result = HttpUtils.GetString("http://www.baidu.com");
                Console.WriteLine(result);
    }
}
