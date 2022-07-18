using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Company.Product.Common.ApiFramework.SourceGenerators.AppSettings
{
    internal class AttributeBasedAppSettingsSyntaxReceiver : ISyntaxReceiver
    {
        public List<AttributeSyntax> AttributeDeclarations { get; } = new List<AttributeSyntax>();
        
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is AttributeSyntax attributeSyntax)
            {
                AttributeDeclarations.Add(attributeSyntax);
            }
        }
    }
}
