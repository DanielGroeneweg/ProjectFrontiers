using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PointsManager : MonoBehaviour
{
    #region variables
    // In Unity Inspector
    [Header("Required Components")]
    [SerializeField] private CarControl carControl;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private PlayTimer timer;
    [SerializeField] private TMP_Text driftScorePrefab;
    [SerializeField] private Canvas mainCanvas;

    [Header("Stats")]
    [SerializeField] private float scoreIncreaseForDrifting;
    [SerializeField] private float driftAngleMinumim;
    [SerializeField] private float pointsPerMoney;
    [SerializeField] private float pointsPerSecondLeft;
    [SerializeField] private float timeBeforeStartingDrift;
    [SerializeField] private float timeBeforeStoppingDrift;

    // Not In Unity Inspector
    [HideInInspector] public bool isDrifting;

    // Internal
    private float score;
    private float driftScore;
    private float driftTime;
    private bool PointsCanBeScored = true;
    private bool startedDrift = false;
    private float time;
    private TMP_Text driftScoreObject;
    private bool instantiated = false;
    #endregion

    private void FixedUpdate()
    {
        if (PointsCanBeScored)
        {
            CheckForDrifting();
        }

        else if (driftScore > 0) ApplyDirftScore();
    }

    #region drift- & score-Calculations
    private void CheckForDrifting()
    {
        StopAndStartDrift();

        DoDrifting();        
    }
    private void StopAndStartDrift()
    {
        // Calculate the angle from the car's velocity direction to the car's face direction
        float angle = Vector3.Angle(playerRB.transform.forward, (playerRB.velocity + playerRB.transform.forward).normalized);

        // If the car is sideways, start drifting
        if (angle > driftAngleMinumim && carControl.isOnGround())
        {
            startedDrift = true;
        }

        // If the car is not sideways ...
        else
        {
            // ... and it has not actually begun drifting
            // Stop
            if (startedDrift && !isDrifting)
            {
                startedDrift = false;
                driftTime = 0;
            }

            // ... and it has also already started to drift
            else if (startedDrift && isDrifting)
            {
                time += Time.deltaTime;

                // stop drifting after not drifting for x seconds
                if (time >= timeBeforeStoppingDrift)
                {
                    time = 0;
                    startedDrift = false;
                    isDrifting = false;
                }
            }
        }

        // Start drifting if 
        if (startedDrift && !isDrifting)
        {
            // the drift is initiated and x seconds have passed
            driftTime += Time.deltaTime;
            if (driftTime >= timeBeforeStartingDrift) isDrifting = true;
        }
    }
    private void DoDrifting()
    {
        if (isDrifting)
        {
            if (!instantiated)
            {
                driftScoreObject = Instantiate(driftScorePrefab, mainCanvas.transform);
                instantiated = true;
            }
            DriftScore();
        }
        else if (driftScore > 0)
        {
            ApplyDirftScore();
            instantiated = false;
            driftScoreObject.GetComponent<DriftScoreAnimation>().StartAnimation();
        }
    }
    private void ApplyDirftScore()
    {
        score += driftScore;
        driftScore = 0;
        scoreDisplay.text = score.ToString();
    }
    private void DriftScore()
    {
        driftScore += scoreIncreaseForDrifting;
        driftScoreObject.text = driftScore.ToString();
    }
    #endregion

    #region ending
    public void ConvertTimeLeftToScore()
    {
        score += timer.GetSecondsLeft() * pointsPerSecondLeft;
        PlayerPrefs.SetInt("SecondsLeft", (int)timer.GetSecondsLeft());
        PlayerPrefs.SetInt("TimeLeftScore", (int)(timer.GetSecondsLeft() * pointsPerSecondLeft));
    }
    public void ConvertScoreToMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Mathf.FloorToInt(score / pointsPerMoney));
        PlayerPrefs.SetInt("Score", (int)score);
        PlayerPrefs.SetInt("ScoreToMoney", Mathf.FloorToInt(score / pointsPerMoney));
    }
    public void DisablePointsGaining()
    {
        PointsCanBeScored = false;
    }
    #endregion
}