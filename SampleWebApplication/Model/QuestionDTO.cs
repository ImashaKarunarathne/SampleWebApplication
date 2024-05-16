using SampleWebApplication.Enums;

namespace SampleWebApplication.Model
{
    public class QuestionDTO
    {
        public required QuestionType Type { get; set; }

        public required string Label { get; set; }

        public bool IsVisible { get; set; }

        public bool IsInternal { get; set; }
    }
}
