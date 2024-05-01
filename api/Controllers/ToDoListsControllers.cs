using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListsControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        public ToDoListsControllers(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
       public async Task<IEnumerable<ToDoList>> GetToDoLists()
       {
        var ToDoLists = await _context.ToDoLists.AsNoTracking().ToListAsync();
        return ToDoLists;
       }


    }
}