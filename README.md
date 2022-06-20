# Starter Web Template
The Starter Web Template is a .NET 6.0 Web API solution template. It is structured according to layered architecture. It is a live template where I keep adding features that I see essential for any new Web API solution.

**Disclaimer:** The Starter Web Template is opinionated. And it also evolves as I learn and gain experience. 

## Features
The following is the list of features in the template.

### 1. Layered architecture
The solution is composed of a hierarchy of directories and projects that manifests layered architecture. The hierarchy is as follows:
* `0.Build`: Contains general build-related files. Not specific to the architecture.
* `1.Presentation`: Contains projects pertaining to the presentation layer, such as `Api` and `Api.ViewModels`.
* `2.Infrastructure`: Contains projects for data access, either persistence or access to upstream data providers. Both are parts of the infrastructure layer in a typical layered architecture.
Presentation and infrastructure are the two tenants of the outermost layer of layered architecture. So, to be precise, each of them is only _half layer_.
* `3.Application`: Contains projects in the application layer.
* `4.Domain`: Contains projects in the domain layer. This is the innermost layer of the architecture, and the most protected and purest one. 

### 2. Directory.Build.Props
The solution employs a `Directory.Build.Props` file that defines all the common properties for projects. The importance of this approach is that it eliminates code duplication in the individual project files and ensures a uniform configuration. The following configurations are enforced across all projects, through a `Directory.Build.Props` file.
* .NET 6.0 target framework.
* Enabled nullable reference types.
* Enabled implicit `using`.

There are other features that are also configured in the `Directory.Build.Props` file. Those are listed below as separate headings due to their significance.

### 3. Root namespace and assembly name
Root namespace and assembly name are both equal to `Company.Product.Service.{ProjectName}`. Replace `Company.Product.Service` with your own value in the `Directory.Build.Props` file. All projects will inherit these settings. Consider this behavior when naming your projects, as the name will end up being part of the root namespace and the assembly name.