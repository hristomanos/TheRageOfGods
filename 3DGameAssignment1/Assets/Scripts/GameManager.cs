using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ThrowSpell m_PlayerThrowSpellScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.IsGameOver() == false)
        {
            PauseGame();
        }     
    }

    void PauseGame()
    {
        m_PlayerThrowSpellScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.PauseMenu();
    }

   public void ResumeGame()
    {
        Cursor.visible = false;
        m_PlayerThrowSpellScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.ResumeButton();
    }


}
