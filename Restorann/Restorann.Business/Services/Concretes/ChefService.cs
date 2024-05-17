using Microsoft.AspNetCore.Hosting;
using Restorann.Business.Exceptions;
using Restorann.Business.Extensions;
using Restorann.Business.Services.Abstracts;
using Restorann.Core.Models;
using Restorann.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Business.Services.Concretes
{
    public class ChefService : IChefService
    {
        private readonly IChefRepository _chefRepository;
        private readonly IWebHostEnvironment _env;

        public ChefService(IChefRepository chefRepository, IWebHostEnvironment env)
        {
            _chefRepository = chefRepository; 
            _env = env;
        }

        public async Task AddAsyncChef(Chef chef)
        { 
            if (chef.ImageFile == null)
                throw new EntityNotFoundException("Chef tapilmadi");

            chef.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\members", chef.ImageFile);

            await _chefRepository.AddAsync(chef);
            await _chefRepository.CommitAsync();

        }

        public void DeleteChef(int id)
        {
            var existChef = _chefRepository.Get(x=> x.Id == id);

            if (existChef == null)
                throw new EntityNotFoundException("Chef tapilmadi");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\members", existChef.ImageUrl);
            _chefRepository.Delete(existChef);
            _chefRepository.Commit();
        }

        public List<Chef> GetAllChefs(Func<Chef, bool>? func = null)
        {
            return _chefRepository.GetAll(func);
        }

        public Chef GetChef(Func<Chef, bool>? func = null)
        {
            return _chefRepository.Get(func);
        }

        public void UpdateChef(int id, Chef newChef)
        {
            var oldChef = _chefRepository.Get(x=> x.Id == id);

            if (oldChef == null)
                throw new EntityNotFoundException("Chef tapilmadi");

            if(newChef.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\members", oldChef.ImageUrl);

                oldChef.ImageUrl= Helper.SaveFile(_env.WebRootPath, @"uploads\members", newChef.ImageFile);
            }

            oldChef.Name = newChef.Name;    
            oldChef.Designation = newChef.Designation;
            oldChef.FbLink = newChef.FbLink;
            oldChef.InstaLink= newChef.InstaLink;
            oldChef.XLink = newChef.XLink;

            _chefRepository.Commit();
        }
    }
}
