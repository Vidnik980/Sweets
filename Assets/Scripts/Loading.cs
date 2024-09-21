using UIEnhance;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public float timer;
    public Slider slider;
    private float time;
    private void Start()
    {
        slider.maxValue = timer;
    }
    public void Update()
    {
        time += Time.deltaTime;
        slider.value = time;
        if (time > timer)
        {
            UIManager.instance.OpenPanel("Main");
        }
    }
}
