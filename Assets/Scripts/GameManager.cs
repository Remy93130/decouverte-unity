using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager m_Instance;

    public static GameManager Instance { get { return m_Instance; } }

    // TODO: Le bouger
    public enum GAMESTATE { Menu, Play, Pause, Victory, GameOver }

    GAMESTATE m_State;

    public static bool IsPlaying { get { return m_Instance.m_State == GAMESTATE.Play; } }

    public static event Action<GAMESTATE> OnGameStateChanged;

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

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GAMESTATE.Menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
