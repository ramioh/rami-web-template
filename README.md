# Starter Web Template
The Starter Web Template is a .NET 6.0 Web API solution template. It uses a service named **Weather Forecast** as an example. 

The template is structured according to layered architecture. The repository is designed as a mono repo, where other sibling solutions could co-exist.

**Disclaimer:** The Starter Web Template is opinionated. And it also evolves as I learn and gain experience. I keep adding features that I see essential for a typical new Web API solution.

## Features
The following is the list of features in the template.

### 1. Mono repo folder structure
The repository is structured to allow for multiple solutions, each in its own folder. The Starter Web Template is in the `weather-forecast` folder under the root level. There is another solution in this repo, named `common-package`. It produces nuget packages that are needed by the Starter Web Template. More on this below.

### 2. Layered architecture
The Weather Forecast solution is composed of a hierarchy of folders and projects that manifests layered architecture. The hierarchy is as follows:
* `0.Build`: Contains general build-related files. Not specific to the architecture.
* `1.Presentation`: Contains projects pertaining to the presentation layer, such as `Api` and `Api.ViewModels`.
* `2.Infrastructure`: Contains projects for data access, either persistence or access to upstream data providers. Both are parts of the infrastructure layer in a typical layered architecture.
    > Presentation and infrastructure are the two tenants of the outermost layer of layered architecture. So, to be precise, each of them is only _half layer_. Being two halves of the same layer does not mean that they could have dependency between each other.

* `3.Application`: Contains projects in the application layer.

* `4.Domain`: Contains projects in the domain layer. This is the innermost layer of the architecture, and the most protected and purest one. 

### 2. Directory.Build.Props
The solution employs a `Directory.Build.Props` file that defines all the common properties for projects. The importance of this approach is that it eliminates code duplication on the individual project level and ensures a uniform configuration. In fact, the props file is put in the repo's root folder, so it is applicable to _all_ solutions in the repo. The following configurations are enforced by the props file.
* .NET 6.0 target framework.
* Enabled nullable reference types.
* Enabled implicit `using`.

There are other features that are also configured in the `Directory.Build.Props` file. Those are listed below as separate items due to their significance.

### 3. Root namespace and assembly name
Root namespace and assembly name are both equal to `Company.Product.WeatherForecast.{ProjectName}`. Replace `Company.Product.WeatherForecast` with your own value in the `Directory.Build.Props` file. All projects will inherit these settings. Consider this behavior when naming your projects, as the name will end up being part of the root namespace and the assembly name.

>### 100. Launch settings and app settings?
>### 101. Swagger?

## Template personalization
When using the Starter Web Template to create a new solution, the following should be replaced with your own values.
1. `Company.Product.WeatherForecast` in `Directory.Build.Props`.
2. The name of the main solution folder `./weather-forecast`
3. The name of the solution file `./weather-forecast/WeatherForecast.sln`


>## Purposefully omitted features
>The following is the list of features that I omitted on purpose. I don't have plans to add them.

>### 1. Separate interface projects in the Application layer
>The presentation layer has a dependency on the Application layer. One could argue that we should have separate projects for the interfaces that are implemented by the application layer, then have the presentation layer depend on those slim interface projects (called adapters in the port-adapter architecture) in order to conceal the actual implementations. 

>I found this to be an overkill. First, the majority of developers have been trained enough to create dependencies on interfaces even when the implementations of these interfaces sit in the same project. Second, the Application layer is often the one with the most projects. Having twice as many of these projects is unnecessary clutter, in my opinion.

>P.S. Re-consider this, because you already do this in data access layer.