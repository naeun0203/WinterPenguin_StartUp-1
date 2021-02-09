using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotControl : MonoBehaviour
{
    public int RandomReward = 1;

    public int StopValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private void Start()
    {
        rowStopped = true;
    }
    public void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;
        for(int i = 0; i < 30; i++)
        {

            if (transform.localPosition.y <= 0f)
                transform.localPosition = new Vector3(0, 900f, 0);

            transform.localPosition -= new Vector3(0, 50f, 0);

            yield return new WaitForSeconds(timeInterval);
        }

        switch (RandomReward)
        {
            case 1:
                StopValue = 60;
                break;
            case 2:
                StopValue = 63;
                break;
            case 3:
                StopValue = 66;
                break;
            case 4:
                StopValue = 69;
                break;
            case 5:
                StopValue = 72;
                break;
            case 6:
                StopValue = 75;
                break;
        }

        for(int i = 0; i < StopValue; i++)
        {
            if(transform.localPosition.y <= 0f)
                transform.localPosition = new Vector3(0, 900f, 0);

            transform.localPosition -= new Vector3(0, 50f, 0);

            if (i > Mathf.RoundToInt(StopValue * 0.5f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(StopValue * 1f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(StopValue * 2f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(StopValue * 2.5f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        rowStopped = true;
    }
}
