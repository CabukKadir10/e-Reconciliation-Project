using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForRegisterTpSecondAccountDto : UserForRegister, IDto
    {
        public int CompanyId { get; set; }
    }
}
