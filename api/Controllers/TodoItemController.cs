using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        // add private variable to hold our content
        private readonly AppDBContext _context;
        // now we will create a constructor that will pass in our context and save it to our variable _context
        public TodoItemController(AppDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        // This first Http request is to get the list of todoitems
        public async Task<IEnumerable<ToDoItem>> getToDoItem()
        {
            var toDoItems = await _context.ToDoItems.AsNoTracking().ToListAsync();
            return toDoItems;
        }

        [HttpPost]
        // this first http request will create a todo item
        public async Task<IActionResult> Create(ToDoItem toDoItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.AddAsync(toDoItem);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        // this http request will delete a todo item
        public async Task<IActionResult> Delete(int id)
        {
            // make variable to hold the data from our AppDBContext 
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            // Check if empty and give notfound
            if(toDoItem == null)
            {
                return NotFound();
            }
            // remove the item toDoItem based on id from parameter
            _context.Remove(toDoItem);
            // save the result of the changes
            var result = await _context.SaveChangesAsync();
            // if the result ocurred give okay response
            if (result > 0)
            {
                return Ok("Item was deleted");
            }
            // if result > 0 failed return badrequest & custom response
            return BadRequest("Unable to delete todo item");


        }

        // ended with delete  still need to code single todoitem get request
        // still need PUT or update request
    }
}