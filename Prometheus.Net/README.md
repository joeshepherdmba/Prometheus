
Example Usage
~~~~~~~~~~~~~~~~~~~`

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Prometheus.Net.Generics;

namespace test
{
    public class MyRepository<T>: GenericRepository<T> where T : class
    {
		public MyRepository(DbContext dataContext)
        :base(dataContext)
		{
			
		}
    }
}
