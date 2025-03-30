using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile Settings")]
    public Sprite[] tileSprites; // ������ �������� ������
    public GameObject tilePrefab;  // ������ ��� ����� (�� SpriteRenderer)
    public float tileSize = 1.0f;   // ������ ����� (� �������� Unity)

    [Header("Map Generation")]
    public int mapRadius = 10;      // ������ ����� (�� ������)
    public Vector2Int centerPosition = Vector2Int.zero; // ������� ������ �����
    public int seed = 0;            // ����� ��� �������

    [Header("Optimization")]
    public Transform tilesParent;   // ������������ ������ ��� ������ (��� �����������)
    public bool useParent = true; // ������������ ������������ ������ ��� ������?
    private void OnValidate()
    {
        if (tileSprites == null || tileSprites.Length == 0)
        {
            Debug.LogWarning("������ �������� ������ ����. ����������, ��������� �������.");
        }

        if (tilePrefab == null)
        {
            Debug.LogWarning("������ ����� �� ��������. ����������, ��������� ������.");
        }

        if (tileSize <= 0)
        {
            Debug.LogError("������ ����� ������ ���� ������ 0");
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
            Debug.LogError("�� ��������� ������� ������ ��� ������ �����!");
            return;
        }

        //������� ������ �����, ���� ��� ����.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Random.InitState(seed); // �������������� ��������� ��������� �����

        for (int x = -mapRadius; x <= mapRadius; x++)
        {
            for (int y = -mapRadius; y <= mapRadius; y++)
            {
                // ���������, ��������� �� ���� � �������� �������
                if (x * x + y * y <= mapRadius * mapRadius)
                {
                    CreateTile(x + centerPosition.x, y + centerPosition.y);
                }
            }
        }
    }

    void CreateTile(int x, int y)
    {
        // �������� ��������� ������ �� �������
        Sprite tileSprite = tileSprites[Random.Range(0, tileSprites.Length)];

        // ������� ��������� ������� �����
        GameObject tileObject = Instantiate(tilePrefab);

        // ������������� ������� �����
        tileObject.transform.position = new Vector3(x * tileSize, y * tileSize, 0);

        // ������������� ������ ��� SpriteRenderer
        SpriteRenderer renderer = tileObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = tileSprite;
        }
        else
        {
            Debug.LogError("������ ����� �� ����� ���������� SpriteRenderer!");
        }

        // ������ ���� �������� �������� ���������� ����� (��� �������)
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
        tileObject.name = "Tile_" + x + "_" + y; // ���� ��� �����
    }
}