﻿namespace Store.Sales
{
    using System;
    using System.Diagnostics;
    using Common;
    using Messages.Commands;
    using Messages.Events;
    using NServiceBus;

    public class SubmitOrderHandler : IHandleMessages<SubmitOrder>
    {
        IBus bus;

        public SubmitOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(SubmitOrder message)
        {
            if (DebugFlagMutator.Debug)
            {
                Debugger.Break();
            }

            Console.WriteLine("We have received an order #{0} for [{1}] products(s).", message.OrderNumber,
                              string.Join(", ", message.ProductIds));

            Console.WriteLine("The credit card values will be encrypted when looking at the messages in the queues");
            Console.WriteLine("CreditCard Number is {0}", message.EncryptedCreditCardNumber);
            Console.WriteLine("CreditCard Expiration Date is {0}", message.EncryptedExpirationDate);

            //tell the client that we received the order
            bus.Publish<OrderPlaced>(o =>
                {
                    o.ClientId = message.ClientId;
                    o.OrderNumber = message.OrderNumber;
                    o.ProductIds = message.ProductIds;
                });
        }
    }
}