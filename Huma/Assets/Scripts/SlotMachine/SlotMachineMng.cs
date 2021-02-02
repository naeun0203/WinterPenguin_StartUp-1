using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMng : MonoBehaviour
{
    public GameObject SlotLobject;
    public Button Slot;
    public Sprite SkillSprite;
    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> SlotSprite = new List<Image>();
    }
    public DisplayItemSlot DisplayItemSlots;
    private void Start()
    {
        Slot.interactable = false;
    }
    public void SlotMachine()
    {
        StartCoroutine(StartSlot());
    }
    IEnumerator StartSlot()
    {
        for(int i = 0; i < 40; i++)
        {
            SlotLobject.transform.localPosition -= new Vector3(0, 50f, 0);
            if (SlotLobject.transform.localPosition.y < 50f)
            {
                SlotLobject.transform.localPosition += new Vector3(0, 300f, 0);
            }
            yield return null;
            Slot.interactable = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
