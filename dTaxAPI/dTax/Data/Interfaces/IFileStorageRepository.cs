
using dTax.Entity.Models.FileStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IFileStorageRepository : IBaseRepository<FileStorageEntity>
    {
        FileStorageEntity GetById(Guid fileId);
        IEnumerable<FileStorageEntity> GetFilesByIds(IEnumerable<Guid> fileIds);
        void UpdateEntity(FileStorageEntity entity);
        Guid InsertFileStorage(FileStorageEntity entity);
        bool IsExists(Guid fileid);
    }
}
