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
    [SerializeField] Image m_ShortPanel;
    [SerializeField] Image m_FullScreenPanel;
    [SerializeField] TextMeshProUGUI m_SlainedText;
    [SerializeField] TextMeshProUGUI m_GameOverText;
    [SerializeField] Image m_RestartButtonImage;
    [SerializeField] TextMeshProUGUI m_RestartButtonText;

    [Header("Crosshair")]
    [SerializeField] Image m_Crosshair;

    [SerializeField] StarterAssets.FirstPersonController m_FirstPersonControllerScript;

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
        DisableGameplayHUD();
        Cursor.visible = true;
        m_RoundsSurvivedText.text = "You survived " + m_CurrentWaveText.text + " rounds";
        m_FirstPersonControllerScript.enabled = false;
        StartCoroutine(PanelSequence());

        

    }

    IEnumerator PanelSequence()
    {
        Sequence firstSequence = DOTween.Sequence();

        firstSequence.Append(m_ShortPanel.DOFade(0.5f,1));
        firstSequence.Join(m_SlainedText.DOFade(1, 1));

        yield return firstSequence.WaitForCompletion();

        Sequence secondSequence = DOTween.Sequence();
        m_Restart.enabled = true;
        secondSequence.Append(m_RoundsSurvivedText.DOFade(1,1));
        secondSequence.Join(m_GameOverText.DOFade(1,1));
        secondSequence.Join(m_FullScreenPanel.DOFade(0.3f, 1));
        secondSequence.Join(m_RestartButtonImage.DOFade(1,1));
        secondSequence.Join(m_RestartButtonText.DOFade(1, 1));

    }


    public void UpdateCurrentWavesUI(int currentWave)
    {
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
