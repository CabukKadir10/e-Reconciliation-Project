using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetList(Expression<Func<T, bool>> filter = null);//Burda olurda getall fonksiyonundan sonra bir linq sorgusu yapmak istediğimizde buna izin veriyoruz. filtreleme olmasa da sorun olmaz manasında filter null yapıyoruz.
        T Get(Expression<Func<T, bool>> filter); //buda yukardaki gibi sorgu yapmak istersek buna yol açmış oluyoruz. ama bu null olamaz
        //T Get(int id); sadece ıd ye göre getirmesini istersek bunu kullanabiliriz. biz her ihtimale karşı sadece ıdye bağlı kalmayalım diye üstteki fonksiyonu kullansak daha iyidir.
    }
}
