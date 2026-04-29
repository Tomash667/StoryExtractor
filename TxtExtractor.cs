namespace StoryExtractor
{
    public class TxtExtractor
    {
        public static List<string> ExtractContents(string filePath)
        {
            string text = File.ReadAllText(filePath);
            List<string> parts = [];
            int currentPos = text.IndexOf("{{[INPUT]}}");
            if (currentPos == -1)
                throw new Exception("Invalid txt file format");
            while (currentPos < text.Length)
            {
                currentPos += "{{[INPUT]}}".Length;
                int end = text.IndexOf("{{[OUTPUT]}}", currentPos);
                if (end == -1)
                    throw new Exception("Invalid txt file format");
                currentPos = end + "{{[OUTPUT]}}".Length;
                end = text.IndexOf("{{[INPUT]}}", currentPos);
                if (end == -1)
                {
                    parts.Add(text[currentPos..].Trim());
                    break;
                }
                else
                {
                    parts.Add(text.Substring(currentPos, end - currentPos).Trim());
                    currentPos = end;
                }
            }
            return parts;
        }
    }
}
