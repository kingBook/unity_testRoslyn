using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
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

        var collector = new NamespaceCollector();
        collector.Visit(root);

        root = root.ReplaceNodes(collector.namespaces, (a, b) => {
            // 改为 MiniGame.xx.xxx
            a = a.WithName(QualifiedName(IdentifierName("MiniGame"), IdentifierName(a.Name.ToString())));
            return a;
        });
        Debug.Log(root.NormalizeWhitespace().ToFullString());
    }
}

