﻿using SampleWebApplication.Enums;

namespace SampleWebApplication.Model
{
    public class CustomQuestionDTO
    {
        public required QuestionType Type { get; set; }

        public required string Label { get; set; }

        public List<string>? Options { get; set; }
    }
}
