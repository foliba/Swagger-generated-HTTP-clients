# Introduction

Just a small example .Net WebAPI project which generates http clients (for C# and TypeScript) based on generated swagger documentation. These clients can be used to distribute to API consumers and as shown here in the API level tests directly.

## Workflow
We have an after build target which does:
1) generate the swagger.json
1) generate the C# http client, used by C# based API consumers and in our API level tests
1) generate the typescript client, used by the front-end
1) move the generated C# client to the test project

# RFC3339 model binder
The ISO 8601 date time format doesn't require the timezone to be set and the local time is assumed when none is set (https://en.wikipedia.org/wiki/ISO_8601#Local_time_(unqualified)). I highly recommend to always use the [RFC3339](https://en.wikipedia.org/wiki/ISO_8601#RFCs) when building APIs where local time of the client and server might differ.

Using this model binder takes forces the json serializer to check against RFC3339 when DateTimes are parsed over.

# Newtonsoft vs System.Text.Json
As System.Text.Json still has some issues with inheritance and Newtonsoft just works so flawlessly in this regard, I still prefere good old newtonsoft.json.
We simply add the JsonInheritanceConverter on the parent class which adds the type discriminator automatically to all json objects.

# Publishing the generated HTTP clients
You can easily add a small step in your build pipeline to create and publish nuget/npm packages for the generated clients using the same version as used for the API itself.

# How to use the generated HttpClients
In case of success responses, the data areautomatically serialized into the correct DTOs. You don't have to take care of serializing or re-creating the DTOs by yourself. It's all abstracted by the HttpClient.
In case of non-success respnonses APIExceptions are thrown. Either of type ValidationProblem (in case of serializer validation exceptions) or ProblemDetail (in case of generic errors).