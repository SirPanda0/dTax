using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models;
using dTax.Entity.Models.FileContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class FileContentRepository : BaseRepository<FileContent>, IFileContentRepository
    {
        public FileContentRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
