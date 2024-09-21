using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public int activeItem;
    public SwapItems items;
    public SwapItems buttons;
    public int[] price;
    public TMP_Text priceText;
    public CharactersSelect charactersSelect;
    private void OnEnable()
    {
        OpenItem(0);
    }
    public void NextItem()
    {
        activeItem++;
        if (activeItem >= price.Length)
            activeItem = 0;
        OpenItem(activeItem);
    }
    public void LastItem()
    {
        activeItem--;
        if (activeItem < 0)
            activeItem = price.Length - 1;
        OpenItem(activeItem);
    }
    public void OpenItem(int number)
    {
        activeItem = number;
        items.StateItems(number);
        priceText.text = price[number].ToString();
        buttons.StateItems(PlayerPrefs.GetInt("Characters" + number));
        if (PlayerPrefs.GetInt("Characters" + "Active") != 0 && activeItem == 0)
        {
            buttons.StateItems(1);
        }
    }
    public void BuyItem()
    {
        if(Energy.instance.TakeEnergy(price[activeItem]))
        {
            PlayerPrefs.SetInt("Characters" + activeItem, 1);
            buttons.StateItems(1);
            PlayerPrefs.SetInt("Mission" + 2, PlayerPrefs.GetInt("Mission" + 2) + 1);
        }
    }
    public void SelectItem()
    {
        PlayerPrefs.SetInt("Characters" + PlayerPrefs.GetInt("Characters" + "Active"), 1);
        PlayerPrefs.SetInt("Characters" + "Active", activeItem);
        PlayerPrefs.SetInt("Characters" + activeItem, 2);
        buttons.StateItems(2);
        charactersSelect.OpenItem(activeItem);
    }
}
