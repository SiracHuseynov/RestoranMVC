using Restorann.Core.Models;
using Restorann.Core.RepositoryAbstracts;
using Restorann.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Data.RepositoryConcretes
{
    public class ChefRepository : GenericRepository<Chef>, IChefRepository
    {
        public ChefRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
