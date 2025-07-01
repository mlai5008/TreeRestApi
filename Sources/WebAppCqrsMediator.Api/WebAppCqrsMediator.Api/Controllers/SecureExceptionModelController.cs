using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Infrastructure.Data;
using WebAppCqrsMediator.Mediator.Common.Exceptions;
using WebAppCqrsMediator.Mediator.SecureExceptionModels.Queries.GetSecureExceptionModels;

namespace WebAppCqrsMediator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecureExceptionModelController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly NodeDbContext _context;

        public SecureExceptionModelController(ISender mediator, NodeDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        // GET: api/SecureExceptionModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecureExceptionModel>>> GetSecureExceptionModels()
        {
            try
            {
                var nodes = await _mediator.Send(new GetSecureExceptionModelsQuery());
                return Ok(nodes);
                               
            }
            catch (SecureException exc)
            {
                RunSpWithParams(exc);                
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }
        }

        // GET: api/SecureExceptionModel/Exception
        [HttpGet]
        [Route("Exception")]
        public async Task<ActionResult<SecureExceptionModel>> GetSecureException()
        {
            try
            {
                await Task.Delay(50);
                throw new SecureException();
            }
            catch (SecureException exc)
            {
                RunSpWithParams(exc);
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }
        }        

        private int RunSpWithParams(Exception exc)
        {
            var idOutParam = new SqlParameter
            {
                ParameterName = "Id",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Output
            };
            int numberOfRowsAffected = _context.Database.ExecuteSqlInterpolated($@"
EXEC SpInsertSecureExceptionModel 
@Name = {exc.GetType().Name}, 
@Message = {exc.Message},
@Type = {exc.GetType().Name},
@Source = {exc.StackTrace ?? string.Empty},
@Url = {Request?.GetDisplayUrl()},
 
@Id = {idOutParam} OUTPUT"
);
            return numberOfRowsAffected;
        }
    }
}
