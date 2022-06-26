using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 替换命名空间引用
/// </summary>
public class TestReplaceNamespaceReference : ScriptableObject {

    [MenuItem("Tools/TestReplaceNamespace")]
    private static void TestReplaceNamespaceHandler() {
        const string sampleCode =
        @"using System;
        using System.Collections;
        using System.Linq;
        using System.Text;
        using UnityEngine;
        using UnityEngine.SceneManagement;

        public class HelloWorld : MonoBehaviour {

            private void Start() {
                SceneManager.LoadScene(""Scenes/Main"");
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(""Scenes/Title"");
            }
        }";


        SyntaxTree tree = CSharpSyntaxTree.ParseText(sampleCode);
        CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();

    }
}
