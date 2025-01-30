using System.Collections.Generic;
using UnityEngine;
public class ProgressIndicator : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoints;
    [SerializeField] private GameObject[] images;
    [SerializeField] private GameObject carImage;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Finish") carImage.transform.position = images[images.Length - 1].transform.position;

        for (int i = 0; i <= checkpoints.Length - 1; i++)
        {
            if (collision.gameObject == checkpoints[i]) carImage.transform.position = images[i].transform.position;
        }
    }
    public void Respawn(Transform checkpoint)
    {
        for (int i = 0; i <= checkpoints.Length - 1; i++)
        {
            if (checkpoint.gameObject == checkpoints[i]) carImage.transform.position = images[i].transform.position;
        }
    }
}
