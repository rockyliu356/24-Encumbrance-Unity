using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Metaface.Utilities;

public class BaselineManage : MonoBehaviour
{
    [Header("Blink Tracking")]
    public BlinkHelper blinkHelper;

    [Header("UI References")]
    public TextMeshProUGUI countdownText;
    public GameObject questionnaireCanvas;
    public GameObject submitButton;


    public Slider q1Slider;
    public Slider q2Slider;
    public Slider q3Slider;
    public Slider q4Slider;
    public Slider q5Slider;
    public Slider q6Slider;
    public Slider q7Slider;
    public Slider q8Slider;
    public Slider q9Slider;
    public Slider q10Slider;

    // Timer
    private float timeRemaining = 180f;  // 3 minutes 
    private bool timerRunning = false;

    // CSV logging path
    private string csvPath;
    private string blinkLogPath;
    private float totalTimeElapsed = 0f;
    private bool blinkLoggingActive = false;

    void Start()
    {
        // Hide the questionnaire by default
        questionnaireCanvas.SetActive(false);
        submitButton.SetActive(false);


        // Prepare the countdown text
        countdownText.text = "03:00";

        // Create a CSV file path
        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        csvPath = Path.Combine(Application.persistentDataPath, "Questionnaire_" + timeStamp + ".csv");

        // Optionally write header row to CSV
        string header = "BlurredNear,BlurredDist,SlowRefocus,IrritatedEyes,DryEyes,Eyestrain,Headache,TiredEyes,Sensitivity,Discomfort,CurrentTime\n";
        File.AppendAllText(csvPath, header);

        // Initialize blink logging
        blinkLogPath = Path.Combine(Application.persistentDataPath, "BlinkLog_" + timeStamp + ".csv");
        File.AppendAllText(blinkLogPath, "BlinkDuration(ms),CurrentTime\n");

        blinkHelper.OnBlink.AddListener(HandleBlinkLogged);

        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning)
        {
            // Decrement time
            timeRemaining -= Time.deltaTime;
            totalTimeElapsed += Time.deltaTime;

            // 第1分钟后开启 blink 记录
            if (!blinkLoggingActive && totalTimeElapsed >= 60f)
            {
                blinkLoggingActive = true;
            }


            if (timeRemaining <= 0f)
            {
                // Time's up
                timerRunning = false;
                countdownText.text = "00:00";
                OnTimeComplete();
            }
            else
            {
                // Update countdown display mm:ss
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                countdownText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }
    }


    /// <summary>
    /// Called automatically after 3 minutes.
    /// </summary>
    private void OnTimeComplete()
    {
        countdownText.gameObject.SetActive(false);
        // Show the questionnaire
        questionnaireCanvas.SetActive(true);
        submitButton.SetActive(true);

        blinkHelper.OnBlink.RemoveListener(HandleBlinkLogged);
    }

    /// <summary>
    /// Suppose you have a "Submit" button on your questionnaire.
    /// Hook up that button to call this method.
    /// </summary>
    public void OnSubmitQuestionnaire()
    {
        // Gather all slider values
        float blurredNear = q1Slider.value;
        float blurredDist = q2Slider.value;
        float slowRefocus = q3Slider.value;
        float irritatedEyes = q4Slider.value;
        float dryEyes = q5Slider.value;
        float eyestrain = q6Slider.value;
        float headache = q7Slider.value;
        float tiredEyes = q8Slider.value;
        float sensitivity = q9Slider.value;
        float discomfort = q10Slider.value;

        // Compose a single CSV line
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string line = string.Format(
            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}\n",
            blurredNear, blurredDist, slowRefocus, irritatedEyes, dryEyes,
            eyestrain, headache, tiredEyes, sensitivity, discomfort,
            currentTime
        );


        // Append to CSV
        File.AppendAllText(csvPath, line);

        // Hide the questionnaire
        questionnaireCanvas.SetActive(false);

        // (Optional) move to next scene or do something else
        // SceneManager.LoadScene("NextScene");
    }

    private void HandleBlinkLogged(BlinkHelper.BlinkEventArgs args)
    {
        // 只在 1 分钟后开始记录，且最多记录 2 分钟
        if (!blinkLoggingActive || totalTimeElapsed > 180f)
            return;

        string blinkTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        float durationMs = args.EyesClosedTime * 1000f;

        string line = $"{durationMs},{blinkTimeStamp}\n";
        File.AppendAllText(blinkLogPath, line);
    }

}
