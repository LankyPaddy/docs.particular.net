---
title: Injecting the Bus into ASP.NET MVC Controller
summary: Leverages Autofac MVC integration to inject IBus into MVC Controllers.
tags: []
redirects:
- nservicebus/injecting-the-bus-into-asp.net-mvc-controller
related:
- nservicebus/containers
- nservicebus/hosting
---

### Packages

Aside from MVC and NServiceBus this solution also leverages both Autofac and Autofac.Mvc packages.

### Wire up Autofac

Open `Global.asax.cs` and look at the `ApplicationStart` method.

<!-- import ApplicationStart -->

### Injection into the Controller

Not that `IBus` is injected into the `DefaultController` at construction time.

<!-- import Controller -->
