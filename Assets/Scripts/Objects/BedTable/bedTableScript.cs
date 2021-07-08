using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedTableScript : MonoBehaviour
{
    private Animator animator;
    private bool cupboard;
    private bool draw;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cupboard = true;
        draw = true;
    }

    private void Cupboard()
    {
        //player interacted with cupboard
        if (cupboard)
        {
            //if cupboard open, play close animation
            animator.Play("CloseCupboard");
        }
        else
        {
            animator.Play("OpenCupboard");
        }
    }

    private void Draw()
    {
        //player interacted with draw
        if (draw)
        {
            //if draw open, play close animation
            animator.Play("CloseDraw");
        }
        else
        {
            animator.Play("OpenDraw");
        }
    }
}
