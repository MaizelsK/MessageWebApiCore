using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

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

        [HttpGet("messages")]
        public async Task<IActionResult> AllMessages()
        {
            return await Task.Run(() =>
            {
                var messages = _context.Messages;

                return Ok(messages);
            });
        }

        [HttpPost(Name = "sendemail")]
        public async Task<IActionResult> SendEmail(EmailMessageDto emailDto)
        {
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

        [HttpPost(Name = "sendsms")]
        public async Task<IActionResult> SendSms(SmsMessageDto smsDto)
        {
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