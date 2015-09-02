using NServiceBus;
using NServiceBus.MessageMutator;

#region message-mutator
public class MessageMutator : IMutateIncomingMessages, IMutateOutgoingMessages
{
    IBus bus;

    public MessageMutator(IBus bus)
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

        context.SetHeader("MessageMutater_Outgoing", "ValueMessageMutater_Outgoing");
    }

    public object MutateIncoming(object message)
    {
        bus.CurrentMessageContext
            .Headers
            .Add("MessageMutator_Incoming", "ValueMessageMutator_Incoming");
        return message;
    }

}
#endregion