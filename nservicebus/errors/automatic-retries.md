---
title: Automatic Retries
summary: With retries, the message causing the exception is instantly retried configured number of times before forwarding to the error queue.
tags:
- Second Level Retry
- Error Handling
- Exceptions
- Automatic retries
redirects:
 - nservicebus/second-level-retries
related:
- samples/faulttolerance
---

Sometimes processing of a message can fail. This could be due to a transient problem like a deadlock in the database, in which case retrying the message a few times might overcome this problem. Or, if the problem is more protracted, like a third party web service going down or a database being unavailable, where it might be useful to wait a little longer before retrying the message again.

For situations like these, NServiceBus offers two levels of retries:

- First Level Retry(FLR) is for the transient errors where quick successive retries could solve the problem.
- Second Level Retry(SLR) is when a small delay is needed between retries.

NOTE: When a message cannot be deserialized, it will bypass all retry mechanisms both the FLR and SLR and the message will be moved directly to the error queue.


## First Level Retries

NServiceBus automatically retries the message when an exception is thrown during message processing up to five successive times by default. This value can be configured through `app.config` or code.

Note: The configured value describes the minimum number of times a message will be retried. Especially in environments with competing consumers on the same queue there is an increased chance of retrying a failing message more often across the endpoints.


### Configuring FLR using app.config

In NServiceBus version 3, this configuration was available via `MsmqTransportConfig`.

From version 4 onward the configuration for this mechanism is implemented in the `TransportConfig` section. For more details on `MsmqTransportConfig` and `TransportConfig` [read this article](/nservicebus/msmq/transportconfig.md).

<!-- import configureFlrViaXml -->


### Configuring FLR through IProvideConfiguration

<!-- import FlrProvideConfiguration -->


### Configuring FLR through ConfigurationSource

<!-- import FLRConfigurationSource -->

<!-- import FLRConfigurationSourceUsage -->


## Second Level Retries

SLR introduces another level of retrying mechanism for messages that fail processing. When using SLR, the message that causes the exception is, as before, instantly retried, but instead of being sent to the error queue, it is sent to a retries queue.

SLR then picks up the message and defers it, by default first for 10 seconds, then 20, and lastly for 30 seconds, then returns it to the original worker queue.

For example, if there is a call to an web service in your handler, but the service goes down for five seconds just at that time. Without SLR, the message is retried instantly and sent to the error queue. With SLR, the message is instantly retried, deferred for 10 seconds, and then retried again. This way, when the Web Service is available the message is processed just fine.

SLR can be configured either via code or through `app.config`.


### Configuring SLR using app.config

To configure SLR, enable its configuration section:

<!-- import SecondLevelRetiesAppConfig -->

 *  `Enabled`: Turns the feature on and off. Default: true.
 *  `TimeIncrease`: A time span after which the time between retries increases. Default: 10 seconds (`00:00:10`).
 *  `NumberOfRetries`: Number of times SLR kicks in. Default: 3.


### Configuration SLR through IProvideConfiguration

<!-- import SlrProvideConfiguration -->


### Configuring SLR through ConfigurationSource

<!-- import SlrConfigurationSource -->

<!-- import SLRConfigurationSourceUsage -->


### Disabling SLR through code

To completely disable SLR through code:

<!-- import DisableSlrWithCode -->


### Custom Retry Policy

You can change the time between retries or the number of retries in code.

Here is a sample method for handling this behavior.

<!-- import SecondLevelRetriesCustomPolicyHandler -->

To plug this into NServiceBus use the following APIs.

<!-- import SecondLevelRetriesCustomPolicy -->
