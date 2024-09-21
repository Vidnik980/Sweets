using System.Xml;
using TMPro;
using UIEnhance;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public int score;
    public int level;
    public TMP_Text lvlText;
    public TMP_Text scoreText;
    public TMP_Text scoreMaxText;
    public Slider slider;
    public GameObject game;
    public Result victoryPanel;
    public Result losePanel;
    public Result endlessPanel;
    public bool isEndless;
    public static GamePlay instance;
    private void Awake()
    {
        instance = this;
    }
    public void StartLvl(int lvl)
    {
        UIManager.instance.OpenPanel("Game");
        score = 0;
        level = lvl;
        lvlText.text = "Lvl." + (level + 1);
        isEndless = false;
        slider.gameObject.SetActive(true);
        scoreText.transform.parent.gameObject.SetActive(false);
        game.SetActive(true);
        slider.value = score;
        slider.transform.GetChild(3).GetComponent<TMP_Text>().text = score.ToString();
        if (ScoreLvl.score[1, level] != 0)
        {
            scoreMaxText.text = ScoreLvl.score[1, level].ToString();
            slider.maxValue = ScoreLvl.score[1, level];
        }
        else
        {
            scoreMaxText.text = ScoreLvl.score[0, level].ToString();
            slider.maxValue = ScoreLvl.score[0, level];
        }
        CandySpawn.instance.UpdateGame();
    }
    public void StartEndless()
    {
        UIManager.instance.OpenPanel("Game");
        score = 0;
        isEndless = true;
        slider.gameObject.SetActive(false);
        scoreText.transform.parent.gameObject.SetActive(true);
        game.SetActive(true);
        scoreText.text = "Score: " + score;
        CandySpawn.instance.UpdateGame();
    }
    public void AddScore(int point)
    {
        score += point;
        if (isEndless)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            slider.value = score;
            slider.transform.GetChild(3).GetComponent<TMP_Text>().text = score.ToString();
            if (slider.value == slider.maxValue)
                EndGame();
        }
    }
    public void Restart()
    {
        if (isEndless)
        {
            Energy.instance.TakeEnergy(5);
            StartEndless();
        }
        else
        {
            Energy.instance.TakeEnergy(3);
            StartLvl(level);
        }
    }
    public void EndGame()
    {
        game.SetActive(false);
        CandySpawn.instance.UpdateGame();
        if (isEndless)
            endlessPanel.ResultData(score);
        else if (slider.value == slider.maxValue)
        {
            PlayerPrefs.SetInt("LvlCompleted" + level, 1);
            victoryPanel.ResultData(score);
        }
        else
            losePanel.ResultData(score);
    }
}
public static class ScoreLvl
{
    public static int[,] score = new int[2, 15] { { 90, 10, 30, 150, 170, 190, 200, 230, 300, 350, 300, 450, 500, 500, 1000 }, { 0, 10, 30, 0, 0, 0, 200, 0, 0, 0, 300, 0, 0, 500, 0 } };
}
