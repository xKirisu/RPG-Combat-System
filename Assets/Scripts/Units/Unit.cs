using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

enum AnimationStates
{
    Idle, Attack, Casting, Hurt, Die
}
public class Unit : MonoBehaviour
{
    //[Header("Objects")]
    Animator Animator;

    Unit Target;

    [Header("Icon")]
    [SerializeField] protected Sprite SPRITE;
    [Header("Statistics")]
    [SerializeField] protected short STR;
    [SerializeField] protected short DEX;
    [SerializeField] protected short INT;

    static short MaxArm = 400;
    [SerializeField] protected short END;
    [SerializeField] protected short SPR;

    short SPD_Dice20 = 0;
    [SerializeField] protected short SPD;
    [SerializeField] protected short LCK;

    [SerializeField] protected short FHP;
    protected short HP;


    [SerializeField] protected short FMP;
    protected short MP;

    [Header("Moves")]
    [SerializeField] protected Move[] Moves = new Move[4];

    static float UnitAwait = 0.75f;

    void Start()
    {
        Animator = GetComponent<Animator>();

        SPD_Dice20 = (short)Random.Range(1, 20);

        HP = FHP;
        MP = FMP;
    }
    public IEnumerator Hurt(int Damage)
    {
        yield return new WaitForSeconds(UnitAwait);
    }
    public virtual IEnumerator TakeAction(List<Unit> queue, PanelManager panel)
    {
        panel.setCharacterInfoPanel(SPRITE, HP, hp_persentage, MP, mp_persentage);

        panel.setTextInfo(this.name + " have a turn");

        yield return new WaitForSeconds(UnitAwait);
    }

    protected void ChangeAnimation(AnimationState state)
    {
        Animator.Play(state.name);
    }

    public short Spd {  get { return (short)(SPD + SPD_Dice20); } }
    public float ReductEnd { get { return (float)END/MaxArm; } }
    public float ReductSpr { get { return (float)SPR/MaxArm; } }
    public float hp_persentage { get { return (float)HP / FHP; } }
    public float mp_persentage { get { return (float)MP / FMP; } }
}
