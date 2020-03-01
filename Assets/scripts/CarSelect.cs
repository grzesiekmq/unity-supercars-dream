using System.Linq;
using TMPro;
using UnityEngine;

public class CarSelect : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject name;
    public GameObject acceleration;
    public GameObject topSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        LoadModels();
    }

    public void LoadModels()
    {
        Cars json = JsonUtility.FromJson<Cars>(jsonFile.text);
        // models of selected make

        foreach (Make car in json.cars.Where(car => car.make == Makes.globalMake))
        {
            foreach (Model model in car.models.Where(model => model.name == Models.globalModel))
            {
                // Debug.Log("Found model: " + model.name + " " + model.acceleration + " " + model.topSpeed + " " + model.modelFile);

                AddInfo(model);

                Object carPrefab = (GameObject)Resources.Load($"prefabs/cars/{model.name}");
                GameObject prefab = (GameObject)Instantiate(carPrefab);

                DontDestroyOnLoad(prefab);
                var carCtrl = GameObject.FindGameObjectWithTag("InGame").GetComponent<CarCtrl>();
                carCtrl.enabled = false;
                prefab.SetActive(true);
                prefab.AddComponent<Turn>();
            }
        }
    }

    private void AddInfo(Model model)
    {
        name.GetComponent<TextMeshProUGUI>().text = model.name;
        acceleration.GetComponent<TextMeshProUGUI>().text = model.acceleration.ToString() + " s";
        topSpeed.GetComponent<TextMeshProUGUI>().text = model.topSpeed.ToString() + " kmh";
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [System.Serializable]
    public class Cars
    {
        public Make[] cars;
    }

    [System.Serializable]
    public class Make
    {
        public string make;
        public Model[] models;
    }

    [System.Serializable]
    public class Model
    {
        public string name;
        public float acceleration;
        public int topSpeed;
        public string modelFile;
    }
}