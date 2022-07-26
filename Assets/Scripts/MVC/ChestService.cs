using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class ChestService : MonoGenericSingleton<ChestService>
{
    private ChestController[] chestSlots;
    private ChestView chestToUnlock;

    public GameObject chestSlotGroup;
    public int noOfChestCanUnlock;
}
