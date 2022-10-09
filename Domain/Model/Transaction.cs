namespace Domain.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PayerName { get; set; }
        public int Points { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}