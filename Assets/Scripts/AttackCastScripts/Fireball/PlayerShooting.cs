void ShootFireball()
{
    if (closestEnemy != null)
    {
        // Вычисление направления к ближайшему врагу
        Vector3 direction = (closestEnemy.position - transform.position).normalized;

        // Выводим информацию в консоль для проверки направления
        Debug.Log("Direction: " + direction);  // Выводим вектор направления

        // Создаем огненный шарик
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Применяем вычисленный вектор направления как скорость
        fireball.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
    }
    else
    {
        Debug.Log("No closest enemy found");
    }
}
