using CommandsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CommandsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;

        //initialize the db context class ---i.e CommandContext.cs in the Models folder

        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        //fetch all commands
        //<IEnumerable<Command>>  Command is the name of the model class
        //GET  api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands() 
        {
            return _context.CommandItems; //CommandItems is the name of the table, implied in CommandContext.cs     
        }

        //fetch single command
        //GET  api/commands/id
        [HttpGet("{id}")]

        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem == null)
            {
                return NotFound();
            }

            return commandItem;
        }

        //create a new command
        //POST api/commands
        [HttpPost]
        public ActionResult<Command> AddCommand(Command command)
        {
            //command is the request body
            _context.CommandItems.Add(command);
            _context.SaveChanges();
            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }

        //update command
        //PUT  api/commands
        [HttpPut("{id}")]

        public ActionResult UpdateCommand(int id, Command command)
        {
            //check if id is valid
            if(id != command.Id)
            {
                return BadRequest();
            }

            //update record using entity framework
            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        //delete command
        [HttpDelete("{id}")]

        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;

        }

    }
}



