/* 
 * Authors : Zoé 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryData {
    //  Attributes
    public int inCrystalRoom;
    public int builtIrrigation;
    public bool possiIsBack;
    public bool inBimbopCave;
    public bool beginStoneQuests;
    public int mainQuestAdvencement;
    public int serenityQuestAdvencement;

    // How to save it from Player class
    public StoryData (StoryManager story) {
        inCrystalRoom = story.inCrystalRoom;
        builtIrrigation = story.builtIrrigation;
        possiIsBack = story.possiIsBack;
        inBimbopCave = story.inBimbopCave;
        beginStoneQuests = story.beginStoneQuests;
        mainQuestAdvencement = story.mainQuestAdvencement;
        serenityQuestAdvencement = story.serenityQuestAdvencement;
    }
}
