using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    private Slot[] slots;
    private int _maxSlotNum;
    private int _selectSlotNum;
    public int ItemCount = 0;
    public event UnityAction<GameObject> BookEvent;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        _maxSlotNum = slots.Length;
        slots[0].SelectSlot();
    }

    public bool AcquireItem(Item _item)
    {
        if (_item.itemData.itemtype == ItemData.ItemType.book)
        {
            BookEvent.Invoke(transform.parent.parent.gameObject);
            return true;
        }

        if (ItemCount <= _maxSlotNum)
        {
            for (int i = 0; i < _maxSlotNum; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item);
                    ++ItemCount;
                    return true;
                }
            }
        }
        return false;
    }
    public void SelectSlot(int slotNum)
    {
        _selectSlotNum = slotNum;
        slots[_selectSlotNum].SelectSlot();
    }
    public bool Useitem()
    {
        if (slots[_selectSlotNum] != null)
        {
            if (slots[_selectSlotNum].UseItem())
            {
                --ItemCount;
                return true;
            }
        }
        return false;
    }


}

