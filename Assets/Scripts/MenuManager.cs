using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    static MenuManager m_Instance;
    public static MenuManager Instance { get { return m_Instance; } }

    [SerializeField] GameObject m_MenuPanel;
    [SerializeField] GameObject m_VictoryPanel;

    List<GameObject> m_Panels = new List<GameObject>();

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
    }

    
    void Update()
    {
        
    }

    public void GameStateChanged(GameManager.GAMESTATE gameState)
    {
        switch (gameState)
        {
            case GameManager.GAMESTATE.Menu:
                OpenPanel(m_MenuPanel);
                break;
            case GameManager.GAMESTATE.Play:
                break;
            case GameManager.GAMESTATE.Pause:
                break;
            case GameManager.GAMESTATE.Victory:
                OpenPanel(m_VictoryPanel);
                break;
            case GameManager.GAMESTATE.GameOver:
                break;
        }
    }
}
