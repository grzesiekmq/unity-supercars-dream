using Siccity.GLTFUtility;
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
        Cars json = JsonUtility.FromJson<Cars>(jsonFile.text);
        // models of selected make

        foreach (Make car in json.cars.Where(car => car.make == Makes.globalMake))
        {
            foreach (Model model in car.models.Where(model => model.name == Models.globalModel))
            {
                Debug.Log("Found model: " + model.name + " " + model.acceleration + " " + model.topSpeed + " " + model.modelFile);

                name.GetComponent<TextMeshProUGUI>().text = model.name;
                acceleration.GetComponent<TextMeshProUGUI>().text = model.acceleration.ToString() + " s";
                topSpeed.GetComponent<TextMeshProUGUI>().text = model.topSpeed.ToString() + " kmh";

                ImportGLTF("Assets/cars/" + Makes.globalMake + "/" + model.modelFile);
            }
        }
    }

    private void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
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