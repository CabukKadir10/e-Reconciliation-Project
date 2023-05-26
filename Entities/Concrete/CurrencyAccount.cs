using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CurrencyAccount : IEntity
    {
        public int Id { get; set; } 
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? TaxDepartment { get; set; } //null olabilir
        public string? TaxIdNumber { get; set; } //null olabilir
        public string? IdentityNumber { get; set; } //null olabilir
        public string? Email { get; set; } //null olabilir
        public string? Authorized { get; set; } //null olabilir
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
