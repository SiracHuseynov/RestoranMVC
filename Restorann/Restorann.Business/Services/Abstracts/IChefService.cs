using Restorann.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Business.Services.Abstracts
{
    public interface IChefService 
    {
        Task AddAsyncChef(Chef chef);
        void DeleteChef(int id);
        void UpdateChef(int id, Chef newChef);
        Chef GetChef(Func<Chef, bool>? func = null);
        List<Chef> GetAllChefs(Func<Chef, bool>? func = null);

    }

}
