---
title: Accessing business data with NHibernate Persistence
summary: How to access business data in sync with message consumption and modifications to NServiceBus-controlled data.
tags:
 - NHibernate
 - Data
 - Saga
---

Most of the time message handlers are meant to modify the internal state of you application. This usually boils down to accessing some kind of a data store. Since in NServiceBus messages are durable (unless transactions are explicitly disabled in very rare cases), there are two possible synchronization options between consuming messages and accessing the data:
 * have an illusion of *exactly-once* message processing either through a distributed transaction or the [outbox](/nservicebus/outbox/)
 * implement data access code in an *idempotent* way

The second is much simpler in theory but much harder in practice. In theory all you need to do is create your own connection in the handler and execute the *idempotent* data access code. The practice shows that making that code idempotent is a non-trivial task. That is why NServiceBus offers APIs that allow you to work on top of *exactly-once* processing illusion. We'll focus on these APIs.

## Accessing data in the handler

The simplest way to access the data in an *exactly-once* way is to just lean on the Distributed Transaction Coordinator (DTC) to make sure all the data access, happening while handling the message, is atomic. This approach has two downsides. First, the throughput when using DTC is much smaller than without it. Second, DTC is not a trivial service to configure, not mentioning making it HA via a cluster. There is one upside though. For the application data you can use any data store that supports the DTC-coordinated transaction.

NServiceBus persistence APIs offer a solution to these problems but limits the data storage choices. The NHibernate persistence allows you too 'hook-up' to the data context used by NServiceBus to ensure atomic changes.

<!-- import NHibernateAccessingDataViaContext -->

As shown above, you can use the `NHibernateStorageContext` directly but we recommend other approach, shown in the listing below

<!-- import NHibernateAccessingDataDirectlyConfig -->

<!-- import NHibernateAccessingDataDirectly -->

The first part tell NServiceBus to inject the `ISession` instance into the handlers. This way your handlers are less coupled NServiceBus APIs and won't need to change should the API change in future. This `ISession` object is fully managed by NServiceBus according to the best practices defined by NServiceBus documentation with regards to transactions.

## Customizing the session

If you need some special behavior in the `ISession` object managed by NServiceBus, you can hook-up to the session creation process by providing your own delegate.

<!-- import CustomSessionCreation -->

## Known limitations

Because of the way NServiceBus opens sessions by passing an existing instance of a database connection, it is  currently impossible to use NHibernate's second-level cache. Such behavior of NServiceBus is caused by still-unresolved [bug](https://nhibernate.jira.com/browse/NH-3023) in NHibernate. 