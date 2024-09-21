using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int energy;
    public int _energy
    {
        get { return energy; }
        set
        {
            energy = value;
            foreach (TMP_Text textEnergy in energyText)
            {
                textEnergy.text = _energy.ToString();
            }
            PlayerPrefs.SetInt("Energy", _energy);
        }
    }
    [SerializeField] private List<TMP_Text> energyText;

    public static Energy instance;
    private void Awake()
    {
        instance = this;
        _energy = PlayerPrefs.GetInt("Energy", 100);
    }
    public void AddEnergy(int number)
    {
        _energy += number;
        PlayerPrefs.SetInt("Mission" + 5, PlayerPrefs.GetInt("Mission" + 5) + number);
    }
    public bool TakeEnergy(int number)
    {
        if (_energy >= number)
        {
            _energy -= number;
            return true;
        }
        return false;
    }
}
