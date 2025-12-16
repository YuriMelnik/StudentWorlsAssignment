using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentWorlsAssignment.Services.TextRendering
{
    public sealed class SyntaxHighlightRules
    {
        public Regex? LineComment { get; init; }
        public Regex? BlockComment { get; init; }
        public Regex? StringLiteral { get; init; }
        public IReadOnlyCollection<string> Keywords { get; init; } = Array.Empty<string>();
        public Color KeywordColor { get; init; }
        public Color CommentColor { get; init; }
        public Color StringColor { get; init; }



    }
}