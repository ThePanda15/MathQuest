using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Text uiTimer, uiProblem, input, error;
    public InputField field;
    public Animator[] animators;
    public RectTransform[] hpMeters;
    public GameObject quest, victory, defeat;

    // 0 = player, 1 = foe
    LivingBeing[] fighters = { new LivingBeing(20), new LivingBeing(20) };
    int currentTurn = 0;
    float timer = 30f;
    int[] problem = new int[3];

    private bool battleEnded {
        get
        {
            return (fighters[0].isDead || fighters[1].isDead);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NewTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!battleEnded)
        {
            if (Input.GetButtonDown("Submit"))
            {
                error.text = "";
                int answer;
                try
                {
                    answer = int.Parse(input.text);
                }
                catch (System.Exception)
                {
                    answer = -2121;
                    error.text = "Error: Please enter only integer numbers.";
                }
                
                if (SolveProblem() == answer)
                {
                    DamageTarget(1);
                } else
                {
                    DamageTarget(0);
                }
            } else if (timer <= 0)
            {
                DamageTarget(0);
            }
            timer -= Time.deltaTime;
            uiTimer.text = ""+(int)timer;
            uiProblem.text = StringifyProblem();
        }
        else if (fighters[0].isDead)
        {
            animators[0].SetTrigger("Die");
            defeat.SetActive(true);
            quest.SetActive(false);
        }
        else if (fighters[1].isDead)
        {
            animators[1].SetTrigger("Die");
            victory.SetActive(true);
            quest.SetActive(false);
        }
    }

    void NewTurn()
    {
        currentTurn++;
        timer = 32f - currentTurn;
        problem = new int[]{ Random.Range(1, 15), Random.Range(1, 15), Random.Range(1, 4) };
        field.Select();
        field.ActivateInputField();
    }

    void DamageTarget(int index)
    {
        int damage = Random.Range(2, 4);
        fighters[index].Damage(damage);

        PlayAnimations(index);
        UpdateHPMeter(index);
        NewTurn();
    }

    int SolveProblem()
    {
        switch (problem[2])
        {
            case 1:
                return problem[0] + problem[1];
            case 2:
                return problem[0] - problem[1];
            case 3:
                return problem[0] * problem[1];
        }
        return 0;
    }

    string StringifyProblem()
    {
        switch (problem[2])
        {
            case 1:
                return problem[0] + " + " + problem[1];
            case 2:
                return problem[0] + " - " + problem[1];
            case 3:
                return problem[0] + " x " + problem[1];
        }
        return "";
    }

    void PlayAnimations(int index)
    {
        if (index == 0) {
            animators[1].SetTrigger("Attack");
            animators[0].SetTrigger("Damage");
        } else if (index == 1)
        {
            int attk = Random.Range(1, 4);
            animators[0].SetTrigger("Attack"+attk);
            animators[1].SetTrigger("Damage");
        }
    }

    void UpdateHPMeter(int index)
    {
        int hp = fighters[index].hp;
        RectTransform meter = hpMeters[index];
        Image img = meter.gameObject.GetComponent<Image>();
        if (hp > 15)
        {
            img.color = new Color32(183, 255, 88, 255);
        } else if (hp > 8)
        {
            img.color = new Color32(237, 215, 71, 255);
        } else
        {
            img.color = new Color32(237, 71, 71, 255);
        }

        meter.sizeDelta = new Vector2(hp * 10, 10);
    }
}
