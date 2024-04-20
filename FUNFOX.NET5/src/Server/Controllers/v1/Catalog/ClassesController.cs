using FUNFOX.NET5.Application.Features.Classes.Commands.AddEdit;
using FUNFOX.NET5.Application.Features.Classes.Commands.Delete;
using FUNFOX.NET5.Application.Features.Classes.Queries.Export;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged;
using FUNFOX.NET5.Application.Features.Classes.Queries.GetProductImage;
using FUNFOX.NET5.Application.Features.Products.Commands.Delete_Enrollment;
using FUNFOX.NET5.Application.Features.Products.Commands.EnrollUser;
using FUNFOX.NET5.Application.Features.Products.Queries.EnrolledUsersExport;
using FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers;
using FUNFOX.NET5.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Server.Controllers.v1.Catalog
{
    public class ClassesController : BaseApiController<ClassesController>
    {
        /// <summary>
        /// Get All Classes
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Classes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var products = await _mediator.Send(new GetAllClassesQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(products);
        }


        /// <summary>
        /// Get All Enrolled Users
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
         [Authorize(Policy = Permissions.Classes.EnrolledUser)]
        [HttpGet("Enrolled-Users")]
        public async Task<IActionResult> GetAllEnrolledUsers(int pageNumber, int pageSize, string searchString,  int classid, string orderBy = null)
        {
            var enrolledusers = await _mediator.Send(new GetEnrolledUsersQuery(pageNumber, pageSize, searchString,  classid,orderBy));
            return Ok(enrolledusers);
        }

        /// <summary>
        /// Get a Product Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Classes.View)]
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetProductImageAsync(int id)
        {
            var result = await _mediator.Send(new GetProductImageQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Add/Edit a Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Classes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditClassCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        ///// <summary>
        ///// Enroll a User in a Class
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.BasicUserPermissions.EnrollInClasses)]
        [HttpPost("enroll-user")]
        public async Task<IActionResult> UserEnroll(EnrollUserCommand command)
        {
           return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Classes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteClassCommand { Id = id }));
        }


        [Authorize(Policy = Permissions.Classes.Delete)]
        [HttpDelete("delete-enrollment/{classid}/{userid}")]
        public async Task<IActionResult> DeleteEnrollment(int classid ,string userid)
        {
            return Ok(await _mediator.Send(new DeleteEnrollmentCommand(userid,classid)));
        }

        /// <summary>
        /// Search Classes and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Classes.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportClassesQuery(searchString)));
        }
        /// <summary>
        /// Search Enrolled Users  and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Classes.EnrolledUser)]
        [HttpGet("export-enrolledusers")]
        public async Task<IActionResult> EnrolledUsersExport(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportEnrolledUsersQuery(searchString)));
        }
    }
}