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
using StudentWorlsAssignment.Ai; // или твой реальный namespace

namespace StudentWorlsAssignment
{
    public partial class AiReviewForm : Form
    {
        
            private readonly AiReviewRequest _request;
            private readonly AiCodeReviewService _aiService;

            public AiReviewForm(AiReviewRequest request, AiCodeReviewService aiService)
            {
                InitializeComponent();

                _request = request;
                _aiService = aiService;

                labelStudent.Text = $"Студент: {_request.StudentName}";
                //checkedListBoxFiles.DataSource = _request.FilePaths;
            }
       
    }
}
