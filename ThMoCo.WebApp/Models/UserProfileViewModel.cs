namespace ThMoCo.WebApp.Models
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ProfileImage { get; set; }

        // Payment card fields
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        // Address fields
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }

}
