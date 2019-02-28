using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
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
            return await GetQuery().FirstOrDefaultAsync(_ => _.FileId == fileId);
        }

        public IEnumerable<FileStorage> GetFilesByIds(IEnumerable<Guid> fileIds)
        {
            return GetFilesByIdsAsync(fileIds).Result;
        }

        private async Task<IEnumerable<FileStorage>> GetFilesByIdsAsync(IEnumerable<Guid> fileIds)
        {
            return await GetQuery().Where(_ => fileIds.Contains(_.FileId)).ToListAsync();
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
            return entity.FileId;
        }
    }
}
