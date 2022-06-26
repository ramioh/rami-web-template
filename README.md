# Starter Web Template
The Starter Web Template is a .NET 6.0 Web API solution template. It uses a service named **Weather Forecast** as an example. 

The template is structured according to layered architecture. The repository is designed as a mono repo, where other sibling solutions could co-exist.

**Disclaimer:** The Starter Web Template is opinionated. And it also evolves as I learn and gain experience. I keep adding features that I see essential for a typical new Web API solution.

## Features
The following is the list of features in the template.

### 1. Mono repo folder structure
The repository is structured to allow for multiple solutions, each in its own folder. The Starter Web Template is in the `weather-forecast` folder under the root level. There is another solution in this repo, named `common-packages`. It produces class libraries that are needed by the Starter Web Template. More on this below.

### 2. Layered architecture
The Weather Forecast solution is composed of a hierarchy of folders and projects that manifests layered architecture. The hierarchy is as follows:
* `0.Build`: Contains general build-related files. Not specific to the architecture.
* `1.Presentation`: Contains projects pertaining to the presentation layer, such as `Api` and `Api.ViewModels`.
* `2.Infrastructure`: Contains projects for data access, either persistence or access to upstream data providers Both are parts of the infrastructure layer in a typical layered architecture.
    > Presentation and infrastructure are the two tenants of the outermost layer of layered architecture. So, to be precise, each of them is only _half layer_. Being two halves of the same layer does not mean that they could have a dependency on one another.
* `3.Application`: Contains projects in the application layer.
* `4.Domain`: Contains projects in the domain layer. This is the innermost layer of the architecture, and the most protected and purest one.

### 3. Separate `Contracts` projects
Layered architecture constrains the direction of dependency between projects. It states that dependency should always point inwards. Besides this, the Starter Web Template applies more constraints to dependencies between projects, as described below:
1. Any interface (C# interface) or model (a data-centric C# record, class, or struct, with no behavior) that is to be shared between two projects must be defined in a _Contracts_ project. Contract projects are lightweight projects that have a `.Contracts` suffix in their names. They only contain declaration of behavior together with any required supporting data types (parameters and return values of behavior). They don't contain implementations, nor do they contain domain entities with encapsulated behavior.
2. When a project depends on another in the next inner layer, a Contracts project must be created in the inner layer to define the interfaces and models. The implementation of the defined contracts could either be in the outer or the inner layer, depending on the case. For example, when the Api project has a dependency on the Application layer, both the contract and the implementation projects should be on the application layer side. Another example is when the application layer has a dependency on the infrastructure layer. In this case, the application layer should define the contract, and the infrastructure layer should have the implementation.
3. At any given time, a dependency between any two projects in the solution must be a dependency on a contracts project. This is a hard rule in this template. The only exception to this is the dependency of the `Api` project on the `Api.ViewModels` project. While view models are contracts in a sense, the ViewModels name is used instead because it provides more clarity. 

### 4. Directory.Build.Props
The solution employs a `Directory.Build.Props` file that defines all the common properties for projects. The importance of this approach is that it eliminates code duplication on the individual project level and ensures a uniform configuration. In fact, the props file is put in the repo's root folder, which means that it is applicable to _all_ solutions in the repo. The following configurations are enforced by the props file.
* .NET 6.0 target framework.
* Enabled nullable reference types.
* Enabled implicit `using`.

There are other features that are also configured in the `Directory.Build.Props` file. Those are listed below as separate items due to their significance.

### 5. Root namespace and assembly name
Root namespace and assembly name are both equal to `Company.Product.{SolutionName}.{ProjectName}`. Replace `Company.Product` with your own value in the `Directory.Build.Props` file. All projects will inherit these settings. Consider this behavior when naming your solutions and projects, as the names will end up being part of the root namespace and the assembly name.

>### 100. Launch settings and app settings?
>### 101. Swagger?

## Template personalization
When using the Starter Web Template to create a new solution, the following should be done before it is ready for use.
1. Replace the value for `Company.Product` in `Directory.Build.Props`.
2. Rename the main solution folder `./weather-forecast`.
3. Rename the solution file `./weather-forecast/WeatherForecast.sln`.
4. All namespace `using` statements that start with `Company.Product.Common` and `Company.Product.WeatherForecast` in both solutions should be replaced.
5. Publish the `DependencyInjection` project as a nuget package, and replace the path-based references to it with its nuget package reference. The use of a path-based reference is only for simplicity and should not be used in production.


>## Purposefully omitted features
>The following is the list of features that I omitted on purpose. I don't have plans to add them.

>### 1. Separate interface projects in the Application layer
>The presentation layer has a dependency on the Application layer. One could argue that we should have separate projects for the interfaces that are implemented by the application layer, then have the presentation layer depend on those slim interface projects (called adapters in the port-adapter architecture) in order to conceal the actual implementations. 

>I found this to be an overkill. First, the majority of developers have been trained enough to create dependencies on interfaces even when the implementations of these interfaces sit in the same project. Second, the Application layer is often the one with the most projects. Having twice as many of these projects is unnecessary clutter, in my opinion.

>P.S. Re-consider this, because you already do this in data access layer.