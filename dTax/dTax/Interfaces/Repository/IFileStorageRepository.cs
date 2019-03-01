using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IFileStorageRepository : IBaseRepository<FileStorage>
    {
        FileStorage GetById(long fileId);
        IEnumerable<FileStorage> GetFilesByIds(IEnumerable<long> fileIds);
        void UpdateEntity(FileStorage entity);
        long InsertFileStorage(FileStorage entity);
        bool IsExists(long fileid);
    }
}
