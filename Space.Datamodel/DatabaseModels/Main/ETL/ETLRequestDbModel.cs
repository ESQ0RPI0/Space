using System.ComponentModel.DataAnnotations;

namespace Space.Server.Datamodel.DatabaseModels.Main.ETL
{
    public class ETLRequestDbModel
    {
        [Key]
        public long Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
    }
}
