using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance;

    public static UIManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<UIManager>();
            }

            return m_Instance;
        }
    }


    [Header("Orbs")]
    [SerializeField] Slider m_HealthOrbSlider;
    [SerializeField] Slider m_ManaOrbSlider;
    [SerializeField] float refillSpeed;
    bool refilling = true;
    
    [Header("HUD")]
    [SerializeField] GameObject m_Wand;
    [SerializeField] GameObject m_HealthOrb;
    [SerializeField] TextMeshProUGUI m_WavesText;
    [SerializeField] TextMeshProUGUI m_CurrentWaveText;
    [SerializeField] TextMeshProUGUI m_WaveCompletedText;

    [Header("Game Over Screen")]
    [SerializeField] GameObject m_GameOverScreen;
    [SerializeField] TextMeshProUGUI m_RoundsSurvivedText;
    [SerializeField] Button m_Restart;


    [Header("Crosshair")]
    [SerializeField] Image m_Crosshair;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void DecreaseHealthValue(int decreaseAmount)
    {
        m_HealthOrbSlider.value -= decreaseAmount;
    }

    public void HealthRefill()
    {
        if (refilling)
        {
            m_HealthOrbSlider.value = m_HealthOrbSlider.value < 100f ? m_HealthOrbSlider.value + (Time.deltaTime * refillSpeed) : m_HealthOrbSlider.value;

            if (m_HealthOrbSlider.value >= 100f)
            {
                refilling = false;
            }
            
        }
    }

    public void DecreaseManaValue(int decreaseAmount)
    {
        m_ManaOrbSlider.value -= decreaseAmount;
    }

    public void InitiateGameOverScreenSequence()
    {
        //Turn Health and mana sliders off
        //Turn Wand Off
        //Turn Waves text off

        DisableGameplayHUD();
        Time.timeScale = 0;
        Cursor.visible = true;

        
        m_RoundsSurvivedText.text = "You survived " + m_CurrentWaveText.text + " rounds";
        m_GameOverScreen.SetActive(true);
        
        //Fade in panel and You have been slained text
        //Wait for two seconds
        //Fade in rest of UI components
        //m_ShortPanel.color = new Color32()
    }

    public void UpdateCurrentWavesUI(int currentWave)
    {
        Debug.Log("In sequence");
        StartCoroutine(WavesCompletedSequence(currentWave));
    }



    IEnumerator WavesCompletedSequence(int currentWave)
    {
        float resetPosition = m_CurrentWaveText.transform.localPosition.x;
        Sequence ScaleUpSequence = DOTween.Sequence();
        ScaleUpSequence.Append(m_WaveCompletedText.DOFade(1, 1));
        ScaleUpSequence.Join(m_CurrentWaveText.transform.DOLocalMoveX(250, 1));
        ScaleUpSequence.Join(m_CurrentWaveText.transform.DOScale(2, 1));
        m_CurrentWaveText.text = currentWave.ToString();
        yield return new WaitForSeconds(3.0f);
        Sequence ResetSequence = DOTween.Sequence();
        ResetSequence.Append(m_CurrentWaveText.transform.DOScale(1, 1));
        ResetSequence.Join(m_WaveCompletedText.DOFade(0, 1.2f));
        ResetSequence.Join(m_CurrentWaveText.transform.DOLocalMoveX(resetPosition, 1));
    }

   public void RestartButton()
    {
        Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Confined;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void DisableGameplayHUD()
    {
        m_WavesText.enabled = false;
        m_CurrentWaveText.enabled = false;
        m_Wand.SetActive(false);
        m_HealthOrb.SetActive(false);
        m_Crosshair.enabled = false;
    }




}
