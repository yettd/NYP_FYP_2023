using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private Transform entrycontainer;
    [SerializeField] private Transform entrytemplate;
    private List<Transform> highscoreEntryTransformList;

    public SceneChanger sceneChanger;

    // IRFAN
    const string  PLAYERPREFDATABASE = "highscoreTable";
    private void Awake()
    {
        //Turn off the template
        entrytemplate.gameObject.SetActive(false);

        //Get the data
        string jsonString = PlayerPrefs.GetString(PLAYERPREFDATABASE);
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        //no nd check if there is no data
        if (CheckIfPlayerPrefSet())
        {
            //sort entry based on score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        //swap
                        HighscoreEntry temp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = temp;

                    }
                }
            }
            
            //Cycle tru that list
            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighScoreEntryTransform(highscoreEntry, entrycontainer, highscoreEntryTransformList);
            }
        }
        else
        {
            Debug.Log("dont have highscoretable");
        }
    }

    private bool CheckIfPlayerPrefSet()
    {
        if (PlayerPrefs.HasKey(PLAYERPREFDATABASE))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Recieve a highscoreEntry object
    //transform for the container
    //list of instianted transform object
    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        //Making the template height to 30f
        float templateHeight = 30f;

        //Instantiate the template into the container
        Transform entryTransform = Instantiate(entrytemplate, container);

        //Reposition the anchoredPosition, goes down based on templateHeight and the amount of entry in the list currently
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        //Set the entry into true
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;

        //Setting the rank for the leaderboard, 1st 2nd 3rd have special text thingy, others can have their basic ass th
        switch (rank)
        {
            default:
                rankString = rank + "th"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        //Abit hard coded since it is base on Find so sorry bout that...
        //Set the rank text
        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        //Set the score text
        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();

        //Set the name text
        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;

        //Add highscoreEntry to our transformList
        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        string jsonString;
        HighScores highscores;

        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        if (CheckIfPlayerPrefSet())//Check if there is data in playerpref
        {
            //Load saved Highscores
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<HighScores>(jsonString);

            //check if name already exist in database
            if (CheckNameInDatabase(name, highscores))
            {
                UpdateScore(name, score, highscores);
            }
            else
            {
                //Add new entry to Highscores
                highscores.highscoreEntryList.Add(highscoreEntry);
            }

            //Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();

        }
        else //if there is no data in playerpref
        {
            Debug.Log("trying to add without player pref being set");
            List<HighscoreEntry> highscoreEntrylist = new List<HighscoreEntry>();//make a new list of HighscoreEntry
            HighscoreEntry tempHighscoreEntry = new HighscoreEntry { score = score, name = name };// make a new HighscoreEntry
            highscoreEntrylist.Add(tempHighscoreEntry);//add the new HighscoreEntry into the list


            HighScores tempHighscores = new HighScores { highscoreEntryList = highscoreEntrylist };// Make a new HighScores and store the list
            string json = JsonUtility.ToJson(tempHighscores);// convert the list into jason
            PlayerPrefs.SetString("highscoreTable", json);// save it to playerpref
            PlayerPrefs.Save();// save that shit

        }
    }

    private bool CheckNameInDatabase(string name, HighScores highscores)
    {
        //check if name already exist
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            if (highscores.highscoreEntryList[i].name == name)
            {
                Debug.Log("name exist in the database");

                return true;
            }
        }
        Debug.Log("no name exist in the database");
        return false;

    }

    private void UpdateScore(string name, int score, HighScores highscores)
    {
        
        string findName = name;
        int newScore = score;
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            if (highscores.highscoreEntryList[i].name == findName)
            {
                highscores.highscoreEntryList[i].score = newScore;

            }
        }

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Represent a single High score entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    public void OnHomeClicked()
    {
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        sceneChanger.ChangeToMainScene();
    }
}
