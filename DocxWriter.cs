using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace StoryExtractor
{
    public class DocxWriter
    {
        public static void SaveTextListToDocx(List<string> texts, string outputPath)
        {
            using (var doc = DocX.Create(outputPath))
            {
                foreach (var text in texts)
                {
                    InsertMarkdownParagraph(doc, text);
                }
                doc.Save();
            }
        }

        public static void InsertMarkdownParagraph(DocX doc, string markdown)
        {
            var paragraph = doc.InsertParagraph();

            int lastIndex = 0;
            var italicRegex = new Regex(@"\*(?!\*\*)([^*\n]+)\*"); // Matches *text* but not *** or **

            foreach (Match match in italicRegex.Matches(markdown))
            {
                // Add text before the match
                if (match.Index > lastIndex)
                {
                    string before = markdown.Substring(lastIndex, match.Index - lastIndex);
                    paragraph.Append(before);
                }

                // Add italic text
                string italicText = match.Groups[1].Value;
                paragraph.Append(italicText).Italic();

                lastIndex = match.Index + match.Length;
            }

            // Add remaining text
            if (lastIndex < markdown.Length)
            {
                string remaining = markdown.Substring(lastIndex);
                paragraph.Append(remaining);
            }

            paragraph.SpacingAfter(10);
        }
    }
}
