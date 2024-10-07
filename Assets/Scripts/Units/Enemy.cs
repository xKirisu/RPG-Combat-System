using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Tactic
{
    Random, Offensive, Deffensive, Executive
}
public class Enemy : Unit
{
    [SerializeField] Tactic Tactic = Tactic.Random;

    public override IEnumerator TakeAction(List<Unit> queue, PanelManager panel)
    {
        panel.hideSpellPanel();

        return base.TakeAction(queue, panel);


    }
}
