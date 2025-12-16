using System.Text.RegularExpressions;

namespace StudentWorlsAssignment.Services.TextRendering
{
    public sealed class SyntaxHighlighter
    {
        private readonly Regex _csLineComment = new(@"//.*$", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _csBlockComment = new(@"/\*.*?\*/", RegexOptions.Singleline | RegexOptions.Compiled);
        private readonly Regex _csString = new("\"[^\"]*\"", RegexOptions.Compiled);

        private readonly Regex _pyComment = new(@"#.*$", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Regex _pyString = new("\"[^\"]*\"|'[^']*'", RegexOptions.Compiled);

        private readonly IReadOnlyCollection<string> _csharpKeywords;
        private readonly IReadOnlyCollection<string> _pythonKeywords;

        public SyntaxHighlighter()
        {
        }

        public SyntaxHighlighter(
            IReadOnlyCollection<string> csharpKeywords,
            IReadOnlyCollection<string> pythonKeywords)
        {
            _csharpKeywords = csharpKeywords ?? Array.Empty<string>();
            _pythonKeywords = pythonKeywords ?? Array.Empty<string>();
        }

        
        public void HighlightCSharp(RichTextBox rtb)
        {
            rtb.SuspendLayout();

            rtb.Select(0, rtb.TextLength);
            rtb.SelectionColor = Color.Black;

            string text = rtb.Text;

            foreach (Match m in _csLineComment.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.Green;
            }

            foreach (Match m in _csBlockComment.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.Green;
            }

            foreach (Match m in _csString.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.Brown;
            }

            foreach (string kw in _csharpKeywords)
            {
                var kwRegex = new Regex(@"\b" + Regex.Escape(kw) + @"\b", RegexOptions.Compiled);
                foreach (Match m in kwRegex.Matches(text))
                {
                    rtb.Select(m.Index, m.Length);
                    rtb.SelectionColor = Color.Blue;
                }
            }

            rtb.Select(0, 0);
            rtb.SelectionColor = Color.Black;

            rtb.ResumeLayout();
        }

        public void HighlightPython(RichTextBox rtb)
        {
            rtb.SuspendLayout();

            rtb.Select(0, rtb.TextLength);
            rtb.SelectionColor = Color.Black;

            string text = rtb.Text;

            foreach (Match m in _pyComment.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.Green;
            }

            foreach (Match m in _pyString.Matches(text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = Color.Brown;
            }

            foreach (string kw in _pythonKeywords)
            {
                var kwRegex = new Regex(@"\b" + Regex.Escape(kw) + @"\b", RegexOptions.Compiled);
                foreach (Match m in kwRegex.Matches(text))
                {
                    rtb.Select(m.Index, m.Length);
                    rtb.SelectionColor = Color.OrangeRed;
                }
            }

            rtb.Select(0, 0);
            rtb.SelectionColor = Color.Black;

            rtb.ResumeLayout();
        }
    }


}
