using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectArchive_APi.Models;
using ProjectArchive_APi.Models.DBModels;

namespace ProjectArchive_APi.Controllers
{
    [Route("archive")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ArchiveDBContext _context;


        public ProjectsController(ArchiveDBContext context)
        {
            _context = context;
        }


        // GET: archive/projects
        // Fetch all projects and related data
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {


            return await _context.Project
                                 .Include(p => p.ProjectDepartmentRelation)
                                      .ThenInclude(p => p.Department)
                                            .ThenInclude(p => p.AssetsDepartmentRelation)
                                                .ThenInclude(p => p.Asset)
                                 .ToListAsync();
        }


        // GET: archive/projects/1
        // Fetch spacific project with its id with related date
        [HttpGet("create/{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Project
                                        .Include(p => p.ProjectDepartmentRelation)
                                            .ThenInclude(p=> p.Department)
                                                .ThenInclude(p => p.AssetsDepartmentRelation)
                                                    .ThenInclude(p => p.Asset)
                                        .SingleOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }


        // POST: archive/replicate
        // Adding new project to the data
        [HttpPost("replicate")]
        public async Task<ActionResult<Project>> PostProject(ProjectUseModel project)
        {

            //form new project from UseModel one to add in right form in database 

            var newproject = new Project()
            {
                Name = project.Name
            };

            _context.Project.Add(newproject);

            await _context.SaveChangesAsync();

            var projectId = _context.Project.Where(p => p.Name == newproject.Name).ToList()[0].Id;

            ////configure the relationship
            var department_project_relation = new ProjectDepartmentRelation()
{
                ProjectId = projectId,
                DepartmentId = _context.Departments.Where(d => d.Name == project.Department).ToList()[0].Id
            };

            _context.ProjectDepartmentRelation.Add(department_project_relation);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = projectId }, newproject);
        }   
    }
}
