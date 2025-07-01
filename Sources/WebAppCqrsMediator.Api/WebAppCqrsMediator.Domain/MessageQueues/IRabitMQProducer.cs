namespace WebAppCqrsMediator.Domain.MessageQueues
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}
