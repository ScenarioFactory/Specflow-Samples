namespace AutoWorkshop.Specs.Screenplay.ServiceBus.Tasks
{
    using System.Threading.Tasks;
    using Abilities;
    using Azure.Messaging.ServiceBus;
    using Extensions;
    using Pattern;

    public class SendCommand : ITask
    {
        private readonly string _queue;
        private readonly object _command;

        private SendCommand(string queue, object command)
        {
            _queue = queue;
            _command = command;
        }

        public static SendCommand To(string queue, object command)
        {
            return new SendCommand(queue, command);
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
