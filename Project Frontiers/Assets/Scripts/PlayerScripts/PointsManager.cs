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
    [SerializeField] private TMP_Text scorePrefab;
    [SerializeField] private Canvas mainCanvas;

    [Header("Drift Score Stats")]
    [SerializeField] private float scoreIncreaseForDrifting;
    [SerializeField] private float driftAngleMinumim;
    [SerializeField] private float timeBeforeStartingDrift;
    [SerializeField] private float timeBeforeStoppingDrift;

    [Header("Airtime Score Stats")]
    [SerializeField] private float scoreIncreaseForAirtime;
    [SerializeField] private float timeBeforeStartingAirtime;

    [Header("Ending Stats")]
    [SerializeField] private float pointsPerMoney;
    [SerializeField] private float pointsPerSecondLeft;

    // Not In Unity Inspector
    [HideInInspector] public bool isDrifting;

    // Internal
    // General
    private float score;
    private TMP_Text scoreObject;
    private bool PointsCanBeScored = true;
    private bool instantiated = false;

    // Drifting
    private float driftScore;
    private float driftTime;
    private bool startedDrift = false;
    private float time;

    // Airtime
    private float airTime;
    private float airtimeScore;
    private bool isInAirtime = false;
    private float airtimeTimer;
    #endregion

    private void FixedUpdate()
    {
        Debug.Log(carControl.isOnGround());

        if (PointsCanBeScored)
        {
            CheckForDrifting();
            CheckForAirtime();
        }

        else
        {
            if (driftScore > 0) ApplyDirftScore();
            if (airTime > 0) ApplyAirtimeScore();
        }
    }

    #region drift- & score-Calculations
    private void CheckForDrifting()
    {
        StopAndStartDrift();

        DoDrifting();        
    }
    private void StopAndStartDrift()
    {
        // Get the velocity, ignoring the Y Axis
        Vector3 velocity = playerRB.velocity;

        velocity *= Mathf.Sign(carControl.ForwardSpeed());

        velocity.y = 0;

        // Get the forward, ignoring the Y axis
        Vector3 forward = playerRB.transform.forward;
        forward.y = 0;
        forward.Normalize();

        // Calculate the angle from the car's velocity direction to the car's face direction
        float angle = Vector3.Angle(forward, (velocity + forward).normalized);

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
                time += 0.02f;

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
                scoreObject = Instantiate(scorePrefab, mainCanvas.transform);
                instantiated = true;
            }
            DriftScore();
        }
        else if (driftScore > 0)
        {
            ApplyDirftScore();
            instantiated = false;
            scoreObject.GetComponent<ScoreDisplayAnimation>().StartAnimation();
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
        scoreObject.text = "Drift: +" + driftScore.ToString();
    }
    #endregion

    #region airtime calulations
    private void CheckForAirtime()
    {
        if (!carControl.isOnGround())
        {
            if (!isInAirtime)
            {
                airtimeTimer += 0.02f;

                if (airtimeTimer < timeBeforeStartingAirtime)
                {
                    airtimeTimer = 0;
                    isInAirtime = true;
                }
            }

            if (isInAirtime)
            {
                if (!instantiated)
                {
                    instantiated = true;
                    scoreObject = Instantiate(scorePrefab, mainCanvas.transform);
                }
                airTime += 0.02f;
                airtimeScore = airTime * scoreIncreaseForAirtime;
                scoreObject.text = "Airtime: +" + ((int)airtimeScore).ToString();
            }
        }

        else if (airTime > 0)
        {
            ApplyAirtimeScore();
            airTime = 0;
            scoreObject.GetComponent<ScoreDisplayAnimation>().StartAnimation();
            instantiated = false;
        }
    }
    private void ApplyAirtimeScore()
    {
        airtimeScore = Mathf.Round(airtimeScore);
        score += airtimeScore;
        scoreDisplay.text = score.ToString();
        airtimeScore = 0;
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