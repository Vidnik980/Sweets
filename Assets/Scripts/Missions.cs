using System.Collections;
using TMPro;
using UnityEngine;

public class Missions : MonoBehaviour
{
    public SwapItems[] complete;
    public int[] missions;
    public int[] missionsMax;
    public TMP_Text[] progress;
    public GameObject[] buttons;
    private void Start()
    {
        StartCoroutine(Timer());
    }
    public void UpdateInfo()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            missions[i] = PlayerPrefs.GetInt("Mission" + i);
            progress[i].text = missions[i] + "/" + missionsMax[i];
            CheckReward(i);
        }
    }

    private void CheckReward(int number)
    {
        if (missions[number] >= missionsMax[number])
        {
            buttons[number].SetActive(true);
        }
        if (PlayerPrefs.GetInt("MissionCompleted" + number) == 1)
        {
            complete[number].StateItems(1);
            buttons[number].SetActive(false);
        }

    }
    public void GetReward(int number)
    {
        Energy.instance.AddEnergy(10);
        complete[number].StateItems(1);
        buttons[number].gameObject.SetActive(false);
        PlayerPrefs.SetInt("MissionCompleted" + number, 1);
    }
    IEnumerator Timer()
    {
        var wait = new WaitForSeconds(10);
        while (true)
        {
            UpdateInfo();
            yield return wait;
        }
    }
}
