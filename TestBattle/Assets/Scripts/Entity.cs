using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum State   //состояния
    {
        idle,
        run_forward,
        run_back,
        attack,
        hit,
        block,
    }

    Animator entityAnimator; //ссылка на аниматор

    public State currentState;  //текущее состояние
    public Enemy targetEnemy;   //цель для атаки
    public ParticleSystem hitParticle;
    public ParticleSystem blockParticle;

    public void Start()
    {
        currentState = State.idle;
        entityAnimator = GetComponent<Animator>();
    }

    public void SetState(int stateID)
    {
        switch(stateID) //задать состояние
        {
            case 0: //idle
                {
                    currentState = State.idle;
                    entityAnimator.SetInteger("StateID", 0);
                    break;
                }
            case 1: //run_forward
                {
                    currentState = State.run_forward;
                    entityAnimator.SetInteger("StateID", 1);
                    StartCoroutine(Move(transform.position + transform.forward * 5f));
                    break;
                }
            case 2: //run_back
                {
                    currentState = State.run_back;
                    entityAnimator.SetInteger("StateID", 2);
                    StartCoroutine(Move(transform.position - transform.forward * 5f));
                    break;
                }
            case 3: //attack
                {
                    currentState = State.attack;
                    entityAnimator.SetInteger("AttackID", Random.Range(0, 2));
                    entityAnimator.SetInteger("StateID", 3);
                    break;
                }
            case 4: //hit
                {
                    currentState = State.hit;
                    entityAnimator.SetInteger("StateID", 4);
                    hitParticle.Play();
                    break;
                }
            case 5: //block
                {
                    currentState = State.block;
                    entityAnimator.SetInteger("StateID", 5);
                    blockParticle.Play();
                    break;
                }
        }
    }

    IEnumerator Move(Vector3 point)     //передвижение при атаке
    {
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, 20f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void HitOrBlock()    //рандомный уворот или прием атаки
    {
        if (Random.Range(0, 2) == 0)
            targetEnemy.SetState(4);
        else
            targetEnemy.SetState(5);
    }
}
