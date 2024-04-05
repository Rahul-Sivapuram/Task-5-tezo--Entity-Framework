using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DB;
namespace EMS.DAL;

public interface IRoleDal
{
    List<Role> GetAll();
    int Insert(Role role);
}
