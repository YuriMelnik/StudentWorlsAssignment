namespace StudentWorlsAssignment
{
    public sealed class AiReviewRequest
    {
        public string StudentName { get; }
        public IReadOnlyList<string> FilePaths { get; }

        public AiReviewRequest(string studentName, IReadOnlyList<string> filePaths)
        {
            StudentName = studentName;
            FilePaths = filePaths;
        }
    }

}
