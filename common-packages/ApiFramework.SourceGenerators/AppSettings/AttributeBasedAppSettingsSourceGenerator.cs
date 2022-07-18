using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Company.Product.Common.ApiFramework.SourceGenerators.AppSettings
{
    [Generator]
    public class AttributeBasedAppSettingsSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AttributeBasedAppSettingsSyntaxReceiver());
        }
        
        public void Execute(GeneratorExecutionContext context)
        {
            SpinWait.SpinUntil(() => Debugger.IsAttached);
            if (!(context.SyntaxReceiver is AttributeBasedAppSettingsSyntaxReceiver receiver))
            {
                return;
            }
            
            
            
            
            context.AddSource("myGeneratedFile.cs", SourceText.From(@"
using Microsoft.Extensions.DependencyInjection;

namespace GeneratedNamespace
{
    public class GeneratedClass
    {
        public static void GeneratedMethod(IServiceCollection services)
        {
            services.AddOptions();
            // generated code
        }
    }
}", Encoding.UTF8));
        
            
        }

       
    }
}