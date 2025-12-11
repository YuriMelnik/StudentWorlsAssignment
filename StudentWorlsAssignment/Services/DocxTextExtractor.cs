using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;

namespace StudentWorlsAssignment.Services;

/// <summary>
/// Извлекает простой (plain) текст из DOCX-документов.
/// Без форматирования, только содержимое абзацев.
/// </summary>
public sealed class DocxTextExtractor
{
    public string ExtractPlainTextFromDocx(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return string.Empty;

        var sb = new StringBuilder();

        using var wordDoc = WordprocessingDocument.Open(filePath, false);
        Body? body = wordDoc.MainDocumentPart?.Document?.Body;
        if (body is null)
            return string.Empty;

        foreach (Paragraph paragraph in body.Elements<Paragraph>()) // абзацы DOCX [web:32][web:175]
        {
            string text = paragraph.InnerText;
            if (!string.IsNullOrWhiteSpace(text))
                sb.AppendLine(text);
        }

        return sb.ToString();
    }
}
