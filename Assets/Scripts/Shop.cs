using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public AudioSource sound;

    public void BuyCoin(int money)
    {
        if (money == 250000)
        {
            //if(1.99f ) донат
            Energy.instance.AddEnergy(money);
            sound.Play();
        }
        if (money == 1000000)
        {
            //if(4.99f ) донат
            Energy.instance.AddEnergy(money);
            sound.Play();
        }
    }

}
