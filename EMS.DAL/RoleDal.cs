using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Common;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
namespace EMS.DAL;

public class RoleDal : IRoleDal
{
    private readonly string _connectionString;
    private readonly RoleDbContext _context;

    public RoleDal(string connectionString, RoleDbContext context)
    {
        _connectionString = connectionString;
        _context = context;
    }

    public List<Role> GetAll()
    {
        // List<Role> roles = new List<Role>();
        // SqlConnection connection = new SqlConnection(_connectionString);
        // try
        // {
        //     using (connection)
        //     {
        //         connection.Open();
        //         string sqlSelect = @"select * from Role;";
        //         using (SqlCommand command = new SqlCommand(sqlSelect, connection))
        //         {
        //             using (SqlDataReader reader = command.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     Role role = new Role();
        //                     role.Id = reader.GetInt32(0);
        //                     role.Name = reader.GetString(1);
        //                     role.DepartmentId = reader.GetInt32(2);
        //                     roles.Add(role);
        //                 }
        //             }
        //         }
        //     }
        // }
        // catch (Exception e)
        // {
        //     throw;
        // }
        // finally
        // {
        //     connection.Close();
        // }
        // return roles;
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
            return role.Id ?? -1;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
}