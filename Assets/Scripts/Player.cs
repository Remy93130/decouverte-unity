using UnityEngine;

public class Player : MonoBehaviour, IColorable
{
    #region Properties

    [Header("Translation & Rotation")]
    [Tooltip("Translation speed in m.s-1")]
    [SerializeField] float m_TranslationSpeed;
    [Tooltip("Rotation speed in m.s-1")]
    [SerializeField] float m_RotationSpeed;
    [Tooltip("Interpolation")]
    [SerializeField] float m_UprightRotKLerp;

    [Header("Ball shooting")]
    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPosition;
    [SerializeField] float m_BallInitialSpeed;
    [SerializeField] float m_BallLifeTime;
    [SerializeField] float m_ShotCooldown;
    float m_ShotNextTime;

    Rigidbody m_Rigidbody;

    bool mustTeleport = false;

    #endregion

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        m_ShotNextTime = Time.time;
        Debug.Log(Time.frameCount + " - Start");
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Comportement dynamique cinetique
    /// Nombre d appel gere dans:
    /// Project Settings -> Time -> Fixed Timestep
    /// </summary>
    private void FixedUpdate()
    {
        if (!GameManager.IsPlaying) return;

        if (mustTeleport)
        {
            Teleport();
        }
        else
        {
            MovePlayer();
            Fire();
        }
    }

    private void MovePlayer()
    {
        // Recuperation des inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        

        // Translation
        Vector3 translationVect = verticalInput * (transform.forward * m_TranslationSpeed * Time.fixedDeltaTime);
        m_Rigidbody.MovePosition(transform.position + translationVect);

        // Empecher les chutes
        float deltaAngle = horizontalInput * (m_RotationSpeed * Time.fixedDeltaTime);
        Quaternion qUpright = Quaternion.FromToRotation(transform.up, Vector3.up);

        // Rotation
        Quaternion qNextOrientation = Quaternion.Lerp(transform.rotation, qUpright * transform.rotation, m_UprightRotKLerp * Time.fixedDeltaTime);
        Quaternion qRotation = Quaternion.AngleAxis(deltaAngle, transform.up);
        qNextOrientation = qRotation * qNextOrientation;
        m_Rigidbody.MoveRotation(qNextOrientation);
    }

    private void Fire()
    {
        bool isFiring = Input.GetButton("Fire1");
        if (isFiring && Time.time > m_ShotNextTime)
        {
            GameObject newBall = Instantiate(m_BallPrefab, m_BallSpawnPosition.position, Quaternion.identity);
            newBall.GetComponent<Rigidbody>().AddForce(m_BallSpawnPosition.forward * m_BallInitialSpeed, ForceMode.VelocityChange);
            Destroy(newBall, m_BallLifeTime);
            m_ShotNextTime = Time.time + m_ShotCooldown;
        }
    }

    private void Teleport()
    {
        Vector2 randomVect = Random.insideUnitCircle * 4f;
        m_Rigidbody.MovePosition(transform.position + new Vector3(randomVect.x, 0, randomVect.y));
        mustTeleport = false;
    }

    public void ColorizeRandom()
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        if (mr)
        {
            mr.material.color = Random.ColorHSV();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        mustTeleport = true;
    }
}
