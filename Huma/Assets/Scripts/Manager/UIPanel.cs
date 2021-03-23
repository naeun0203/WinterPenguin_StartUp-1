using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    //public bool isOpen = false;

    public virtual void OpenBehavior() 
    {
        gameObject.SetActive(true);
        /*        if (!isOpen)
                {
                    isOpen = true;
                    gameObject.SetActive(true);
                }*/
    }

    public virtual void UpdateBehavior()
    {

    }
    public virtual void CloseBehavior()
    {
        gameObject.SetActive(false);
/*        if (isOpen)
        {
            isOpen = false;
            gameObject.SetActive(false);
        }*/
    }
}
