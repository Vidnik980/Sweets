using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public TMP_Text timerText; // ���� ��� ����������� �������
    public TMP_Text DayText;
    public SwapItems pet;
    public SwapItems buttons;

    public GameObject panelDead;
    public GameObject panelPrice;

    private void Start()
    {

        // ��������, ����� �� ��������� �������
        CheckFeedAvailability();

        // �������� �������� ��� �������
        StartCoroutine(UpdateTimer());

        pet.StateItems(PlayerPrefs.GetInt("Combo"));
        DayText.text = (PlayerPrefs.GetInt("Combo") + 1) + "/9";
    }

    public void FeedPet()
    {
        // �������� ������� �����
        System.DateTime now = System.DateTime.Now;

        // �������� ���� ���������� ���������
        string lastFeedDate = PlayerPrefs.GetString("LastFeedDate", "");

        if (PlayerPrefs.GetInt("Combo") >= 8)
        {
            panelPrice.SetActive(true);
        }
        else
            PlayerPrefs.SetInt("Combo", PlayerPrefs.GetInt("Combo") + 1);

        buttons.StateItems(1);

        DayText.text = (PlayerPrefs.GetInt("Combo") + 1) + "/9";
        // ���������, ��������� �� ���� ���������� ��������� � �����������
        if (lastFeedDate != now.ToString("yyyy-MM-dd"))
        {
            // ��������� ���� ���������� ���������
            PlayerPrefs.SetString("LastFeedDate", now.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
    }

    private void CheckFeedAvailability()
    {
        // �������� ������� �����
        System.DateTime now = System.DateTime.Now;

        // �������� ���� ���������� ���������
        string lastFeedDate = PlayerPrefs.GetString("LastFeedDate", "");
        string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

        // ���������, ��������� �� ���� ���������� ��������� � �����������
        if (lastFeedDate != now.ToString("yyyy-MM-dd"))
        {
            if (lastFeedDate == yesterday)
            {
                buttons.StateItems(0);
            }
            else
            {
                panelDead.SetActive(true);
                PlayerPrefs.SetInt("Combo", 0);
                pet.StateItems(0);
                DayText.text = "1/9";
            }
        }
        else
        {
            //feedbackText.text = "�� ��� ��������� ������� �������!";
            buttons.StateItems(1);
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            // �������� ������� �����
            System.DateTime now = System.DateTime.Now;

            // ������� ����� �� ��������
            System.DateTime midnight = now.Date.AddDays(1);
            TimeSpan timeLeft = midnight - now;

            // ��������� ����� �������
            timerText.text = string.Format("{0:D2}:{1:D2}",
                timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);

            // ���� 1 ������� ����� �����������
            yield return new WaitForSeconds(1);
        }
    }
    public void GetGift()
    {
        Energy.instance.AddEnergy(80);
        panelPrice.SetActive(false);
        PlayerPrefs.SetInt("Combo", 0);
        pet.StateItems(0);
        DayText.text = "1/9";
    }
}
