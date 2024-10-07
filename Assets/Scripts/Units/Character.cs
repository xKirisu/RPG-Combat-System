using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    Move MOVE;
    static bool ButtonPressedFlag = false;
    public override IEnumerator TakeAction(List<Unit> queue, PanelManager panel)
    {
        panel.showSpellPanel(Moves, this);

        yield return base.TakeAction(queue, panel);

        ButtonPressedFlag = false;

        yield return new WaitUntil(() => ButtonPressedFlag);

        //turn off panel
        //cast
    }

    public void ButtonPress(int index)
    {
        MOVE = Moves[index];
        ButtonPressedFlag = true;
    }
}
