using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.Constans;
using Core.Entities.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Entities.Dtos;
using Core.Aspects.AutoFac.Transaction;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        #region dependency injection
        //şimdi akla şu soru gelebilir biz bunu dal katmanında yaptık niye aynı şeyı buradada yazıyoruz. bunun sebebi biz service katmanında iş kuralları yazıyoruz. bunlarlar beraber kontrol etmemiz gereken şeyler de var. mesela kullanıcı giriş izni var mı(authentication), kullanıcı giriş yaptı ama buna yetkisi var mı(authorization), log işlemleri, validation işlemlerinin hepsini burada yapıyoruz.
        private readonly ICompanyDal _companyDal; //burda direk classı değilde soyut olan interface kullanıyoruz. bunun sebebi bağımlı hale getitmemek için. interfaceler newlenmediği için bunu çağırması için contructorda referans veriyoruz. yani IcompanyDal ==> EfCompanyDal eşleştirmesi yapıyoruz. yarın bir değişiklik yapmak istediğimizde kodda tek tek düzeltmek yerine sadece referansı değiştirebiliriz.
        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }
        #endregion


        [ValidationAspect(typeof(CompanyValidator))]
        public IResult Add(Company company)
        {
            _companyDal.Add(company);
            return new SuccessResult(Messages.AddedCompany);
        }

        [ValidationAspect(typeof(CompanyValidator))]
        [TransactionScopeAspect]
        public IResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            _companyDal.Add(companyDto.Company);
            _companyDal.UserCompanyAdd(companyDto.UserId, companyDto.Company.Id);
            return new SuccessResult(Messages.AddedCompany);
        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyDal.Get(c => c.Name == company.Name && c.TaxDepartment == company.TaxDepartment && c.TaxIdNumber == company.TaxIdNumber && c.IdentityNumber == company.IdentityNumber);

            if (result != null)
            {
                return new ErrorResult(Messages.CompanyAlreadyExists);
            }

            return new SuccessResult();
        }

        public IDataResult<Company> GetById(int id)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(p => p.Id == id));
        }

        public IDataResult<UserCompany> GetCompany(int userId)
        {
            return new SuccessDataResult<UserCompany>(_companyDal.GetCompany(userId));
        }

        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetList());
        }

        public IResult Update(Company company)
        {
            _companyDal.Update(company);
            return new SuccessResult(Messages.UpdatedCompany);
        }

        public IResult UserCompanyAdd(int userId, int compnayId)
        {
            _companyDal.UserCompanyAdd(userId, compnayId);
            return new SuccessResult();
        }
    }
}
