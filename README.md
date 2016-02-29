consul-api [![Build status](https://ci.appveyor.com/api/projects/status/b6l1hfiknqjxogrf?svg=true)](https://ci.appveyor.com/project/danesparza/consul-api)
==========

Consul-API is a .NET client for the highly availble service discovery and configuration system, [Consul](https://www.consul.io/).  It aims to be an easy-to-use client. Need [Consul installation instructions](https://www.consul.io/intro/getting-started/install.html)?

### Quick Start

Install the [NuGet package](https://www.nuget.org/packages/Consul-api/) from the package manager console:

```powershell
Install-Package Consul-api
```
Next, you will need to provide consul-api with your Consul server endpoint in code. 

In your application, call:

```CSharp
// Pass the Consul server location on the constructor:
ConsulManager client = new ConsulManager("http://your-consul-server.com:8500");

// Next, make any API call you'd like:
ConfigItem configItem = client.GetConfigItem(configKey);
```

### Examples

##### Getting a single configuration item:

```CSharp
using Consul;
using Consul.Config;

// Create a consul client and get the config value for a specified key
ConsulManager client = new ConsulManager("http://your-consul-server.com:8500");
ConfigItem configItem = client.GetConfigItem("testing/testconfigitem");

// If we can find the value ... 
if(configItem != null)
{
    // Print out the config value...
    Console.WriteLine("The value for key '{0}' is '{1}' (The raw base64 value is: {2})", configItem.Key, configItem.Value, configItem.Base64Value);
}

```

