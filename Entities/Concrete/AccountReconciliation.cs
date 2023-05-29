using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AccountReconciliation : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CurrencyAccountId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrencyDebit { get; set; }
        public decimal CurrencyCredit { get; set; }
        public bool IsSendEmail { get; set; }
        public DateTime? sendEmailDate { get; set; } //null olabilir.
        public bool? IsEmailRead { get; set; } //null olabilir.
        public DateTime? EmailReadDate { get; set; } //null olabilir.
        public DateTime? ResultDate { get; set; } //null olabilir.
        public bool? IsResultSucceed { get; set; } //null olabilir.
        public string? ResultNote { get; set; } //null olabilir.
        public string? Guid { get; set; } //null olabilir.
    }
}
