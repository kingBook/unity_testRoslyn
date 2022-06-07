using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

public class SyntaxTransformation : MonoBehaviour {



    private void Start() {
        NameSyntax name = IdentifierName("System");
        name = QualifiedName(name, IdentifierName("Collections"));
        name = QualifiedName(name, IdentifierName("Generic"));
        Debug.Log(name.ToString());


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

        var oldUsing = root.Usings[1];
        var newUsing = oldUsing.WithName(name);

        root = root.ReplaceNode(oldUsing, newUsing);
        Debug.Log(root.ToString()); // 成功的将 using System.Collections 替换为 using System.Collections.Generic
    }
}
