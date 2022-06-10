using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingCollector : CSharpSyntaxWalker {


    private List<UsingDirectiveSyntax> m_usings = new List<UsingDirectiveSyntax>();

    public ICollection<UsingDirectiveSyntax> usings => m_usings;

    public override void VisitUsingDirective(UsingDirectiveSyntax node) {
        Debug.Log($"\tVisitUsingDirective called with {node.Name}.");
        if (node.Name.ToString() != "System" &&
            !node.Name.ToString().StartsWith("System.")) {
            Debug.Log($"\t\tSuccess. Adding {node.Name}.");
            m_usings.Add(node);
        }
    }


}
