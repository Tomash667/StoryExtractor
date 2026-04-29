using System.Globalization;

namespace StoryExtractor
{
    public static partial class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
                return;

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            List<string> contents;
            bool chapters;
            if (Path.GetExtension(args[0]) == ".txt")
            {
                contents = TxtExtractor.ExtractContents(args[0]);
                chapters = false;
            }
            else
            {
                contents = JsonExtractor.ExtractModelContents(args[0]);
                chapters = true;
            }
            string path = Path.GetFileNameWithoutExtension(args[0]) + ".docx";
            DocxWriter.SaveTextListToDocx(contents, path, chapters);
        }
    }
}
