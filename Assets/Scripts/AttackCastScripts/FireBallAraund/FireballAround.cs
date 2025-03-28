using UnityEngine;

public class FireballAround : MonoBehaviour
{
    public int damage = 10;  // Урон, который наносит огненный шар
    public float speed = 5f; // Скорость вращения огненного шарика
    private float lifetime = 10f; // Время существования огненного шарика

    private Vector3 centerPosition; // Центр, вокруг которого будет вращаться огненный шар
    private float radius = 3f; // Радиус вращения, вокруг которого движутся шарики
    private float angle; // Текущий угол для вращения

    void Start()
    {
        Destroy(gameObject, lifetime); // Уничтожаем шар через 10 секунд
    }

    void Update()
    {
        // Увеличиваем угол для вращения, чтобы огненные шарики двигались по окружности
        angle += speed * Time.deltaTime;

        // Рассчитываем новое положение шарика по окружности относительно центра героя
        float x = Mathf.Cos(angle) * radius; // Новое X положение
        float y = Mathf.Sin(angle) * radius; // Новое Y положение

        // Обновляем позицию шарика с учетом центра (позиции героя)
        transform.position = centerPosition + new Vector3(x, y, 0f);
    }

    // Этот метод вызывается при столкновении с врагом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Получаем компонент врага и наносим урон
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    // Метод для инициализации огненного шарика
    public void Initialize(int newDamage, float newSpeed, Vector3 newCenter)
    {
        damage = newDamage;
        speed = newSpeed;
        centerPosition = newCenter; // Устанавливаем центр вращения как позицию героя
    }
}
