using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models;

namespace MovieStore.DataAccess.Repository;
public interface ICustomRepository<T>: IRepository<T> where T: class
{
    T Get(Expression<Func<T, bool>> filter);
    List<T> Filter(Expression<Func<T, bool>> filter);    
    void Update(T obj);
}