 using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.FileStorages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class FileStorageRepository : BaseRepository<FileStorage>, IFileStorageRepository
    {
        DbPostrgreContext CommonDbContext;

        public FileStorageRepository(DbPostrgreContext commonDbContext) : base(commonDbContext)
        {
            CommonDbContext = commonDbContext; ;
        }


        public FileStorage GetById(Guid fileId)
        {
            return GetByIdAsync(fileId).Result;
        }

        private async Task<FileStorage> GetByIdAsync(Guid fileId)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == fileId);
        }

        public IEnumerable<FileStorage> GetFilesByIds(IEnumerable<Guid> fileIds)
        {
            return GetFilesByIdsAsync(fileIds).Result;
        }

        private async Task<IEnumerable<FileStorage>> GetFilesByIdsAsync(IEnumerable<Guid> fileIds)
        {
            return await GetQuery().Where(_ => fileIds.Contains(_.Id)).ToListAsync();
        }

        public void UpdateEntity(FileStorage entity)
        {
            this.Update(entity);
            this.Commit();
        }

        public Guid InsertFileStorage(FileStorage entity)
        {
            this.Insert(entity);
            this.Commit();
            return entity.Id;
        }

        public bool IsExists(Guid fileid)
        {
            
                var file = GetQuery().FirstOrDefault(_ => _.Id == fileid);
                if (file == null)
                    return false;
                return true;
           
        }

    }
}
