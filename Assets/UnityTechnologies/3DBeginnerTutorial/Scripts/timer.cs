using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public TextMeshProUGUI timeText;
    public float fadeDuration = 10f;
    public float displayImageDuration = 10f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
        {
            if (!m_HasAudioPlayed)
            {
                audioSource.Play();
                m_HasAudioPlayed = true;
            }

            m_Timer += Time.deltaTime;
            imageCanvasGroup.alpha = m_Timer / fadeDuration;

            if (m_Timer > fadeDuration + displayImageDuration)
            {
                if (doRestart)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                
                TimeLeft = 0;
                TimerOn = false;
                EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
