using Microsoft.Extensions.Options;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Common;
using Project.Infrastructure.Configurations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace Project.Application.Services
{
    public class SMSService : ISMSService
    {
        private readonly SmsServiceOptions options;

        public SMSService(IOptions<SmsServiceOptions>  options)
        {
            this.options = options.Value;
            TwilioClient.Init(options.Value.AccountSid, options.Value.AuthToken);
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber))
            {
                From = new PhoneNumber(options.FromNumber),
                Body = message
            };

            var msg = await MessageResource.CreateAsync(messageOptions);
        }
    }
}
