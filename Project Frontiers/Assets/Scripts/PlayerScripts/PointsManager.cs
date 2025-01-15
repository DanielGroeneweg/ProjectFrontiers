using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PointsManager : MonoBehaviour
{
    // In Unity Inspector
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text driftScoreDisplay;
    [SerializeField] private float scoreIncreaseForDrifting;
    [SerializeField] private float driftAngleMinumim;

    // Not In Unity Inspector
    [HideInInspector] public bool isDrifting;

    // Internal
    private float score;
    private float driftScore;
    private float driftTime;
    private void FixedUpdate()
    {
        if (playerRB.angularVelocity.magnitude > driftAngleMinumim) isDrifting = true;
        else isDrifting = false;

        if (isDrifting) DriftScore();
        else if (driftScore > 0)
        {
            score += driftScore;
            driftScore = 0;
            scoreDisplay.text = score.ToString();
            driftScoreDisplay.gameObject.SetActive(false);
        }
    }

    private void DriftScore()
    {
        if (!driftScoreDisplay.gameObject.activeSelf) driftScoreDisplay.gameObject.SetActive(true);
        driftScore += scoreIncreaseForDrifting;
        driftScoreDisplay.text = driftScore.ToString();
    }
}
