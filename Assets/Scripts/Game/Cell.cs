using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isActive = false;
    public GameObject candy;
    public bool isSpecial;
    public void AddCandy(GameObject obj, bool special = false)
    {
        candy = obj;
        isActive = true;
        isSpecial = special;
    }
    public int RemoveCandy()
    {
        Destroy(candy);
        isActive = false;
        if (isSpecial)
            return 1;
        return 0;
    }
}
