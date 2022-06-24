using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

public class TestAddOrReplaceNamespace : ScriptableObject {

    [MenuItem("Tools/TestAddNamespace")]
    private static void TestAddNameSpaceHandler() {
        const string sampleCode =
        @"using System;
        using System.Collections;
        using System.Linq;
        using System.Text;
        using UnityEngine;

        public enum SayType {
            A,
            B
        }
            
        [RequireComponent(typeof(Transform))]
        public class HelloWorld : MonoBehaviour {

            private GameObject m_effect;

            private void Start() {
                Debug.Log(""HelloWorld"");
            }
        }";


        SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
        CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();

        NameSyntax name = IdentifierName("Qianyou");
        name = QualifiedName(name, IdentifierName("KingBook"));
        name = QualifiedName(name, IdentifierName("MiniGame"));
        name = name.WithLeadingTrivia(Whitespace(" ")).WithTrailingTrivia(Whitespace(" "));
        NamespaceDeclarationSyntax namespaceDeclaration = NamespaceDeclaration(name);
        namespaceDeclaration = namespaceDeclaration.WithMembers(root.Members);

        CompilationUnitSyntax compilationUnit = CompilationUnit();
        compilationUnit = compilationUnit.WithUsings(root.Usings);
        compilationUnit = compilationUnit.WithExterns(root.Externs);
        compilationUnit = compilationUnit.AddMembers(namespaceDeclaration);
        Debug.Log(compilationUnit.NormalizeWhitespace().ToFullString());
    }


    [MenuItem("Tools/TestReplaceNamespace")]
    private static void TestReplaceNamespaceHandler() {
        const string sampleCode =
         @"using System;
        using System.Collections;
        using System.Linq;
        using System.Text;
        using UnityEngine;
        namespace Helo {
            public enum SayType {
                A,
                B
            }

            [RequireComponent(typeof(Transform))]
            public class HelloWorld : MonoBehaviour {

                private GameObject m_effect;

                private void Start() {
            
                }
            }

            namespace Helo_1 {
                
            }
        }
        namespace Two.Helo1 {
            
        }";


        SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
        CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();

        /*foreach (var member in root.Members) {
            if(member is NamespaceDeclarationSyntax) {
                var ns = (NamespaceDeclarationSyntax)member;
                var nns = ns.WithName(QualifiedName(IdentifierName("MiniGame"), IdentifierName(ns.Name.ToString())));
                root = root.ReplaceNode(ns, nns);
            }
        }*/
        
        var collector = new NamespaceCollector();
        collector.Visit(root);
        foreach (var ns in collector.namespaces) {
            var nns = ns.WithName(QualifiedName(IdentifierName("MiniGame"), IdentifierName(ns.Name.ToString())));
            root = root.ReplaceNode(ns, nns);
        }

        Debug.Log(root.NormalizeWhitespace().ToFullString());
    }
}

