namespace Manufacturing.Core.Features
{
    public class GetUserDetailsResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserPin { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? District { get; set; }
        public string? Taluka { get; set; }
        public string? PinCode { get; set; }
        public string? Address { get; set; }
        public string? LandMark { get; set; }
        public string? AdharCardNumber { get; set; }
        public string? PanCardNumber { get; set; }
        public byte[]? Photo { get; set; }
        public List<string>? Roles { get; set; }
    }
}
