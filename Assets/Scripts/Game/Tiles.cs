using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Tiles : MonoBehaviour
{
    public List<Cell> cells;
    public GameObject[] selection;
    public int length = 8;
    public bool vector;
    public static Tiles instance;
    public AudioSource source;
    private void Awake()
    {
        instance = this;
        foreach (Transform child in transform)
        {
            cells.Add(child.GetComponent<Cell>());
        }
    }
    public void CheckLines()
    {
        vector = false;
        for (int x = 0; x < 2; x++)
        {
            for (int i = 0; i < length; i++)
            {
                int number = 0;
                for (int ii = 0; ii < length; ii++)
                {
                    if (cells[PosTile(i, ii)].isActive == true)
                        number++;
                }
                if (number == length)
                {
                    StartCoroutine(DeleteLine(i, vector));
                }
            }
            vector = true;

        }
    }
    private int PosTile(int i, int ii)
    {
        if (vector)
            return i * length + ii;
        else
            return i + length * ii;
    }
    private int PosTile(int i, int ii, bool vector1)
    {
        if (vector1)
            return i * length + ii;
        else
            return i + length * ii;
    }
    public void RemoveField()
    {
        foreach (Cell cell in cells)
        {
            cell.RemoveCandy();
        }
    }
    IEnumerator DeleteLine(int i, bool vector1)
    {
        GameObject obj;
        if (vector1 == true)
        {
            obj = selection[0];
            obj.transform.position = new Vector2(obj.transform.position.x, cells[PosTile(i, 0, vector1)].transform.position.y);
        }
        else
        {
            obj = selection[1];
            obj.transform.position = new Vector2(cells[PosTile(i, 0, vector1)].transform.position.x, obj.transform.position.y);
        }
        obj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        int special = 0;
        for (int ii = 0; ii < length; ii++)
        {
            special += cells[PosTile(i, ii, vector1)].RemoveCandy();
            source.Play();
            yield return new WaitForSeconds(0.2f);
        }
        obj.SetActive(false);
        if (ScoreLvl.score[1, GamePlay.instance.level] != 0)
            GamePlay.instance.AddScore(special);
        else
            GamePlay.instance.AddScore(length);
    }
}
