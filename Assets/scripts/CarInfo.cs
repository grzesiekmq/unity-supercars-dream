using System;
using System.Linq;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    [SerializeField]
    private TextAsset jsonFile;

    public static int topSpeedValue;
    public static float acceleration;

    [SerializeField]
    private GameObject car;

    // Start is called before the first frame update
    private void Start()
    {
        LoadInfo();
    }

    /// <summary>
    /// loads info
    /// </summary>
    private void LoadInfo()
    {
        Cars json = JsonUtility.FromJson<Cars>(jsonFile.text);

        // models

        foreach (Model model in json.models.Where(model => model.name == car.name))
        {
            //  Debug.Log("Found model: " + model.name + " " + model.acceleration + " " + model.topSpeed);

            topSpeedValue = model.topSpeed;

            
        }
    }

    // inner classes

    [Serializable]
    public class Cars
    {
        public Model[] models;
    }

    [Serializable]
    public class Model
    {
        public string name;
        public float acceleration;
        public int topSpeed;
    }
}