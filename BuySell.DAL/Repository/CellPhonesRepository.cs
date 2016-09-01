using BuySell.DAL.Data;
using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.DAL.Repository
{
    public class CellPhonesRepository : RepositoryBase<CellPhone>
    {
        public CellPhonesRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}