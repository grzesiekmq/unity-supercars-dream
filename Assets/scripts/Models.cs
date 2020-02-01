using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Models : MonoBehaviour
{
    public GameObject button;
    public GameObject backButton;

    public RectTransform canvas;
    public GameObject scenes;
    public static string globalModel;

    private readonly object[] cars = new[]
    {
        new
        { Name= "adi", Models=new[]{"a4 dtm","a8","avus","r18","r8 fsi","r8 le mans","r8 lms","rs5","rs7","rsq","tt clubsport" }
        },
        new
        { Name= "akura", Models=new[]{"nzx", "xrx", "NZX GT3"}
        },
        new
        { Name= "alfa", Models=new[] {"4c","8c spider", "zagato tz3 stradale" }
        },
        new
        {
        Name= "aston",
        Models=new[] {
          "DBR9",
          "db10",
          "dbs volante",
          "one 77",
          "rapide",
          "v12 vanquish",
          "vanquish",
          "vantage",
          "vulcan"
        }
        },
        new
      { Name= "bentle", Models=new[] {"continental gt supersports" } },
        new
  {
    Name= "bmv",
    Models=new[] {"335i","7", "8 series concept", "i8", "m3 gt3","m3 GTR", "m3", "m4", "m6","z4" }
  },
        new
  { Name= "bugati", Models=new[] {"chiron", "divo", "veyron", "centodieci" } },
        new
  { Name= "cadilac", Models=new[] {"cien","ct6", "cts" } },
        new
  { Name= "chonda", Models=new[] {"NZX TAKATA DOME", "s2000 tuned" } },
        new
  { Name= "citron", Models=new[] {"citron gt" } },
        new
  {
    Name= "dodg",
    Models=new[] {"viper gts", "viper zrt", "viper", "challenger", "charger" }
  },
        new
  {
    Name= "ferari",
    Models=new[] {
      "430",
      "458 italia",
      "488",
      "599",
      "812 superfast",
      "armanno",
      "enzo",
      "f12",
      "ff",
      "la ferari",
      "modena",
      "ml1",
      "testarosa"
    }
  },
        new
  { Name= "fort", Models=new[] {"gt 90", "fort gt", "mustang salen", "mustang", "mustang gt"} },
        new
  { Name= "henesey", Models=new[] {"venom gt"} },
        new
  {
    Name= "hevrolet",
    Models=new[] {"camaro old", "camaro", "corvete concept", "corvete c7"}
  },
        new
  { Name= "holdem", Models=new[] {"monaro"} },
        new
  { Name= "hrysler", Models=new[] {"300c", "crossfire", "me"} },
        new
  { Name= "jagur", Models=new[] {"c-x75", "f type", "xkr"} },
        new
  { Name= "konigseg", Models=new[] {"agera", "ccx", "one1"} },
        new
  {
    Name= "lambo",
    Models=new[] {
      "asterion",
      "aventador",
      "aventador roadster",
      "centenario",
      "diablo sv",
      "gallardo",
      "huracan",
      "murcielago",
      "reventon",
      "urus",
      "veneno"
    }
  },
        new
  { Name= "lotuz", Models=new[] {"evora", "exige s" } },
        new
  { Name= "luxus", Models=new[] {"gs", "lfa" } },
        new
  { Name= "maklaren", Models=new[] {"576 gt", "f1", "maklaren gt", "mp4", "p1" } },
        new
  { Name= "masda", Models=new[] {"mx5", "mx5 2016", "rx7", "rx8" } },
        new
  { Name= "maybah", Models=new[] {"exelero" } },
        new
  { Name= "mazerati", Models=new[] {"G Turismo", "gran cabrio", "mc12", "quatroporte", "spyder" } },
        new
  {
    Name= "merc",
    Models=new[] {
      "amge gts",
      "concept",
      "cl",
      "clk dtm",
      "clk gtr",
      "slr maklaren",
      "sls amge",
      "sls"
    }
  },
        new
  { Name= "mitsushi", Models=new[] {"3000gt", "eclipse", "lancer evolution x", "lancer wrc" } },
        new
  { Name= "nisan", Models=new[] {"370z", "gtr nizmo", "R35 GTR", "skyline r34 gtr", "skyline gtr" } },
        new
  { Name= "opl", Models=new[] {"speedster" } },
        new
  {
    Name= "other",
    Models=new[] {
      "asterisk",
      "byakko",
      "concept car 2009",
      "concept car 7",
      "concept car 5",
      "exotic car",
      "mega suv",
      "race car",
      "SuperSport",
      "tavaculo",
      "vm x1",
      "wizard gt"
    }
  },
        new
  { Name= "pahani", Models=new[] {"huayra", "zonda r" } },
        new
  { Name= "pegot", Models=new[] {"onyx" } },
        new
  { Name= "pontiak", Models=new[] {"gto" } },
        new
  {
    Name= "porshe",
    Models=new[] {
      "911 turbo",
      "918 r type",
      "962",
      "996",
      "997 gt3",
      "boxster s",
      "boxster",
      "carrera gt",
      "cayman",

      "panamera turbo"
    }
  },
        new
  { Name= "reno", Models=new[] {"alpine a442b" } },
        new
  { Name= "rols-roys", Models=new[] {"phantom" } },
        new
  { Name= "rufe", Models=new[] {"rk coupe","rt 12s" } },
        new
  { Name= "sab", Models=new[] {"aero x" } },
        new
  { Name= "salen", Models=new[] {"s7" } },
        new
  { Name= "shelbi", Models=new[] {"cobra" } },
        new
  { Name= "tezla", Models=new[] {"model s" } },
        new
  { Name= "tojota", Models=new[] {"soarer" } },
        new
  { Name= "twr", Models=new[] {"sagaris" } },
        new
  { Name= "vendeta", Models=new[] {"gtr 800" } },
        new
  { Name= "vw", Models=new[] {"ego" } },
        new
  { Name= "zenwo", Models=new[] {"st1" } },
        new
  { Name= "zubaru", Models=new[] {"b11s", "impreza" } }
};

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var car in cars)
        {
            var makeName = car.GetType()
                   .GetProperty("Name")
                   .GetValue(car);
            // Debug.Log("model names " + modelName);
            if ((string)makeName == Makes.globalMake)
            {
                var models = (string[])car.GetType()
                    .GetProperty("Models")
                    .GetValue(car);
                foreach (string model in models)
                {
                    GameObject goButton = (GameObject)Instantiate(button);

                    goButton.GetComponentInChildren<TextMeshProUGUI>().text = model;
                    var buttonComponent = goButton.GetComponent<Button>();
                    buttonComponent.onClick.AddListener(OnButtonClick);

                    goButton.transform.SetParent(canvas.transform, false);
                }
            }
        }
    }

    private void OnButtonClick()
    {
        var scenesComponent = scenes.GetComponent<Scenes>();

        globalModel = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        scenesComponent.ChangeToCarSelect();

        Debug.Log(globalModel);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}