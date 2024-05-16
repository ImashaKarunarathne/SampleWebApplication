using SampleWebApplication.Enums;
using System.Collections.Generic;

namespace SampleWebApplication.Model
{
    public class FormDTO
    {
        public required string Id { get; set; }

        public required string ProgramId { get; set; }

        public List<QuestionDTO> FixedQuestions { get; set; }

        public List<CustomQuestionDTO>? CustomQuestions { get; set; }

        public FormDTO()
        {
            FixedQuestions = new List<QuestionDTO>
            {
                new QuestionDTO { Type = QuestionType.Text, Label = "First Name", IsVisible = true , IsInternal = true },
                new QuestionDTO { Type = QuestionType.Text, Label = "Last Name", IsVisible = true , IsInternal = true  },
                new QuestionDTO { Type = QuestionType.Text, Label = "Email", IsVisible = true , IsInternal = true  },
                new QuestionDTO { Type = QuestionType.Text, Label = "Nationality", IsVisible = true , IsInternal = true },
                new QuestionDTO { Type = QuestionType.Text, Label = "Current Residence", IsVisible = true , IsInternal = true },
                new QuestionDTO { Type = QuestionType.Text, Label = "ID Number", IsVisible = true , IsInternal = true },
                new QuestionDTO { Type = QuestionType.Date, Label = "Date of Birth", IsVisible = true , IsInternal = true  },
                new QuestionDTO { Type = QuestionType.Dropdown, Label = "Gender", IsVisible = true , IsInternal = true }
            };
        }
    }
}
