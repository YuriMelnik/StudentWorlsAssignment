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

            // по умолчанию все файлы,которые выбраны 
            // в списке файлов для ревью, отмечены в checkedListBox
            // дубликатов не будет, т.к. форма модальная и для изменения 
            // списка файлов нужно заново открыть форму
            foreach (var filePath in _request.FilePaths)
            {
                listBoxAiFiles.Items.Add(filePath); 
            }
            // TODO: ДОБАВИТЬ КОД ИЗ ФАЙЛОВ в prompt

        }
       
    }
}
