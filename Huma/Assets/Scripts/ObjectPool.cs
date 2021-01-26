using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private Poolable poolObj;
    [SerializeField]
    private int spitcateCount;

    private Stack<Poolable> poolStack = new Stack<Poolable>();
    void Start()
    {
        Spitcate();
    }

    public void Spitcate()
    {
        for (int i = 0; i < spitcateCount; i++)
        {
            Poolable spitcateObj = Instantiate(poolObj, this.gameObject.transform);
            spitcateObj.Create(this);
            poolStack.Push(spitcateObj);
        }

    }

    public GameObject Pop()
    {
        Poolable obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }

    public void Push(Poolable obj)
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }
}
