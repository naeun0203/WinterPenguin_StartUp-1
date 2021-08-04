using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RnadomReward : MonoBehaviour
{
    public int[] tabel =
    {
        1,
        8,
        2,
        2,
        2,
        17,
        17,
        17,
        17,
        17
    };
    /// <summary>
    /// 
    /// </summary>
    public void randomSelect()
    {
        int number = Random.Range(0, 100);
        if (number>=0&& number < 1)
        {

        }
        if (number >= 1 && number < 8)
        {

        }
        if (number >= 8 && number < 10)
        {

        }
        if (number >= 10 && number < 12)
        {

        }
        if (number >= 12 && number < 14)
        {

        }
        if (number >= 14 && number < 31)
        {

        }
        if (number >= 31 && number < 48)
        {


        }
        if (number >= 48 && number < 65)
        {

        }
        if (number >= 65 && number < 82)
        {

        }
        if (number >= 82 && number < 100)
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
