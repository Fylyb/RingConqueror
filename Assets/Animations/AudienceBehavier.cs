using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceBehavier : MonoBehaviour
{
    public Animator audienceAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhichAnimation(int value)
    {
        switch (value)
        {
            case 1:
                audienceAnimator.Play("applause");
                break;
            case 2:
                audienceAnimator.Play("applause2");
                break;
            case 3:
                audienceAnimator.Play("celebration");
                break;
            case 4:
                audienceAnimator.Play("celebration2");
                break;
            case 5:
                audienceAnimator.Play("celebration3");
                break;
            case 6:
                audienceAnimator.Play("idle");
                break;
            default:
                break;
        }
    }
}
