using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportButton : MonoBehaviour
{
    Camera cam;
    public Animator transition;

    Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    IEnumerator showTransition()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        (Globals.playerCoordsY, Globals.playerCoordsX)  = ShowConnections.roomMap[(Globals.playerCoordsY, Globals.playerCoordsX)];

        currentCoroutine = null;
        yield return new WaitForSeconds(1f);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void checkClick()
    {
        var mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hit, 100f) && hit.transform == transform && currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(showTransition());
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkClick();

        
    }
}
