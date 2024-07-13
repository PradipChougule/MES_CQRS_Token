using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Core.Domain
{
    public class UserManagement
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserPin { get; set; }
        public string? Password { get; set; }
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
        public bool? IsActive { get; set; } = true;
        public DateTime? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
