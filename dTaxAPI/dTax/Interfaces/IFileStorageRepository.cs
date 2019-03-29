using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IFileStorageRepository : IBaseRepository<FileStorage>
    {
        FileStorage GetById(Guid fileId);
        IEnumerable<FileStorage> GetFilesByIds(IEnumerable<Guid> fileIds);
        void UpdateEntity(FileStorage entity);
        Guid InsertFileStorage(FileStorage entity);
        bool IsExists(Guid fileid);
    }
}
