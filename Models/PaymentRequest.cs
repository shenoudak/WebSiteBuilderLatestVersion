using System;
using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class PaymentRequest
    {
        [Key]
        public int PaymentRequestId { get; set; }
        public string AffiliateId { get; set; }
        public DateTime RequestData { get; set; }
        public double CurrentBalance { get; set; }
        public double RequestPoints { get; set; }
        public double ConfirmPoints { get; set; }
        public double ConfirmAmount { get; set; }
        public bool IsPaid { get; set; }
        public int RequestStatusId { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
