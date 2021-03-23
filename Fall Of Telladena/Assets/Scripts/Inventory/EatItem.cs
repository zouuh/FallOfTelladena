using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItem : MonoBehaviour {

    public void eatItem(PlayerMovement playerMov) {
        Item item = Inventory.instance.usedItem;
        if(item != null) {
            item.eatItem(playerMov);
        }
    }
}
