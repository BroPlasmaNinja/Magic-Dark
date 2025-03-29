using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public GameObject energyBallPrefab; // ������ ��� ���� �������
    public GameObject beamPrefab; // ������ ��� ����
    public float ballSpawnDistance = 1f; // ���������� �� ��������� ��� ��������� ����
    public float shrinkDuration = 1f; // �����, �� ������� ����� �����������
    public float beamRotationSpeed = 180f; // �������� �������� ���� (������� � �������)
    public float beamRadius = 2f; // ������ �������� ���� ������ ���������
    public float beamDuration = 1f; // ������������ ������������� ����
    public AudioClip spellSound; // ���� ��� ����� ����������
    private AudioSource audioSource; // �������� �����

    public float initialAngle = 45f; // ��������� ���� ��� ������ ����

    private GameObject energyBall; // �������� ���
    private GameObject beamSprite; // ������ ����
    private bool beamActive = false; // ���� ���������� ����
    private bool isShrinking = false; // ����, ����� ����� �����������
    private float shrinkTimer = 0f; // ������ ��� ������� ������� ���������� ����
    private float beamTimer = 0f; // ������ ��� ������� ������� ������������� ����
    private float beamAngle = 0f; // ���� �������� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("��������� AudioSource �� ������ �� ������� " + gameObject.name);
        }

        if (energyBallPrefab == null || beamPrefab == null)
        {
            Debug.LogError("�� ������ ������ ��� Energy Ball ��� Beam!");
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
        // ��� ������� ������� E ������� ��� � ��������� ���
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
                // ����� ���� ��� ����� ����������, ��������� ���
                StartBeam(initialAngle);
                Destroy(energyBall); // ���������� ���
                isShrinking = false; // ������������� ����������
            }
        }
    }

    void HandleBeamRotation()
    {
        if (beamActive && beamSprite)
        {
            // ������������ ��� ������ ��������� �� �������� ����
            beamAngle += beamRotationSpeed * Time.deltaTime;

            // ����������� ���� � ������� � ��������� ������� ���� �� ����� � �������� beamRadius
            float angleInRadians = beamAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);

            // ��������� ������� ����, ����� �� �������� ������ ��������� �� ���������� beamRadius
            beamSprite.transform.position = transform.position + direction * beamRadius;

            // ������� ���, ����� �� ��� ������������ � ������� ��� ��������
            beamSprite.transform.up = direction;

            // ��������� ������, ����� ���������� ��� ����� beamDuration
            beamTimer += Time.deltaTime;
            if (beamTimer >= beamDuration)
            {
                Destroy(beamSprite); // ������� ��� ����� ��������� �������
                beamActive = false;  // ���������� ���� ����������
            }
        }
    }

    void SpawnEnergyBall()
    {
        // ������� ��� ������, ������� ������� ���������
        Vector3 spawnPosition = transform.position + transform.up * ballSpawnDistance;
        energyBall = Instantiate(energyBallPrefab, spawnPosition, Quaternion.identity);

        // ������������� ����
        if (audioSource != null && spellSound != null)
        {
            audioSource.PlayOneShot(spellSound);
        }

        // ������������� ����, ����� ������ ���������� ����
        isShrinking = true;
        shrinkTimer = 0f; // ���������� ������ ��� ���������� ����
    }

    void StartBeam(float angle)
    {
        // ������� ������ ���� (����� ������ �� ���������)
        Vector3 beamStartPosition = transform.position + transform.right * ballSpawnDistance; // ���������� transform.right ��� ������ ������
        beamSprite = Instantiate(beamPrefab, beamStartPosition, Quaternion.identity);

        // ��������� ���� ��� ����
        beamAngle = angle; // ������������� ��������� ����
        beamSprite.transform.rotation = Quaternion.Euler(0f, 0f, beamAngle); // ��������� ���� ��������

        // ������������� ��������� ���������� ����
        beamActive = true;
        beamTimer = 0f; // ���������� ������ �� 0
    }
}
