using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DataContext _context;

        public TaskController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("update/db")]
        public async Task<ActionResult> UpdateDatabase()
        {
            try
            {
                var lastTask = _context.SystemTasks.LastOrDefault();
                string taskId = "0";
                if (lastTask != null)
                {
                    taskId = (lastTask.Id).ToString();
                }
                switch (taskId)
                {
                    case "0":
                        {
                        }
                        break;
                    case "1":
                        {
                        }
                        break;
                }
            } catch(Exception ex)
            {

            }
            return Ok();
        }
    }
}