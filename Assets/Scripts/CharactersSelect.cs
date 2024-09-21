using UnityEngine;

public class CharactersSelect : MonoBehaviour
{
    public int activeItem;
    public SwapItems caracters;
    public Characters mainScr;
    private void OnEnable()
    {
        
    }
    public void NextItem()
    {
        activeItem++;
        if (activeItem >= caracters.items.Length)
            activeItem = 0;
        if (PlayerPrefs.GetInt("Characters" + activeItem) == 0 && activeItem != 0)
            NextItem();
        else
        {
            mainScr.OpenItem(activeItem);
            mainScr.SelectItem();
        }
    }
    public void LastItem()
    {
        activeItem--;
        if (activeItem < 0)
            activeItem = caracters.items.Length - 1;
        if (PlayerPrefs.GetInt("Characters" + activeItem) == 0 && activeItem != 0)
            LastItem();
        else
        {
            mainScr.OpenItem(activeItem);
            mainScr.SelectItem();
        }
    }
    public void OpenItem(int number)
    {
        activeItem = number;
        caracters.StateItems(activeItem);
    }
}
