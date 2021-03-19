namespace RollCall.MVC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        [DisplayName("Street, Number...")]
        public string AddressField { get; set; }
    }
}