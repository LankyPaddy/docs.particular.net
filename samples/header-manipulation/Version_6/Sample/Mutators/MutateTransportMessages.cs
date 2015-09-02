using NServiceBus;
using NServiceBus.MessageMutator;

#region mutate-transport-messages

public class MutateTransportMessages : IMutateOutgoingTransportMessages, IMutateIncomingTransportMessages
{
    public void MutateIncoming(TransportMessage transportMessage)
    {
        transportMessage.Headers
            .Add("MutateTransportMessages_Incoming", "ValueMutateTransportMessages_Incoming");
    }
    
    public void MutateOutgoing(MutateOutgoingTransportMessagesContext context)
    {
        context.SetHeader("MutateTransportMessages_Outgoing", "ValueMutateTransportMessages_Outgoing");
    }

}

#endregion