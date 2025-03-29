using System.Collections;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;  // Префаб сундука, который будет спауниться
    public GameObject[] itemsToDrop;  // Список предметов, которые могут выпасть из сундука
    public float spawnRadius = 5f;  // Радиус вокруг героя, где будут спауниться сундуки
    public float minSpawnTime = 30f;  // Минимальное время между спауном сундуков (в секундах)
    public float maxSpawnTime = 60f;  // Максимальное время между спауном сундуков (в секундах)

    private Transform heroTransform;  // Ссылка на трансформ героя

    void Start()
    {
        // Получаем ссылку на трансформ героя (предполагаем, что скрипт прикреплен к герою)
        heroTransform = transform;

        // Запускаем корутину для спауна сундуков
        StartCoroutine(SpawnChests());
    }

    IEnumerator SpawnChests()
    {
        while (true)
        {
            // Генерируем случайное время между спауном сундуков (от 30 до 60 секунд)
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);

            // Ждем случайное время перед спауном нового сундука
            yield return new WaitForSeconds(spawnDelay);

            // Генерируем случайную позицию в радиусе от героя
            Vector3 spawnPosition = heroTransform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),  // Случайное смещение по X
                Random.Range(-spawnRadius, spawnRadius),  // Случайное смещение по Y
                0);  // Сундук будет спауниться на той же высоте, что и герой

            // Создаем сундук на вычисленной позиции
            GameObject newChest = Instantiate(chestPrefab, spawnPosition, Quaternion.identity);

            // Тегируем сундук для проверки наличия сундуков
            newChest.tag = "Chest";  // Присваиваем тег "Chest"

            Debug.Log("Сундук появился на позиции: " + spawnPosition);
        }
    }

    // Этот метод будет вызываться при столкновении с коллайдером
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, если это герой, который столкнулся с сундуком
        if (collision.gameObject.CompareTag("Chest"))
        {
            // Выпадает случайный предмет из списка
            DropRandomItem(transform.position);

            // Удаляем сундук
            Destroy(gameObject);

            Debug.Log("Сундук удален, так как герой столкнулся с ним!");
        }
    }

    // Функция для выпадения случайного предмета из списка
    void DropRandomItem(Vector3 position)
    {
        // Проверяем, есть ли предметы для выпадения
        if (itemsToDrop.Length > 0)
        {
            // Выбираем случайный предмет из списка
            GameObject randomItem = itemsToDrop[Random.Range(0, itemsToDrop.Length)];

            // Спауним выбранный предмет в месте, где был сундук
            Instantiate(randomItem, position, Quaternion.identity);
            Debug.Log("Предмет выпал на позицию: " + position);
        }
        else
        {
            Debug.LogWarning("Нет предметов для выпадения!");
        }
    }
}
