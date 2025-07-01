using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppCqrsMediator.Domain.Entities;
using WebAppCqrsMediator.Domain.MessageQueues;
using WebAppCqrsMediator.Infrastructure.Data;
using WebAppCqrsMediator.Mediator.Common.Exceptions;
using WebAppCqrsMediator.Mediator.Nodes.Commands.CreateNode;
using WebAppCqrsMediator.Mediator.Nodes.Commands.DeleteNode;
using WebAppCqrsMediator.Mediator.Nodes.Commands.UpdateNode;
using WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodeById;
using WebAppCqrsMediator.Mediator.Nodes.Queries.GetNodes;

namespace WebAppCqrsMediator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private ISender _mediator = null;
        private readonly IRabitMQProducer _rabitMQProducer;
        private readonly NodeDbContext _context;

        public NodeController(IRabitMQProducer rabitMQProducer, NodeDbContext context)
        {
            _rabitMQProducer = rabitMQProducer;
            _context = context;
        }

        public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        // GET: api/Node
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Node>>> GetNodes()
        {
            try
            {                
                var nodes = await Mediator.Send(new GetNodesQuery());
                return Ok(nodes);                
            }
            catch (SecureException exc)
            {
                RunSpWithParams(exc);
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }
        }

        // GET: api/Node/{B486F904-557C-4830-B720-27C065E76A86}
        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> GetNode(Guid id)
        {            
            try
            {
                var node = await Mediator.Send(new GetNodeByIdQuery() { NodeId = id });
                if (node == null)
                {
                    return NotFound();
                }
                return Ok(node);
            }
            catch (SecureException exc)
            {
                int rowUpdate = RunSpWithParams(exc);
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }            
        }


        // POST: api/Node
        [HttpPost]
        public async Task<ActionResult<Node>> CreateNode(CreateNodeCommand createNodeCommand)
        {
            try
            {
                var nodeCreated = await Mediator.Send(createNodeCommand);
                _rabitMQProducer.SendMessage(nodeCreated);
                return CreatedAtAction("CreateNode", new { id = nodeCreated.Id }, nodeCreated);
            }
            catch (SecureException exc)
            {
                int rowUpdate = RunSpWithParams(exc);
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }            
        }

        // PUT: api/Node/{B486F904-557C-4830-B720-27C065E76A86}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNode(Guid id, UpdateNodeCommand updateNodeCommand)
        {
            try
            {
                if (id != updateNodeCommand.Id)
                {
                    return BadRequest();
                }
                var idUpdate = await Mediator.Send(updateNodeCommand);

                return NoContent();
            }
            catch (SecureException exc)
            {
                int rowUpdate = RunSpWithParams(exc);
                return StatusCode(500, new { type = exc.GetType().Name, message = exc.Message });
            }
        }

        // DELETE: api/Node/{B486F904-557C-4830-B720-27C065E76A86}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            try
            {
                var idDelete = await Mediator.Send(new DeleteNodeCommand { Id = id });
                if (idDelete == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (SecureException exc)
            {
                int rowUpdate = RunSpWithParams(exc);
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
