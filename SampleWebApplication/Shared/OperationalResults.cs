namespace SampleWebApplication.Shared
{
    public class OperationalResults
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public required byte[] ConcurrencyKey { get; set; }

        public int Id { get; set; }

        public required string UpdatedByName { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsWarning { get; set; }
    }
}
