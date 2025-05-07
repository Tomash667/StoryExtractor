using StoryExtractor;
using System.Globalization;

public static partial class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
            return;

        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

        var modelTexts = JsonExtractor.ExtractModelContents(args[0]);
        DocxWriter.SaveTextListToDocx(modelTexts, args[0] + ".docx");
    }
}
