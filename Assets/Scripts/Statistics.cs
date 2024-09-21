using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public GameObject pref;
    public Transform content;
    private RectTransform rect;
    public void UpdateInfo()
    {

        foreach (Transform obj in content)
        {
            Destroy(obj.gameObject);
        }

        int leftNumber = 0;
        int number = PlayerPrefs.GetInt("Games");
        if (number > 50)
            leftNumber = number - 50;
        for (int i = number; i >= leftNumber; i--)
        {
            Transform obj = Instantiate(pref, content).transform;
            obj.GetChild(0).GetComponent<TMP_Text>().text = "Game " + (i + 1);
            
            if(PlayerPrefs.HasKey("Game" + i + "Lvl"))
            obj.GetChild(0).GetComponent<TMP_Text>().text = "Lvl " + (PlayerPrefs.GetInt("Game" + i + "Lvl") + 1);


            obj.GetChild(1).GetComponent<SwapItems>().StateItems(PlayerPrefs.GetInt("Game" + i + "Mode"));
            if(PlayerPrefs.GetInt("Game" + i + "Mode") == 0)
            {
                obj.GetChild(3).gameObject.SetActive(false);
                obj.GetChild(2).GetComponent<TMP_Text>().text = "Total Score: " +  PlayerPrefs.GetInt("Game" + i + "Score").ToString() + "\n" + "Received: " +  PlayerPrefs.GetInt("Game" + i + "Energy");
            }
            else
            {
                if (PlayerPrefs.HasKey("Game" + i + "Energy"))
                {
                    obj.GetChild(3).GetComponent<TMP_Text>().text = "Victory";
                    obj.GetChild(2).GetComponent<TMP_Text>().text = "Total Score: " + PlayerPrefs.GetInt("Game" + i + "Score") + "/" + ScoreLvl.score[0, PlayerPrefs.GetInt("Game" + i + "Lvl")] + "\n" + "Received: " + PlayerPrefs.GetInt("Game" + i + "Energy");
                }
                else
                {
                    obj.GetChild(3).GetComponent<TMP_Text>().text = "Lost";
                    obj.GetChild(2).GetComponent<TMP_Text>().text = "Total Score: " + PlayerPrefs.GetInt("Game" + i + "Score") + "/" + ScoreLvl.score[0, PlayerPrefs.GetInt("Game" + i + "Lvl")];
                }
            }
        }
        rect = content.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, (number + 1) * (pref.GetComponent<RectTransform>().rect.size.y + 20) + 30);
    }
}
