using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    static MenuManager m_Instance;

    bool m_IsReady = false;
    
    public static bool IsReady { get { return m_Instance.m_IsReady; } }

    public static MenuManager Instance { get { return m_Instance; } }

    [SerializeField] GameObject m_MenuPanel;
    [SerializeField] GameObject m_VictoryPanel;

    List<GameObject> m_Panels = new List<GameObject>();

    public static event Action OnPlayButtonClick;

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => item.SetActive(item == panel));
    }

    private void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);

        m_Panels.Add(m_MenuPanel);
        m_Panels.Add(m_VictoryPanel);
    }


    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
        m_IsReady = true;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }

    public void GameStateChanged(GAMESTATE gameState)
    {
        switch (gameState)
        {
            case GAMESTATE.Menu:
                OpenPanel(m_MenuPanel);
                break;
            case GAMESTATE.Play:
                OpenPanel(null);
                break;
            case GAMESTATE.Pause:
                break;
            case GAMESTATE.Victory:
                OpenPanel(m_VictoryPanel);
                break;
            case GAMESTATE.GameOver:
                break;
        }
    }

    public void PlayButtonClick() => OnPlayButtonClick?.Invoke();
}
