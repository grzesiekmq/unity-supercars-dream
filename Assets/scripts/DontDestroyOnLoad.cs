using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject car = GameObject.FindObjectOfType<Turn>().gameObject;

        var camera = GameObject.Find("Main Camera");

        camera.transform.parent = car.transform;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}