using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ThrowSpell m_PlayerThrowSpellScript;
    bool m_isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.IsGameOver() == false && m_isGamePaused == false)
        {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && m_isGamePaused && UIManager.Instance.IsGameOver() == false)
        {
            ResumeGame();
        }

    }

    void PauseGame()
    {
        m_isGamePaused = true;
        m_PlayerThrowSpellScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.PauseMenu();
    }

   public void ResumeGame()
    {
        m_isGamePaused = false;
        Cursor.visible = false;
        m_PlayerThrowSpellScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.ResumeButton();
    }


}
