namespace AutoWorkshop.Specs.Services
{
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using Extensions;

    public class ServiceBus
    {
        private readonly AppSettings _appSettings;

        public ServiceBus(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SendAsync<T>(string queue, T command)
        {
            await using var client = new ServiceBusClient(_appSettings.ServiceBusConnectionString);
            var sender = client.CreateSender(queue);
            var message = new ServiceBusMessage(command.ToJson());
            await sender.SendMessageAsync(message);
        }
    }
}
