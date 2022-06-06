using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// C# 语法分析：https://docs.microsoft.com/zh-cn/dotnet/csharp/roslyn-sdk/get-started/syntax-analysis

public class SyntaxTreeManualTraversal : MonoBehaviour {

    void Start() {
        const string programText =
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

        SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

        Debug.Log($"The tree is a {root.Kind()} node.");
        Debug.Log($"The tree has {root.Members.Count} elements in it.");
        Debug.Log($"The tree has {root.Usings.Count} using statements. They are:");
        foreach (UsingDirectiveSyntax element in root.Usings) {
            Debug.Log($"\t{element.Name}");
        }

    }


}
