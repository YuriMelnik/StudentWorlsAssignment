using StudentWorlsAssignment.Models;

namespace StudentWorlsAssignment
{
    public sealed class AiReviewRequest
    {
        public string StudentName { get; }
        public IReadOnlyList<string> FilePaths { get; }

        public AiReviewRequest(string studentName, IReadOnlyList<FileItem> items)
        {
            StudentName = studentName;
            FilePaths = items
                .Select(i => i.FullPath)
                .ToList();    // List<string> неявно приводится к IReadOnlyList<string>
        }
    }

}
