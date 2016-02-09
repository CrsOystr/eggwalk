using UnityEngine;
using System.Collections;
using System.Xml;

/*
 * This is a class intended to make the process of extracting data from PlayerPrefs easier.
 * 
 * KEY FORMATS:
 * ------------
 * 
 * Delivery Scores are stored as ints:
 * key: "[levelname]_eggsdelivered"
 * value: [number of eggs delivered]
 * example: "CitySmall_eggsdelivered" --> 12
 * 
 * Time Scores (index starts at 1, with 1 being the best score):
 * key: "[levelname]_timescore_[index]"
 * value: [time in seconds as float]
 * example: "CitySmall_timescore_1" --> 35.03939f
 *
 * Succesful egg deliveries are stored as ints, with 0 for false, a.k.a. not yet delivered succesfully, and 1 for delivered succesfully)
 * key: "[index]_[eggname]"
 * value: [success]
 * example: "1_Common Egg" --> 1
 */
using System.Collections.Generic;

public class PlayerPrefsManager : MonoBehaviour
{

    // RIGHT NOW this might not be generic enough, maybe eventually create a method that looks like addScore(string levelname, string scoreType, float newScore) ?

    [SerializeField] private TextAsset _eggDataXML;

    public const int SUCCESSFUL = 1;
    public const int UNSUCCESSFUL = 0;

    private List<EggData> _allEggsInGame;

    public List<EggData> AllEggsInGame
    {
        get
        {
            if (_allEggsInGame == null) _allEggsInGame = loadAllEggData();
            return _allEggsInGame;
        }
    }

    public void addTimeScore(string levelname, float newTime)
    {

        int numOfScores = 10;

        float[] timeScores = getTimeScores(levelname, numOfScores);
        float[] newTimeScores = new float[timeScores.Length];

        bool scoreSet = false;
        float tempScore;
        for (int i = 0; i < numOfScores; i++)
        {
            if (!scoreSet)
            {
                if (newTime < timeScores[i] || timeScores[i] <= 0f)
                {
                    tempScore = timeScores[i];
                    newTimeScores[i] = newTime;
                    scoreSet = true;
                }
                else
                {
                    newTimeScores[i] = timeScores[i];
                }
            }
            else if (i + 1 <= newTimeScores.Length - 1) newTimeScores[i + 1] = timeScores[i]; // shift time scores down
        }

        setTimeScores(levelname, newTimeScores);
    }

    public float[] getTimeScores(string levelname, int numOfScores)
    {

        float[] timeScores = new float[numOfScores];

        // return the value if it exists, else return zero
        for (int i = 0; i < numOfScores; i++)
        {
            if (PlayerPrefs.HasKey(levelname + "_timescore_" + (i + 1).ToString()))
                timeScores[i] = PlayerPrefs.GetFloat(levelname + "_timescore_" + (i + 1).ToString());
            else
                timeScores[i] = 0f;
        }

        return timeScores;
    }

    private void setTimeScores(string levelname, float[] timeScores)
    {
        for (int i = 0; i < timeScores.Length; i++)
        {
            string key = levelname + "_timescore_" + (i + 1).ToString();
            PlayerPrefs.SetFloat(key, timeScores[i]);
        }
    }

    public void recordSetEggDelivery(int eggIndex, string eggName, int success)
    {
        PlayerPrefs.SetInt(eggIndex.ToString() + "_" + eggName, success);
    }

    public Object LoadRandomEgg()
    {
        int r = Random.Range(0, AllEggsInGame.Count);
        Debug.Log(r + ", " + AllEggsInGame[r].name);
        Object egg = Resources.Load(AllEggsInGame[r].name);
        if (egg == null) Debug.Log("egg resource is null!");
        return egg;
    }

    private List<EggData> loadAllEggData()
    {

        List<EggData> eggList = new List<EggData>();

        //TODO: Load data from XML file
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_eggDataXML.text);
        XmlNodeList indexList = xmlDoc.GetElementsByTagName("Index");
        XmlNodeList nameList = xmlDoc.GetElementsByTagName("Name");

        // load names and indexes
        for (int i = 0; i < indexList.Count; i++)
        {
            EggData newData = new EggData();
            newData.name = nameList[i].FirstChild.Value;
            newData.index = int.Parse(indexList[0].FirstChild.Value);

            eggList.Add(newData);
        }

        // load whether or not the egg has been succesfully delivered
        for (int i = 0; i < eggList.Count; i++)
        {
            bool hasBeenDelivered = false;
            string key = eggList[i].index.ToString() + "_" + eggList[i].name;
            if (PlayerPrefs.HasKey(key))
            {
                int intBool = PlayerPrefs.GetInt(key);
                if (intBool == 1) hasBeenDelivered = true;
                else if (intBool == 0) hasBeenDelivered = false;
                else
                {
                    Debug.LogError("Error parsing egg data: 'hasBeenSuccesfullyDelivered' not stored as 1 or 0");
                }
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
            }

            eggList[i].hasBeenSuccesfullyDelivered = hasBeenDelivered;
        }

        return eggList;

    }

    public class EggData
    {
        public int index;
        public string name;
        public bool hasBeenSuccesfullyDelivered;
    }

}
