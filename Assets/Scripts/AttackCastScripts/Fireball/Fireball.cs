using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 10;  // Урон, который наносит огненный шар

    // Этот метод вызывается при столкновении с другими объектами
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Выводим в консоль информацию о столкновении
        Debug.Log("Fireball collided with: " + collision.gameObject.name);

        // Проверяем, если столкнулись с объектом, у которого есть тег "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Получаем компонент врага (например, если у врага есть скрипт Health, который отвечает за здоровье)
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Наносим урон врагу
                enemyHealth.TakeDamage(damage);
                Debug.Log("Damage applied to enemy");
            }
            else
            {
                Debug.Log("Enemy does not have EnemyHealth component");
            }

            // Уничтожаем огненный шар после столкновения
            Destroy(gameObject);
        }
    }
}
