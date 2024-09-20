using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Detvarmestehjul.Services
{
    public class Repo
    {
        public interface IRepository<T> where T : class
        {
            void Add(T entity);
            T Get(int id);
            IEnumerable<T> GetAll();
            void Update(T entity);
            void Delete(int id);
        }
    }
}
