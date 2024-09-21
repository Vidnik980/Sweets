using UnityEngine;

public class SwapItems : MonoBehaviour
{
    public GameObject[] items;
    public int index;
    public void StateItems(int number)
    {
        items[index].SetActive(false);
        if (number >= items.Length)
            number = 0;
        if (number < 0)
            number = items.Length - 1;
        index = number;
        items[index].SetActive(true);
    }
    public void NextItem()
    {
        StateItems(index + 1);
    }
    public void LastItem()
    {
        StateItems(index - 1);
    }
}
