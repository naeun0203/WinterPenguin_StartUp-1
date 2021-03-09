using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotControl : MonoBehaviour
{
    public RandomReward randomReward;
    public GameObject Slot;
    public GameObject SlotObject;
    private Transform SlotObj;

    public int StopValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private void Start()
    {
        SlotObj = SlotObject.transform;
        rowStopped = true;
    }
    public void StartRotating()
    {
        SlotObj.localPosition = new Vector3(0, 0, 0);
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;
        for(int i = 0; i < (10*2)*3; i++)
        {

            if (SlotObj.localPosition.y <= 0f)
                SlotObj.localPosition = new Vector3(0, 1050f, 0);

            SlotObj.localPosition -= new Vector3(0, 50f, 0);

            yield return new WaitForSeconds(timeInterval);
        }

        /*        StopValue = Random.Range(60, 100);
                switch (StopValue % 3)
                {
                    case 1:
                        StopValue += 2;
                        break;
                    case 2:
                        StopValue += 1;
                        break;
                }*/
        switch (randomReward.ResultCase)
        {
            case 1:
                StopValue = 66;
                break;
            case 2:
                StopValue = 69;
                break;
            case 3:
                StopValue = 72;
                break;
            case 4:
                StopValue = 75;
                break;
            case 5:
                StopValue = 78;
                break;
            case 6:
                StopValue = 81;
                break;
            case 7:
                StopValue = 84;
                break;
/*            case 8:
                StopValue = 87;
                break;
            case 9:
                StopValue = 84;
                break;
            case 10:
                StopValue = 87;
                break;*/
        }

        for (int i = 0; i < StopValue; i++)
        {
            if(SlotObj.localPosition.y <= 0f)
                SlotObj.localPosition = new Vector3(0, 1050f, 0);

            SlotObj.localPosition -= new Vector3(0, 50f, 0);

            if (i > Mathf.RoundToInt(StopValue * 0.5f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(StopValue * 1f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(StopValue * 1.5f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(StopValue * 2f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        rowStopped = true;
    }
}
