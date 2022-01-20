namespace DebitCardApi.DAL.Models
{
    public class DebitCard : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public int SecureCode { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
