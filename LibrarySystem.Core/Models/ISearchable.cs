using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Models

{
    public interface ISearchable
    {

        bool Matches(string searchTerm);
    }
}
