# SMSGateway.me Client for API v4
Recently the implementation of the smsgateway.me for sending/receiving SMS's has changed
and broke the library that I was using, so I decided to make my own version

Fork of https://github.com/beejjacobs/smsgateway.me-api-wrapper


## Token
Get your API Token in the [settings page](https://smsgateway.me/dashboard/settings)

## Client initalization
Before starting is necessary to create the client with the token as parameter
```
using SmsGateway.MeApiWrapperCore;
gateway = new SmsGatewayApi('Your API Key here');
```

## Device lookup
Sending messages requires the device id, so its necessary to get it
```
var devices = await gateway.GetDevices();
```
> Its possible to pass a filter object as parameter as [specified on the docs](https://smsgateway.me/sms-api-documentation/devices/searching-android-devices)

## Sending messages
```
var messages = await gateway.SendMessage(_targetDeviceId, _targetPhoneNumber, "Test message");
```

# Support
I decided to implement only the endpoints that I need, but if you need anything else, please make a PR :)

Below what is already implemented and what's not:

## Devices
 - :heavy_check_mark: Get devices information
 - :heavy_check_mark: Search for devices

## Messages
 - :heavy_check_mark: Sending a message
 - :heavy_check_mark: Searching for messages
 - :x: Canceling a message
 - :x: Getting a message information
 
## Contacts
 - :x: Creating a contact
 - :x: Updating an existing contact
 - :x: Adding a phone to an existing contact
 - :x: Remove a phone from an existing contact
 - :x: Getting a contact information
 - :x: Searching for contacts

## Callbacks
 - :x: Creating a callback
 - :x: Updating an existing callback
 - :x: Getting callback information
 - :x: Searching for callbacks

# Tests
See Test folder for more info

# Contributing
PR's are welcome
