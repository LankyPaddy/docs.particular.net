---
title: Automatic Retries
summary: With SLR, the message causing the exception is instantly retried via a retries queue instead of an error queue.
tags:
- Second Level Retry 
- Error Handling
- Exceptions
- Automatic retries
related:
- nservicebus/errors
---

When you run the sample, you should start them using Ctrl+F5 (start without debugging), press the letter "S" in both windows at the same time and watch the different outputs. 

Both endpoints execute the same code.

<!-- import handler -->  

## The output

### Without SLR 

```
ReplyToAddress: Samples.ErrorHandling.WithoutSLR@RETINA MessageId:91cc7d3b-b763-4e01-9a3b-a42f0014f233
ReplyToAddress: Samples.ErrorHandling.WithoutSLR@RETINA MessageId:91cc7d3b-b763-4e01-9a3b-a42f0014f233
ReplyToAddress: Samples.ErrorHandling.WithoutSLR@RETINA MessageId:91cc7d3b-b763-4e01-9a3b-a42f0014f233
ReplyToAddress: Samples.ErrorHandling.WithoutSLR@RETINA MessageId:91cc7d3b-b763-4e01-9a3b-a42f0014f233
ReplyToAddress: Samples.ErrorHandling.WithoutSLR@RETINA MessageId:91cc7d3b-b763-4e01-9a3b-a42f0014f233
2015-01-29 01:16:18.480 ERROR NServiceBus.Faults.Forwarder.FaultManager Message with '91cc7d3b-b763-4e01-9a3b-a42f0014f33' id has failed FLR and will be moved to the configured error queue.
```

### With SLR

```
2015-01-29 01:13:57.517 WARN  NServiceBus.Faults.Forwarder.FaultManager Message with '24ea8afe-7610-41a0-b201-a42f00143fb4' id has failed FLR and will be handed over to SLR for retry attempt 2.
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
This is second level retry number 2
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
2015-01-29 01:14:18.537 WARN  NServiceBus.Faults.Forwarder.FaultManager Message with '24ea8afe-7610-41a0-b201-a42f00143fb4' id has failed FLR and will be handed over to SLR for retry attempt 3.
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
This is second level retry number 3
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
ReplyToAddress: Samples.ErrorHandling.WithSLR@RETINA MessageId:24ea8afe-7610-41a0-b201-a42f00143fb4
2015-01-29 01:14:49.573 ERROR NServiceBus.Faults.Forwarder.FaultManager SLR has failed to resolve the issue with message 24ea8afe-7610-41a0-b201-a42f00143fb4 and will be forwarded to the error queue at error@RETINA
```

