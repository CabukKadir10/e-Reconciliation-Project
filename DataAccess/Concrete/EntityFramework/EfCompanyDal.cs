using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, ContextDb>, ICompanyDal
    {
        public UserCompany GetCompany(int userId)
        {
            using(var context = new ContextDb())
            {
                var result = context.UserCompanys.Where(p =>  p.UserId == userId).FirstOrDefault();
                return result;
            }
        }

        public void UserCompanyAdd(int userId, int compnayId)
        {
            using (var context = new ContextDb())
            {
                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = compnayId,
                    AddedAt = DateTime.Now,
                    IsActive = true
                };
                context.UserCompanys.Add(userCompany);
                context.SaveChanges();
            }
        }
    }
}
