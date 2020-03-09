using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    public static List<string> cars = new List<string>();
    private GameObject prefab;

    // Start is called before the first frame update
    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        var sceneName = "Garage";
        var compareScenes = string.Compare(currentScene, sceneName);
        if (compareScenes == 0)
        {
            LoadCar();
        }
    }

    private void LoadCar()
    {
        var carsLength = cars.Count;
        for (var x = 0; x < carsLength; x++)
        {
            var car = cars[x];
            prefab = Resources.Load($"prefabs/cars/{car}") as GameObject;

            var posOffset = new Vector3(x * 2f, 0, 0);
            var goCar = Instantiate(prefab, posOffset, Quaternion.identity);

            var carCtrl = goCar.GetComponent<CarCtrl>();
            carCtrl.enabled = false;
            var carRb = goCar.GetComponent<Rigidbody>();
            carRb.useGravity = false;
            var cam = GameObject.Find("Main Camera");
            
            cam.transform.LookAt(goCar.transform);

            Debug.Log("cars in garage: " + car);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}