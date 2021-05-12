namespace AutoWorkshop.Specs.Screenplay.ServiceBus.Tasks
{
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using Extensions;
    using Pattern;

    public class SendCommand : ITask
    {
        private readonly object _command;
        private string _queue;

        private SendCommand(object command)
        {
            _command = command;
        }

        public static SendCommand Of(object command)
        {
            return new SendCommand(command);
        }

        public SendCommand To(string queue)
        {
            _queue = queue;
            return this;
        }

        public void PerformAs(IActor actor)
        {
            SendAsync(actor.Using<UseServiceBus>().ConnectionString, _queue, _command).Wait();
        }

        private async Task SendAsync(string connectionString, string queue, object command)
        {
            await using var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender(queue);
            var message = new ServiceBusMessage(command.ToJson());
            await sender.SendMessageAsync(message);
        }
    }
}
