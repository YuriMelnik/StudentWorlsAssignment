using StudentWorlsAssignment.Ai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentWorlsAssignment
{
    public partial class AiReviewForm : Form
    {
        private readonly AiReviewRequest _request;
        private readonly AiCodeReviewService _aiService;
        private Services.AiCodeReviewService aiCodeReviewService;

        public AiReviewForm(AiReviewRequest request, AiCodeReviewService aiService)
        {
            InitializeComponent();

            _request = request;
            _aiService = aiService;

            labelStudent.Text = $"Студент: {_request.StudentName}";    // метка сверху
            //checkedListBoxFiles.DataSource = _request.FilePaths;

            //if (_request.FilePaths.Count > 0)
            //    LoadFileToPreview(_request.FilePaths[0]);
        }

        public AiReviewForm(AiReviewRequest request, Services.AiCodeReviewService aiCodeReviewService)
        {
            _request = request;
            this.aiCodeReviewService = aiCodeReviewService;
        }
    }
}
