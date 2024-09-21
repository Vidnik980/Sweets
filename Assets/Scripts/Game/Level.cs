using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text lvlText;
    public int lvl;
    public TMP_Text aimText;
    public GameObject lego;
    public SwapItems[] buttons;
    public TMP_Text recordLvlText;
    public void Awake()
    {
        LoadLvl();
    }
    public void SelectLvl(int number)
    {
        lvl = number;
        panel.SetActive(true);
        lvlText.text = "Lvl." + (lvl + 1);
        if (ScoreLvl.score[1, lvl] != 0)
        {
            aimText.text = "Aim " + ScoreLvl.score[1, lvl];
            lego.SetActive(true);
        }
        else
        {
            aimText.text = "Aim " + ScoreLvl.score[0, lvl];
            lego.SetActive(false);
        }
    }
    public void Play()
    {
        if (Energy.instance.TakeEnergy(3))
        {
            GamePlay.instance.StartLvl(lvl);
            panel.SetActive(false);
        }
    }
    public void NextLvl()
    {
        Energy.instance.TakeEnergy(3);
        if (lvl + 1 < buttons.Length - 1)
        {
            GamePlay.instance.StartLvl(lvl + 1);
            panel.SetActive(false);
        }
        else
        {
            GamePlay.instance.StartLvl(lvl);
            panel.SetActive(false);
        }
    }
    public void LoadLvl()
    {
        int record = -1;
        for (int i = 0; i < buttons.Length - 1; i++)
        {
            if (PlayerPrefs.HasKey("LvlCompleted" + i))
                buttons[i + 1].StateItems(1);
            else
                break;
            record = i;
        }
        recordLvlText.text = (record + 2).ToString();
    }
}
