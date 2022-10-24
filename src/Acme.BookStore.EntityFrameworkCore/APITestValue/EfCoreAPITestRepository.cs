using Acme.BookStore.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.APITestValue
{
    public class EfCoreAPITestRepository : EfCoreRepository<BookStoreDbContext, APITest, Guid>,
            IAPITestRepository
    {
        public EfCoreAPITestRepository(
            IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //public Task<APITest> InsetAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
