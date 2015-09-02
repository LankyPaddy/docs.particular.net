using NServiceBus;
using NServiceBus.MessageMutator;

#region mutate-outgoing-messages
public class MutateOutgoingMessages : IMutateOutgoingMessages
{
    IBus bus;

    public MutateOutgoingMessages(IBus bus)
    {
        this.bus = bus;
    }
    public void MutateOutgoing(MutateOutgoingMessagesContext context)
    {
        IMessageContext incomingContext = bus.CurrentMessageContext;
        if (incomingContext != null)
        {
            string incomingMessageId = incomingContext.Headers["NServiceBus.MessageId"];
        }

        context.SetHeader("MutateOutgoingMessages", "ValueMutateOutgoingMessages");
    }

}
#endregion