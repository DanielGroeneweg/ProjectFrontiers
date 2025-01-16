using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PointsManager : MonoBehaviour
{
    // In Unity Inspector
    [Header("Required Components")]
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text driftScoreDisplay;
    [SerializeField] private PlayTimer timer;

    [Header("Stats")]
    [SerializeField] private float scoreIncreaseForDrifting;
    [SerializeField] private float driftAngleMinumim;
    [SerializeField] private float pointsPerMoney;
    [SerializeField] private float pointsPerSecondLeft;

    // Not In Unity Inspector
    [HideInInspector] public bool isDrifting;

    // Internal
    private float score;
    private float driftScore;
    private float driftTime;
    private bool PointsCanBeScored = true;
    private void FixedUpdate()
    {
        if (PointsCanBeScored)
        {
            if (playerRB.angularVelocity.magnitude > driftAngleMinumim) isDrifting = true;
            else isDrifting = false;

            if (isDrifting) DriftScore();
            else if (driftScore > 0) ApplyDirftScore();
        }

        else if (driftScore > 0) ApplyDirftScore();
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
    }
    public void ConvertScoreToMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Mathf.FloorToInt(score / pointsPerMoney));
    }
    public void DisablePointsGaining()
    {
        PointsCanBeScored = false;
    }
}