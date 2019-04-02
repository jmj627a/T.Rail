using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StateBook : MonoBehaviour {

   public GameObject State_Book;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    public void Open_Book()
    {

        // 여기다가 애니메이션 추가
        State_Book.SetActive(true); 
    }
    public void Close_Book()
    {
        State_Book.SetActive(false);
    }

    public void Book_Next_Page()
    {
        
    }
    public void Book_Prev_Page()
    {

    }
}
