using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelloWorld : MonoBehaviour {

    private void Start() {
        SceneManager.LoadScene("Scenes/Main");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Scenes/Title");
    }
}
