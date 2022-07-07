>### 100. Launch settings and app settings?
>### 101. Swagger?

>## Purposefully omitted features
>The following is the list of features that I omitted on purpose. I don't have plans to add them.

>### 1. Separate interface projects in the Application layer
>The presentation layer has a dependency on the Application layer. One could argue that we should have separate projects for the interfaces that are implemented by the application layer, then have the presentation layer depend on those slim interface projects (called adapters in the port-adapter architecture) in order to conceal the actual implementations. 

>I found this to be an overkill. First, the majority of developers have been trained enough to create dependencies on interfaces even when the implementations of these interfaces sit in the same project. Second, the Application layer is often the one with the most projects. Having twice as many of these projects is unnecessary clutter, in my opinion.

>P.S. Re-consider this, because you already do this in data access layer.

Code analysis
global usings

