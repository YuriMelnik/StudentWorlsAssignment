using StudentWorksAssignment.Models;

namespace StudentWorksAssignment
{
    public sealed class AiReviewRequest
    {
        public string StudentName { get; }
        public List<FileItem> FilesToReview { get; }
        public string AssignmentDescription { get; } // Добавьте это свойство

        public AiReviewRequest(
            string studentName,
            List<FileItem> filesToReview,
            string assignmentDescription)
        {
            StudentName = studentName;
            FilesToReview = filesToReview;
            AssignmentDescription = assignmentDescription;
        }
    }

}
