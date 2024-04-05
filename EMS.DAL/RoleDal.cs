using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DB;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
namespace EMS.DAL;

public class RoleDal : IRoleDal
{
    private readonly RahulContext _context;

    public RoleDal(RahulContext context)
    {
        _context = context;
    }

    public List<Role> GetAll()
    {
        try
        {
            var data = _context.Roles.ToList();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public int Insert(Role role)
    {
        try
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.Id;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
}