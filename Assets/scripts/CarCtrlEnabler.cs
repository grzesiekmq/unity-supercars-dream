using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCtrlEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject car = GameObject.FindGameObjectWithTag("InGame");
        var sceneName = SceneManager.GetActiveScene().name;
        var testDrive = "TestDrive";
        if (string.Compare(sceneName, testDrive) == 0)
        {
            var carCtrl = GameObject.FindGameObjectWithTag("InGame").GetComponent<CarCtrl>();
            carCtrl.enabled = true;
        }
        else { return; }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}