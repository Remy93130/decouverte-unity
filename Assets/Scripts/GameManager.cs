using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager m_Instance;

    public static GameManager Instance { get { return m_Instance; } }

    GAMESTATE m_State;

    [SerializeField] Player m_Player;
    [SerializeField] int m_VictoryValue;

    int m_Counter = 0;

    public static bool IsPlaying { get { return m_Instance.m_State == GAMESTATE.Play; } }

    public static event Action<GAMESTATE> OnGameStateChanged;
    public static event Action<int> OnGameCounterChange;

    public void ChangeState(GAMESTATE state)
    {
        m_State = state;
        OnGameStateChanged?.Invoke(m_State);
    }

    private void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);
    }

    IEnumerator Start()
    {
        MenuManager.OnPlayButtonClick += PlayButtonClick;
        // On attend que le MenuManager s abonne
        while (!MenuManager.IsReady)
            yield return null;
        if (m_Player)
            m_Player.OnPlayerRecolorized += PlayerRecolorized;
        ChangeState(GAMESTATE.Menu);
    }

    void SetCounter(int counter)
    {
        m_Counter = counter;
        OnGameCounterChange?.Invoke(m_Counter);
    }

    private void PlayerRecolorized()
    {
        if (!IsPlaying) return;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        SetCounter(++m_Counter);
        if (m_Counter >= m_VictoryValue)
            Victory();
    }

    private void OnDestroy()
    {
        MenuManager.OnPlayButtonClick -= PlayButtonClick;
    }

    void PlayButtonClick()
    {
        SetCounter(0);
        ChangeState(GAMESTATE.Play);
    }

    private void Victory()
    {
        ChangeState(GAMESTATE.Victory);
    }
}
