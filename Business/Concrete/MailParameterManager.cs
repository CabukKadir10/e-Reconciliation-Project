using Business.Abstract;
using Business.Constans;
using Core.Aspects.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MailParameterManager : IMailParameterService
    {
        #region Dependency
        private readonly IMailParameterDal _mailParameterDal;

        public MailParameterManager(IMailParameterDal mailParameterDal)
        {
            _mailParameterDal = mailParameterDal;
        }
        #endregion

        [CacheAspect(60)]
        public IDataResult<MailParameter> Get(int companyId)
        {
            return new SuccessDataResult<MailParameter>(_mailParameterDal.Get(m => m.CompanyId== companyId));
        }

        [CacheRemoveAspect("IMailParameterService.Get")]
        public IResult Update(MailParameter mailParameter)
        {
            var result = Get(mailParameter.CompanyId);
            if(result.Data == null)
            {
                _mailParameterDal.Add(mailParameter);
            }else
            {
                result.Data.SMTP = mailParameter.SMTP;
                result.Data.Port = mailParameter.Port;
                result.Data.SSL = mailParameter.SSL;
                result.Data.Email = mailParameter.Email;
                result.Data.Password = mailParameter.Password;
                _mailParameterDal.Update(result.Data);
            }

            return new SuccessResult(Messages.MailParameterUpdated);
        }
    }
}
