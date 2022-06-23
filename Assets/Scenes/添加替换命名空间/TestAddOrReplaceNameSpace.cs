using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddOrReplaceNameSpace : MonoBehaviour {

    private void Start() {
        const string sampleCode =
       @"using System;
        using System.Collections;
        using System.Linq;
        using System.Text;

        namespace HelloWorld
        {
            class Program
            {
                static void Main(string[] args)
                {
                    Console.WriteLine(""Hello, World!"");
                }
            }
        }";

        SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
        var root = (CompilationUnitSyntax)tree.GetRoot();


        var childNodes = root.ChildNodes();
        foreach (var childNode in childNodes) {
            Debug.Log(childNode);
        }

        var identifierName = SyntaxFactory.IdentifierName("King").WithLeadingTrivia(SyntaxFactory.Whitespace(" ")).WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
        NamespaceDeclarationSyntax namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("King"));
        foreach (var m in root.Members) {
            namespaceDeclaration = namespaceDeclaration.AddMembers(m);
        }
        Debug.Log(namespaceDeclaration);

    }
}

