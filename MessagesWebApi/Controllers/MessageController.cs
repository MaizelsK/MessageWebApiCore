using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MessagesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public MessageController(MessageDbContext context,
            IEmailService emailService,
            ISmsService smsService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
        }

        // GET api/message
        [HttpGet]
        public async Task<IActionResult> AllMessages()
        {
            var messages = await _context.Messages.ToListAsync();

            return Ok(messages);
        }

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail([FromBody]EmailMessageDto emailDto)
        {
            if (emailDto == null)
                return BadRequest();

            await _emailService.SendEmail(emailDto.To, emailDto.Subject, emailDto.Text);

            _context.Messages.Add(new Model.Message
            {
                To = emailDto.To,
                MessageText = emailDto.Text,
                Subject = emailDto.Subject,
                SendDate = DateTime.Now,
                Type = Model.MessageType.Email
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("sendsms")]
        public async Task<IActionResult> SendSms([FromBody]SmsMessageDto smsDto)
        {
            if (smsDto == null)
                return BadRequest();

            await _smsService.SendSms(smsDto.To, smsDto.Text);

            _context.Messages.Add(new Model.Message
            {
                To = smsDto.To,
                MessageText = smsDto.Text,
                SendDate = DateTime.Now,
                Type = Model.MessageType.Sms
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}