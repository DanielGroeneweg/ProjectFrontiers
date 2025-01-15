using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PointsManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text driftScoreDisplay;
    [SerializeField] private float scoreIncreaseForDrifting;
    private float score;
    private float driftScore;
    private bool isDrifting;
    private float driftTime;
    private void FixedUpdate()
    {
        if (playerRB.angularVelocity.magnitude > 0.01f) isDrifting = true;
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
        if (!driftScoreDisplay.gameObject.active) driftScoreDisplay.gameObject.SetActive(true);
        driftScore += scoreIncreaseForDrifting;
        driftScoreDisplay.text = driftScore.ToString();
    }
}
