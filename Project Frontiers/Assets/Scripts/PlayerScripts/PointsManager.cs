using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PointsManager : MonoBehaviour
{
    // In Unity Inspector
    [Header("Required Components")]
    [SerializeField] private CarControl carControl;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text driftScoreDisplay;
    [SerializeField] private PlayTimer timer;

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
    private void FixedUpdate()
    {
        if (PointsCanBeScored)
        {
            CheckForDrifting();
        }

        else if (driftScore > 0) ApplyDirftScore();
    }
    private void CheckForDrifting()
    {
        // If the car is sideways, start drifting
        if (GetAngularVelocity() > driftAngleMinumim && carControl.isOnGround())
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

        // Do drifting
        if (isDrifting) DriftScore();
        else if (driftScore > 0) ApplyDirftScore();
    }
    private float GetAngularVelocity()
    {
        Vector3 velocity = new Vector3(
            0,
            playerRB.velocity.y,
            0);

        return velocity.magnitude;
    }
    private void ApplyDirftScore()
    {
        score += driftScore;
        driftScore = 0;
        scoreDisplay.text = score.ToString();
        driftScoreDisplay.gameObject.SetActive(false);
    }
    private void DriftScore()
    {
        if (!driftScoreDisplay.gameObject.activeSelf) driftScoreDisplay.gameObject.SetActive(true);
        driftScore += scoreIncreaseForDrifting;
        driftScoreDisplay.text = driftScore.ToString();
    }
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
}