---
title: Install ServicePulse in IIS
summary: Describes how to manually install ServicePulse in IIS.
tags:
- ServicePulse
- Security
- IIS
---


## Prerequisites

These instructions assume the following:

* ServiceControl has installed and is  listening on `http://localhost:33333/api`
* ServicePulse has been installed


## Basic Setup

[ServicePulse](introduction-and-installing-servicepulse.md), by default, is installed as a Windows Service that will self-host the ServicePulse web application.

It is possible to manually install ServicePulse using IIS following these steps:

* Extract ServicePulse files using, at a command prompt, the following command:

```
ServicePulse.Host.exe --extract --serviceControlUrl="http://localhost:33333/api" --outPath="C:\ServicePulse"
```

Note: `ServicePulse.Host.exe` can be found in the ServicePulse installation directory, whose default is `%programfiles(x86)%\Particular Software\ServicePulse`

Once all the ServicePulse files are successfully extracted you can configure a new IIS web site whose physical path points to the location where files have been extracted.

When using IIS to host ServicePulse the ServicePulse.Host service is not used.  To remove the service uninstall ServicePulse from Add/Remove programs.


## Advanced Configuration

ServicePulse relies on the ServiceControl REST API.  It is possible to add a reverse proxy to the web site using  the Microsoft [Application Request Routing](http://www.iis.net/downloads/microsoft/application-request-routing) IIS extension.
This is useful if you which to lock down access to ServicePulse or if wish to expose the web site over a single port.

Installation Steps:

1. Install the IIS [Application Request Routing](http://www.iis.net/downloads/microsoft/application-request-routing) extension.
- Go to the root folder for the Web site you created in the basic configuration
- Create a new subdirectory called `api`
- Edit `config.js` and change the `serviceControlUrl` value from `http://localhost:33333/api` to `/api`
- Open the IIS management tool
- Select the api sub folder from within IIS management tool
- Click the `URL Rewrite`
- Add a new URL Rewrite Rule
- Choose `Reverse Proxy` from the list of rule templates
- Enter `localhost:33333/api` into the inbound field and leave SSL offload enabled then click OK to add the rule.
- The website should now answer on /api as though you were directly accessing the ServiceControl.  

The procedure above should result in a web.config file in the newly created /api folder similar to this:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
    <system.webServer>
        <rewrite>
            <rules>
                <rule name="ReverseProxyInboundRule1" stopProcessing="true">
                    <match url="(.*)" />
                    <action type="Rewrite" url="http://localhost:33333/api/{R:1}" />
                </rule>
            </rules>
        </rewrite>
    </system.webServer>
</configuration>

```

WARNING: The default configuration for ServiceControl allows access to REST API via localhost only. By exposing the REST API via the reverse proxy configuration this protection is no longer in place. To address this it is recommended that the IIS Web site be configured with one of the IIS authentication providers such as Windows integration authentication.
It is also recommended that the IIS web site be configured to use SSL if an authorization provider is used.


### Limitations

If ServiceControl is secured with an authentication module other that Windows Authentication  ServiceInsight will not be able to connect to the REST API exposed via IIS. ServiceInsight V1.4 or greater will is required to use Windows authentication.

Older version of ServiceInsight can still be used locally, bypassing the security by connecting to the ServiceControl port directly using the `http://localhost:33333/api` URL.  

## Upgrades

When ServicePulse is hosted in IIS the upgrade process is as follows:

1. Go to the root directory of the IIS web site,
- View the contents of `config.js` and record the current value of `serviceControlUrl`
- Remove all files and folders in the root of the IIS Web Site except the `api` folder if it exists
- Install the new version of ServicePulse using the standard instructions
- Extract the files from the ServicePulse.Host.exe using the following commandline, replacing `<rescordedvalue>` with the value from the `config.js` and `<webroot>` with the path to the root directory of the IIS website
```
ServicePulse.Host.exe` --extract --serviceControlUrl="<rescordedvalue>" --outPath="<webroot>"
```
- Optionally remove the unneeded Windows Service by uninstalling ServicePulse via the Add/Remove applet in control panel
