using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAccessImportService
    {

        /// <summary>
        /// Imports data from an Access database file.
        /// </summary>
        /// <param name="accessFilePath">The path to the Access database file.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Import(string accessFilePath);
    }
}
