using UnityEngine;

public class CandySpawn : MonoBehaviour
{
    public Transform[] spawns = new Transform[3];
    public GameObject[] candy = new GameObject[3];
    public Sprite[] skins;
    public GameObject[] prefCandy;
    public static CandySpawn instance;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateGame()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(candy[i]);
            CreateCandy(i);
        }
        Tiles.instance.RemoveField();
    }
    public void CreateCandy(int number)
    {
        candy[number] = Instantiate(prefCandy[Random.Range(0, prefCandy.Length)]);
        candy[number].transform.parent = transform;
        candy[number].transform.position = spawns[number].transform.position;
        candy[number].GetComponent<MovableShape>().numberPos = number;

        int rnd = Random.Range(0, skins.Length);
        foreach (Transform child in candy[number].transform)
        {
            child.GetComponent<SpriteRenderer>().sprite = skins[rnd];
        }
        if (rnd == skins.Length - 1)
            candy[number].GetComponent<MovableShape>().isSpecial = true;
        Tiles.instance.CheckLines();
    }
}
