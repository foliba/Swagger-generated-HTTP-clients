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

# API level tests vs unit tests
I was involved in multiple projects where we didn't write any unit test, but API level tests only. Reasoning behind thi sdecission was:
- Individual unit tests don't tell me whether the end product consumed by the client (the API) works as expected. Might still be that some unit in combination won't work.
- With the automatically generated HTTP clients the API level tests are super cheap and easy to write.
- As the complete stack is worked through when doing HTTTP calls to your API in test, you instantly get a extremely high test coverage for relaticvely little test code to be written.
- You have to write *significantly* less tests, which reduces developement overhead and speads up test execution by a lot.

## Additional tests
In addition to the fast and always run API level tests, I sugegst to run integration tests and e2e test in combination with your front-end and dependencies in your CI/CD pipeline as well.

# DB mocks
On one project our first approach was to spin up our MSSQL in docker containers using the TestFactory and Fixtures for each TestClass. This works pretty damn well and we could use the same migration scripts (by entity framework) as for the production databases. Which gave us huge confidence in our automated tests. But after a growing test code base, these databases in docker turned out to be a massive performance bottlekneck. So we decided to move away from docker based MSSQL databases spin up in the TestFactory and use in memory collections. This way the tests ran through in seconds instead of minutes.