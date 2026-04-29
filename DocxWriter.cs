using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace StoryExtractor
{
    public partial class DocxWriter
    {
        public static void SaveTextListToDocx(List<string> texts, string outputPath, bool chapters)
        {
            using var doc = DocX.Create(outputPath);

            var title = doc.InsertParagraph(Path.GetFileNameWithoutExtension(outputPath));
            title.FontSize(28);
            title.SpacingAfter(10);

            if (chapters)
            {
                int counter = 1;
                foreach (var text in texts)
                {
                    var paragraph = doc.InsertParagraph($"Chapter {counter}");
                    paragraph.StyleId = "Heading1";
                    paragraph.SpacingAfter(10);
                    ++counter;

                    InsertMarkdownParagraph(doc, text);

                    doc.InsertSectionPageBreak();
                }
            }
            else
            {
                foreach (var text in texts)
                    InsertMarkdownParagraph(doc, text);
            }

            doc.Save();
        }

        public static void InsertMarkdownParagraph(DocX doc, string markdown)
        {
            var paragraph = doc.InsertParagraph();

            int lastIndex = 0;
            var italicRegex = ItalicRegex(); // Matches *text* but not *** or **

            foreach (Match match in italicRegex.Matches(markdown))
            {
                // Add text before the match
                if (match.Index > lastIndex)
                {
                    string before = markdown[lastIndex..match.Index];
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
                string remaining = markdown[lastIndex..];
                paragraph.Append(remaining);
            }

            paragraph.SpacingAfter(10);
        }

        [GeneratedRegex(@"\*(?!\*\*)([^*\n]+)\*")]
        private static partial Regex ItalicRegex();
    }
}
