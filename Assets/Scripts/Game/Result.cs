using Febucci.UI.Effects;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public TMP_Text lvl;
    public TMP_Text score;
    public GameObject lego;
    public TMP_Text energy;
    public bool isVictory;
    public void ResultData(int point)
    {
        gameObject.SetActive(true);
        if (lvl)
        {
            lvl.text = "Lvl." + (GamePlay.instance.level + 1);
            if (ScoreLvl.score[1, GamePlay.instance.level] != 0)
                lego.SetActive(true);
            else
                lego.SetActive(false);
            if (isVictory)
            {
                score.text = point.ToString();
                energy.text = (10 + GamePlay.instance.level).ToString();
                Energy.instance.AddEnergy(10 + GamePlay.instance.level);
            }
            else
            {
                if (ScoreLvl.score[1, GamePlay.instance.level] != 0)
                    score.text = point + "/" + ScoreLvl.score[1, GamePlay.instance.level];
                else
                    score.text = point + "/" + ScoreLvl.score[0, GamePlay.instance.level];
            }
        }
        else
        {
            score.text = point.ToString();

            energy.text = (point / 100 * 3).ToString();
            Energy.instance.AddEnergy(point / 100 * 3);
        }

        int number = PlayerPrefs.GetInt("Games") + 1;
        PlayerPrefs.SetInt("Games", number);
        PlayerPrefs.SetInt("Game" + number + "Score", point);
        if(lvl)
        {
            PlayerPrefs.SetInt("Game" + number + "Mode", 1);
            PlayerPrefs.SetInt("Game" + number + "Lvl", GamePlay.instance.level);
            if(isVictory)
                PlayerPrefs.SetInt("Game" + number + "Energy", (10 + GamePlay.instance.level));
        }
        else
        {
            PlayerPrefs.SetInt("Game" + number + "Mode", 0);
            PlayerPrefs.SetInt("Game" + number + "Energy", (point / 100 * 3));
        }

        if (GamePlay.instance.level > PlayerPrefs.GetInt("Mission" + 0))
            PlayerPrefs.SetInt("Mission" + 0, GamePlay.instance.level);
        if (point > PlayerPrefs.GetInt("Mission" + 1))
            PlayerPrefs.SetInt("Mission" + 1, point);

    }
}
