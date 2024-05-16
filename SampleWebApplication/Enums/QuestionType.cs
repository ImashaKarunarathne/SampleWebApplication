using System.ComponentModel;

namespace SampleWebApplication.Enums
{
    public enum QuestionType
    {
        [Description("Text")]
        Text = 1,
        [Description("Numeric")]
        Numeric = 2,
        [Description("Date")]
        Date = 3,
        [Description("Dropdown")]
        Dropdown = 4,
        [Description("Yes/No")]
        YesNo = 5,
        [Description("Paragraph")]
        Paragraph = 6
    }
}
