using System;
using System.Linq;
using System.Threading.Tasks;
using MessageApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;


namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService _messageService;

        public MessageController(
            ILogger<MessageController> logger,
            IMessageService messageService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        // GET: api/Message
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var all = await _messageService.SelectAll();
                return Ok(all.Select(x=> new MessageDto { Id = x.Id.ToString(), MessageContent = x.MessageContent }));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get messages", ex);
                throw;
            }
        }

        // GET: api/Message/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                Guid idGuid;

                if (string.IsNullOrEmpty(id)
                    || !Guid.TryParse(id, out idGuid))
                {
                    return BadRequest();
                }

                var item = await _messageService.SelectById(idGuid);

                if(item == null)
                {
                    return NotFound();
                }

                return Ok(new MessageDto{ Id = item.Id.ToString(), MessageContent = item.MessageContent });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add message for {id}.", ex);
                throw;
            }
        }

        // POST: api/Message
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageDto message)
        {
            try
            {
                if(message == null 
                    || string.IsNullOrEmpty(message.MessageContent))
                {
                    return BadRequest();
                }
                
                var item = await _messageService.AddMessage(message.MessageContent);

                return CreatedAtAction(nameof(GetById), new MessageDto { Id = item.Id.ToString() }, item);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to add message for {message.MessageContent}.", ex);
                throw;
            }
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
