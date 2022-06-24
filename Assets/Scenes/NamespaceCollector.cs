using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

public class NamespaceCollector : CSharpSyntaxWalker {

    private List<NamespaceDeclarationSyntax> m_namespaces = new List<NamespaceDeclarationSyntax>();

    public ICollection<NamespaceDeclarationSyntax> namespaces => m_namespaces;

    public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node) {
        m_namespaces.Add(node);
    }
}
