using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StateBook : MonoBehaviour {

    public Canvas this_canvas;

   public GameObject State_Book;

    Animator animat;
	// Use this for initialization
	void Start () {
        animat = this_canvas.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    public void Open_Book()
    {
        State_Book.SetActive(true); 
        animat.SetBool("state_book", true);
        // 여기다가 애니메이션 추가
       
    }
    public void Close_Book()
    {
        animat.SetBool("state_book", false);
        State_Book.SetActive(false);
    }

    public void Book_Next_Page()
    {
        
    }
    public void Book_Prev_Page()
    {

    }
}
