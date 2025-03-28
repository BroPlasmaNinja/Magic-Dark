using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public GameObject fireballPrefab; // Префаб огненного шарика
    public float fireballSpeed = 5f;  // Скорость вращения огненного шарика
    public float attackRadius = 3f;   // Радиус, в котором будут летать огненные шарики
    public float attackInterval = 10f; // Интервал между атаками в секундах
    public int increasedDamage = 20;  // Урон, который будет наносить огненный шар

    private float timeSinceLastAttack = 0f;

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackInterval)
        {
            timeSinceLastAttack = 0f;
            SpawnFireballs(); // Создаем огненные шарики
        }
    }

    void SpawnFireballs()
    {
        // Создаем несколько огненных шариков вокруг героя
        for (int i = 0; i < 8; i++)  // 8 шариков по кругу
        {
            // Вычисляем угол для каждого шарика на окружности
            float angle = i * Mathf.PI * 2 / 8;
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * attackRadius + transform.position;

            // Создаем огненный шар
            GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

            // Инициализируем огненный шар с нужным уроном, скоростью и позицией героя
            fireball.GetComponent<FireballAround>().Initialize(increasedDamage, fireballSpeed, transform.position); // Передаем позицию героя как центр вращения
        }
    }
}
