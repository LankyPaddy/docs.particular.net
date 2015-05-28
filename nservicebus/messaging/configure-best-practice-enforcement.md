---
title: Configuring enforcement of best practices
summary: How to enable/disable the enforcement of messaging best practices based on Events and Commands
tags: 
- Conventions
- Message Semantics
- Command
- Event
---

By default NServiceBus will make sure that you are following [messaging best practices](messages-events-commands.md) for the messages you define as either Commands or Events.  While this worked it caused other features like [auto subscribe](publish-subscribe/how-to-pub-sub.md) to stop working since only `Events` are auto subscribed.

You can bypass these enforcements by defining all your messages as plain messages.

BETA: In version 6 you can now override the default behavior

You can now turn this feature on and off on the endpoint level using:

<!-- import DisableBestPracticeEnforcementPerEndpoint -->

or at the message level using:

<!-- import DisableBestPracticeEnforcementPerMessage -->

