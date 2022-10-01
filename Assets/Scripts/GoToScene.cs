using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void goToScene(string sceneToGoTo)
    {
        print($"loading {sceneToGoTo}");
        SceneManager.LoadScene(sceneToGoTo);
    }
}
