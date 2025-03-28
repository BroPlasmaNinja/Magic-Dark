using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;  // Начальное количество здоровья врага

    // Метод для нанесения урона
    public void TakeDamage(int damage)
    {
        health -= damage;  // Уменьшаем здоровье на величину урона

        if (health <= 0)
        {
            Die();  // Если здоровье меньше или равно 0, вызываем метод смерти
        }
    }

    // Метод смерти
    private void Die()
    {
        // Логика смерти врага (например, уничтожение объекта)
        Destroy(gameObject);  // Уничтожаем объект врага
    }
}
