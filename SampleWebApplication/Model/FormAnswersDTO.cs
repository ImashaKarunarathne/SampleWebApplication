namespace SampleWebApplication.Model
{
    public class FormAnswersDTO
    {
        public required string Id { get; set; }

        public required string FormId { get; set; }

        public required string CandidateId { get; set; }

        public required List<QuestionAnswerDTO> QuestionAnswers { get; set; }
    }
}
