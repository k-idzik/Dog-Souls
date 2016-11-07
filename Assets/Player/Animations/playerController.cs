using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
	
	private Animator animator;
    private float prevVertical  = 0;
    private float prevHorizontal = 0;
	
	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
        AnimationControl();
	}

    void AnimationControl()
    {
        //checking if joysticks are connected
        if (Input.GetJoystickNames()[0] == "")
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (vertical > .1)
            {
                animator.SetInteger("Direction", 3);
            }
            else if (vertical < -.01)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (horizontal > .01)
            {
                animator.SetInteger("Direction", 2);
            }
            else if (horizontal < -.01)
            {
                animator.SetInteger("Direction", 4);
            }
            else
            {
                animator.SetInteger("Direction", 0);
            }
        }
        else
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            if(Mathf.Abs(prevVertical) > Mathf.Abs(vertical) && Mathf.Abs(prevHorizontal) > Mathf.Abs(horizontal))
            {
                animator.SetInteger("Direction", 0);
            }
            else if (vertical > .1)
            {
                animator.SetInteger("Direction", 3);
            }
            else if (vertical < -.01)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (horizontal > .01)
            {
                animator.SetInteger("Direction", 2);
            }
            else if (horizontal < -.01)
            {
                animator.SetInteger("Direction", 4);
            }
            prevHorizontal = horizontal;
            prevVertical = vertical;
        }        
    }
}
