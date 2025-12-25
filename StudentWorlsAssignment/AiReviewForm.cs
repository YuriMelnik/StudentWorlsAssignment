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
using StudentWorlsAssignment.Ai;
using StudentWorlsAssignment.Services; // или твой реальный namespace

namespace StudentWorlsAssignment
{
    public partial class AiReviewForm : Form
    {

        private readonly AiReviewRequest _request;
        private readonly AiCodeReviewService _aiService;
        private readonly DocxTextExtractor _docxTextExtractor;

        public AiReviewForm(
            AiReviewRequest request, 
            AiCodeReviewService aiService,
            DocxTextExtractor docxTextExtractor)
        {
            InitializeComponent();

            _request = request;
            _aiService = aiService;
            _docxTextExtractor = docxTextExtractor;

            // Настраиваем элементы управления
            SetupUi();
        }

        private void SetupUi()
        {
            labelStudent.Text = $"Студент: {_request.StudentName}";

            // по умолчанию все файлы,которые выбраны 
            // в списке файлов для ревью, отмечены в checkedListBox
            // дубликатов не будет, т.к. форма модальная и для изменения 
            // списка файлов нужно заново открыть форму

            // 2. Заполняем список файлов для ревью
            listBoxAiFiles.Items.Clear();
            foreach (var file in _request.FilesToReview)
            {
                listBoxAiFiles.Items.Add(Path.GetFileName(file.FullPath));
            }
            // 3. Устанавливаем начальный статус
            labelStatus.Text = "Готов к оценке";
            progressBarAi.Visible = false;
        }

        private async void buttonGenerateReview_Click(object sender, EventArgs e)
        {
            // 1. Обновляем UI для индикации процесса
            richTextBoxAiReview.Clear();
            labelStatus.Text = "Подготавливаю запрос...";
            progressBarAi.Visible = true;
            progressBarAi.Style = ProgressBarStyle.Marquee; // Бегущая строка
            buttonGenerateReview.Enabled = false; // Блокируем кнопку на время выполнения

            try
            {
                // 2. Собираем промпт
                labelStatus.Text = "Отправляю запрос на AI-ревью...";
                string prompt = BuildPrompt();

                // 3. Отправляем запрос в асинхронном режиме
                string aiResponse = await _aiService.GetReviewAsync(prompt);

                // 4. Отображаем результат
                labelStatus.Text = "Оценка получена.";
                richTextBoxAiReview.Text = aiResponse;
            }
            catch (Exception ex)
            {
                // 5. Обрабатываем возможные ошибки
                labelStatus.Text = "Ошибка!";
                richTextBoxAiReview.Text = $"Не удалось получить отзыв от AI.\n\nДетали ошибки: {ex.Message}";
                MessageBox.Show($"Произошла ошибка при обращении к AI: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 6. Возвращаем UI в исходное состояние
                progressBarAi.Visible = false;
                buttonGenerateReview.Enabled = true;
            }
        }

        /// <summary>
        /// Собирает итоговый промпт для отправки в AI, подставляя все необходимые данные.
        /// </summary>
        private string BuildPrompt()
        {

            // Собираем содержимое всех файлов студента в одну строку
            var studentFilesContent = new StringBuilder();
            foreach (var file in _request.FilesToReview)
            {
                studentFilesContent.AppendLine($"--- ФАЙЛ: {Path.GetFileName(file.FullPath)} ---");
                try
                {
                    string content;
                    string ext = Path.GetExtension(file.FullPath).ToLowerInvariant();

                    // Проверяем расширение файла
                    if (ext == ".docx")
                    {
                        // Используем специальный извлекатель для DOCX
                        content = _docxTextExtractor.ExtractPlainTextFromDocx(file.FullPath);
                    }
                    else
                    {
                        // Используем универсальный метод для всех остальных текстовых файлов
                        content = ReadTextWithUTF8orANSI(file.FullPath);
                    }

                    studentFilesContent.AppendLine(content);
                }
                catch (Exception ex)
                {
                    studentFilesContent.AppendLine($"[ОШИБКА ЧТЕНИЯ ФАЙЛА: {ex.Message}]");
                }
                studentFilesContent.AppendLine(); // Пустая строка между файлами для читаемости
            }

            // Для удобства шаблон можно хранить в отдельном файле в проекте
            // и читать его через File.ReadAllText(). Но для примера оставим его здесь.
            string finalPrompt =  $@"
Ты — опытный преподаватель программирования и эксперт-разработчик. Твоя задача — объективно и конструктивно оценить работу студента по предоставленному заданию.

Ты должен оценить работу по 100-балльной шкале, основываясь на критериях ниже. В конце ответа предоставь итоговую оценку и подробное объяснение.

---

### 1. Условие задачи

{_request.AssignmentDescription}

---

### 2. Работа студента

Студент: {_request.StudentName}

Ниже представлены файлы, предоставленные студентом:

{studentFilesContent}

---

### Критерии оценки

Оцени работу по следующим критериям, распределяя 100 баллов:

**1. Корректность и функциональность (до 40 баллов)**
*   Программа полностью выполняет все требования из условия задачи.
*   Корректно обрабатывает стандартные и граничные случаи.
*   Программа стабильна и не падает с ошибками.
*   Вывод программы соответствует ожидаемому формату.

**2. Качество кода и читаемость (до 25 баллов)**
*   **Именование:** Переменные, методы и классы имеют осмысленные и понятные имена.
*   **Структура:** Код логически организован.
*   **Комментарии:** Ключевые места в коде прокомментированы.
*   **Форматирование:** Соблюдены единые стандарты форматирования.

**3. Алгоритм и эффективность (до 20 баллов)**
*   Выбранный алгоритм является оптимальным или эффективным.
*   Нет избыточных операций.
*   Продемонстрировано понимание ключевых алгоритмических концепций.

**4. Следование лучшим практикам (до 15 баллов)**
*   Соблюдаются базовые принципы программирования (например, DRY).
*   Правильно используются структуры данных.
*   Код безопасен (например, проверяются входные данные).

---

### Формат ответа

Предоставь ответ в следующем формате:

**Итоговая оценка: [X]/100**

**Краткое резюме:**
[Одно-два предложения, обобщающие общее впечатление от работы.]

**Детальная оценка по критериям:**

*   **Корректность и функциональность:** [Баллы]/40
    *   *Комментарий:* [Подробное объяснение, что работает, а что нет.]
*   **Качество кода и читаемость:** [Баллы]/25
    *   *Комментарий:* [Прокомментируй именование, структуру, комментарии.]
*   **Алгоритм и эффективность:** [Баллы]/20
    *   *Комментарий:* [Оцени эффективность предложенного решения.]
*   **Следование лучшим практикам:** [Баллы]/15
    *   *Комментарий:* [Укажи на сильные стороны и на то, что можно было бы улучшить.]

**Что сделано хорошо:**
*   [Список положительных моментов работы.]

**Что можно улучшить:**
*   [Список конкретных, конструктивных предложений по улучшению кода и логики. Приведи примеры, если возможно.]
";

             return finalPrompt;
        }

        // Нам понадобится тот же метод чтения файлов, что и в Form1.
        // Лучше вынести его в отдельный статический класс-хелпер, но для простоты продублируем здесь.
        private static string ReadTextWithUTF8orANSI(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            var utf8Strict = new UTF8Encoding(false, true);
            try
            {
                return utf8Strict.GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                return Encoding.GetEncoding(1251).GetString(bytes);
            }
        }

    }
}
