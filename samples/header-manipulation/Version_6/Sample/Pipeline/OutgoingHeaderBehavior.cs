using System;
using NServiceBus;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;
using NServiceBus.TransportDispatch;

#region outgoing-header-behavior

class OutgoingHeaderBehavior : Behavior<OutgoingContext>
{
    IBus bus;

    public OutgoingHeaderBehavior(IBus bus)
    {
        this.bus = bus;
    }

    public override void Invoke(OutgoingContext context, Action next)
    {
        TransportMessage incomingMessage;
        if (context.TryGetIncomingPhysicalMessage(out incomingMessage))
        {
            string incomingMessageId = incomingMessage.Headers["NServiceBus.MessageId"];
        }

        context.SetHeader("OutgoingHeaderBehavior", "ValueOutgoingHeaderBehavior");
        next();
    }
}

#endregion