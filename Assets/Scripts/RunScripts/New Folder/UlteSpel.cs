using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public GameObject energyBallPrefab; // Префаб для шара энергии
    public GameObject beamPrefab; // Префаб для луча
    public float ballSpawnDistance = 1f; // Расстояние от персонажа для появления шара
    public float shrinkDuration = 1f; // Время, за которое шарик уменьшается
    public float beamRotationSpeed = 180f; // Скорость вращения луча (градусы в секунду)
    public float beamRadius = 2f; // Радиус вращения луча вокруг персонажа
    public float beamDuration = 1f; // Длительность существования луча
    public AudioClip spellSound; // Звук при касте заклинания
    private AudioSource audioSource; // Источник звука

    public float initialAngle = 45f; // Начальный угол при спавне луча

    private GameObject energyBall; // Активный шар
    private GameObject beamSprite; // Префаб луча
    private bool beamActive = false; // Флаг активности луча
    private bool isShrinking = false; // Флаг, когда шарик уменьшается
    private float shrinkTimer = 0f; // Таймер для отсчета времени уменьшения шара
    private float beamTimer = 0f; // Таймер для отсчета времени существования луча
    private float beamAngle = 0f; // Угол поворота луча

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Компонент AudioSource не найден на объекте " + gameObject.name);
        }

        if (energyBallPrefab == null || beamPrefab == null)
        {
            Debug.LogError("Не указан префаб для Energy Ball или Beam!");
        }
    }

    void Update()
    {
        HandleInput();
        HandleEnergyBallShrink();
        HandleBeamRotation();
    }

    void HandleInput()
    {
        // При нажатии клавиши E создаем шар и запускаем луч
        if (Input.GetKeyDown(KeyCode.E) && !beamActive && !isShrinking)
        {
            SpawnEnergyBall();
        }
    }

    void HandleEnergyBallShrink()
    {
        if (isShrinking && energyBall)
        {
            shrinkTimer += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 0f, shrinkTimer / shrinkDuration);
            energyBall.transform.localScale = Vector3.one * scale;

            if (shrinkTimer >= shrinkDuration)
            {
                // После того как шарик уменьшился, запускаем луч
                StartBeam(initialAngle);
                Destroy(energyBall); // Уничтожаем шар
                isShrinking = false; // Останавливаем уменьшение
            }
        }
    }

    void HandleBeamRotation()
    {
        if (beamActive && beamSprite)
        {
            // Поворачиваем луч вокруг персонажа на заданный угол
            beamAngle += beamRotationSpeed * Time.deltaTime;

            // Преобразуем угол в радианы и вычисляем позицию луча на круге с радиусом beamRadius
            float angleInRadians = beamAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);

            // Обновляем позицию луча, чтобы он вращался вокруг персонажа на расстоянии beamRadius
            beamSprite.transform.position = transform.position + direction * beamRadius;

            // Вращаем луч, чтобы он был ориентирован в сторону его движения
            beamSprite.transform.up = direction;

            // Обновляем таймер, чтобы уничтожить луч после beamDuration
            beamTimer += Time.deltaTime;
            if (beamTimer >= beamDuration)
            {
                Destroy(beamSprite); // Удаляем луч после окончания времени
                beamActive = false;  // Сбрасываем флаг активности
            }
        }
    }

    void SpawnEnergyBall()
    {
        // Позиция для шарика, немного впереди персонажа
        Vector3 spawnPosition = transform.position + transform.up * ballSpawnDistance;
        energyBall = Instantiate(energyBallPrefab, spawnPosition, Quaternion.identity);

        // Воспроизводим звук
        if (audioSource != null && spellSound != null)
        {
            audioSource.PlayOneShot(spellSound);
        }

        // Устанавливаем флаг, чтобы начать уменьшение шара
        isShrinking = true;
        shrinkTimer = 0f; // Сбрасываем таймер для уменьшения шара
    }

    void StartBeam(float angle)
    {
        // Позиция начала луча (спавн справа от персонажа)
        Vector3 beamStartPosition = transform.position + transform.right * ballSpawnDistance; // Используем transform.right для спавна справа
        beamSprite = Instantiate(beamPrefab, beamStartPosition, Quaternion.identity);

        // Начальный угол для луча
        beamAngle = angle; // Устанавливаем начальный угол
        beamSprite.transform.rotation = Quaternion.Euler(0f, 0f, beamAngle); // Применяем угол поворота

        // Устанавливаем начальную активность луча
        beamActive = true;
        beamTimer = 0f; // Сбрасываем таймер на 0
    }
}
