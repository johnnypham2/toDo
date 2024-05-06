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

        [HttpPost]

        public async Task<IActionResult> Create(ToDoList toDoList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.AddAsync(toDoList);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

//delete list
     [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDoList = await _context.ToDoLists.FindAsync(id);
            if(toDoList == null)
            {
                return NotFound();
            }

            _context.Remove(toDoList);

            var result = await _context.SaveChangesAsync();

               if (result > 0)
               {
                return Ok("List was removed");
               }
               return BadRequest("unable to delete list");
        }


//Get single to do list
        [HttpGet("{id:int}")]

        public async Task<ActionResult<ToDoList>> GetToDoLists(int id)
        {
            var toDoList = await _context.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound("Not Found");
            }
            return Ok(toDoList);
        }

//update list
      [HttpPut("{id:int}")]

      public async Task <IActionResult> EditToDoList(int id, ToDoList toDoList)
      {
        var toDoListFromDb = await _context.ToDoLists.FindAsync(id);
        if(toDoList == null)
        {
            return BadRequest("Not found");
        }
        toDoListFromDb.Task1 = toDoList.Task1;
        toDoListFromDb.Task2 = toDoList.Task2;
        toDoListFromDb.Task3 = toDoList.Task3;

        var result = await _context.SaveChangesAsync();

        if(result > 0)
        {
            return Ok("Edited");
        }
        return BadRequest("Unable to update data");
      }



    }
}