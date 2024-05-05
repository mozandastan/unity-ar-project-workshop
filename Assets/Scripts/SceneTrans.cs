using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrans : MonoBehaviour
{
    public void doSceneTransition(string param)
    {
        SceneManager.LoadScene(param);
    }
}
