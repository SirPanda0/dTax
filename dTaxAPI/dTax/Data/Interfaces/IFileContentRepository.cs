using dTax.Entity.Models;
using dTax.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models.FileContents;

namespace dTax.Data.Interfaces
{
    public interface IFileContentRepository : IBaseRepository<FileContentEntity>
    {
    }
}
