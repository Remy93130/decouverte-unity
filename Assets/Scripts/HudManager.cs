using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    static HudManager m_Instance;

    [SerializeField] TMP_Text m_Counter;

    public static HudManager Instance { get { return m_Instance; } }

    private void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);
    }

    void SetCounterValue(int count) => m_Counter.text = count.ToString();

    void GameCounterChange(int counter) => SetCounterValue(counter);

    private void Start()
    {
        GameManager.OnGameCounterChange += GameCounterChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameCounterChange -= GameCounterChange;
    }
}
