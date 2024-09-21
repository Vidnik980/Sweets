using System.Collections.Generic;
using UnityEngine;

public class MovableShape : MonoBehaviour
{
    public Vector2 gridPosition; // Текущая позиция в сетке
    public float moveSpeed = 0.1f; // Скорость перемещения
    private Vector3 initialPosition; // Исходная позиция фигуры
    private bool isDragging = false; // Флаг перетаскивания
    public int numberPos;
    public bool isSpecial;
    private List<Cell> cells;

    private void Start()
    {
        initialPosition = transform.position; // Сохраняем исходную позицию
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Проверка нажатия на фигуру
            if (IsMouseOver())
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            MoveShape();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            PlaceShape();
            isDragging = false;
        }
    }

    private bool IsMouseOver()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = GetComponent<Collider2D>();
        return collider.OverlapPoint(mousePos);
    }

    private void MoveShape()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    private void PlaceShape()
    {

        if (IsPositionFree())
        {
            GridPosition();
            gridPosition = transform.position; // Обновляем позицию в сетке
            this.enabled = false;
            CandySpawn.instance.CreateCandy(numberPos);
        }
        else
        {
            // Если место занято, возвращаем в исходное положение
            transform.position = initialPosition;
        }
    }

    private void GridPosition()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            transform.GetChild(i).position = cells[i].transform.position;
        }
    }

    private bool IsPositionFree()
    {
        cells = new List<Cell>();
        foreach (Transform child in transform)
        {
            // Определяем позицию в мире на основе позиции в сетке
            Vector3 worldPosition = new Vector3(child.position.x, child.position.y, 0);

            // Проверяем наличие коллайдера в этой позиции
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            // Если луч попал во что-то
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Cell cell))
                {
                    cells.Add(cell);
                    // Возвращаем значение isActive
                    if (cell.isActive)
                        return false; // Если ячейка не активна, значит, она свободна
                }
                if (hit.collider.tag == "Bin")
                {
                    if(Energy.instance.TakeEnergy(5))
                    {
                        CandySpawn.instance.CreateCandy(numberPos);
                        Destroy(gameObject);
                        return false;
                    }
                }

            }
        }
        for (int i = 0; i < cells.Count; i++)
        {
            for (int ii = i+1; ii < cells.Count; ii++)
            {
                if (cells[i] == cells[ii])
                    return false;
            }
        }
        if (cells.Count != transform.childCount)
            return false;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].AddCandy(transform.GetChild(i).gameObject, isSpecial);
        }

        // Если ничего не найдено, считаем позицию свободной
        return true;
    }
}