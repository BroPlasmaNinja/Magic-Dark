using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile Settings")]
    public Sprite[] tileSprites; // Массив спрайтов тайлов
    public GameObject tilePrefab;  // Префаб для тайла (со SpriteRenderer)
    public float tileSize = 1.0f;   // Размер тайла (в единицах Unity)

    [Header("Map Generation")]
    public int mapRadius = 10;      // Радиус карты (от центра)
    public Vector2Int centerPosition = Vector2Int.zero; // Позиция центра карты
    public int seed = 0;            // Зерно для рандома

    [Header("Optimization")]
    public Transform tilesParent;   // Родительский объект для тайлов (для организации)
    public bool useParent = true; // Использовать родительский обьект для тайлов?
    private void OnValidate()
    {
        if (tileSprites == null || tileSprites.Length == 0)
        {
            Debug.LogWarning("Массив спрайтов тайлов пуст. Пожалуйста, назначьте спрайты.");
        }

        if (tilePrefab == null)
        {
            Debug.LogWarning("Префаб тайла не назначен. Пожалуйста, назначьте префаб.");
        }

        if (tileSize <= 0)
        {
            Debug.LogError("Размер тайла должен быть больше 0");
            tileSize = 1;
        }
    }

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        if (tileSprites == null || tileSprites.Length == 0 || tilePrefab == null)
        {
            Debug.LogError("Не настроены спрайты тайлов или префаб тайла!");
            return;
        }

        //Очистка старой карты, если она есть.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Random.InitState(seed); // Инициализируем генератор случайных чисел

        for (int x = -mapRadius; x <= mapRadius; x++)
        {
            for (int y = -mapRadius; y <= mapRadius; y++)
            {
                // Проверяем, находится ли тайл в пределах радиуса
                if (x * x + y * y <= mapRadius * mapRadius)
                {
                    CreateTile(x + centerPosition.x, y + centerPosition.y);
                }
            }
        }
    }

    void CreateTile(int x, int y)
    {
        // Выбираем случайный спрайт из массива
        Sprite tileSprite = tileSprites[Random.Range(0, tileSprites.Length)];

        // Создаем экземпляр префаба тайла
        GameObject tileObject = Instantiate(tilePrefab);

        // Устанавливаем позицию тайла
        tileObject.transform.position = new Vector3(x * tileSize, y * tileSize, 0);

        // Устанавливаем спрайт для SpriteRenderer
        SpriteRenderer renderer = tileObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = tileSprite;
        }
        else
        {
            Debug.LogError("Префаб тайла не имеет компонента SpriteRenderer!");
        }

        // Делаем тайл дочерним объектом генератора карты (для порядка)
        if (useParent)
        {
            if (tilesParent != null)
            {
                tileObject.transform.SetParent(tilesParent);
            }
            else
            {
                tileObject.transform.SetParent(transform);
            }
        }
        tileObject.name = "Tile_" + x + "_" + y; // Даем имя тайлу
    }
}