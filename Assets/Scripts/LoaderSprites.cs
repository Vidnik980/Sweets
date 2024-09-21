using UnityEngine;
using UnityEngine.UI;

public class LoaderSprites : MonoBehaviour
{
    public string name;
    public Sprite[] sprites;
    private void OnEnable()
    {
        GetComponent<Image>().sprite = sprites[PlayerPrefs.GetInt(name)];
    }
}
