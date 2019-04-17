using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MyGameObject
{

    public GameObject selfobject;

    public List<MyGameObject> childobjects;

    public Dictionary<string, MyGameObject> childsdic;

    private bool root = false;


    public void est_root()
    {
        root = true;
    }

    public MyGameObject getChildsDic(string temp_path)
    {
        MyGameObject temp_mygameobject = this;

        string[] stArrayData = temp_path.Split('/');


        foreach (string temp_name in stArrayData)
        {
            if (temp_mygameobject.getChildDic(temp_name) != null)
            {
                temp_mygameobject = temp_mygameobject.getChildDic(temp_name);
            }
            else
            {
                temp_mygameobject = null;
                break;
            }
        }

        return temp_mygameobject;



    }

    public MyGameObject getChildDic(string temp_name)
    {
        if (childsdic.ContainsKey(temp_name))
        {
            return childsdic[temp_name];
        }
        else
        {
            return null;
        }

    }

    public void saiki()
    {
        if (root)
        {
            GameObject[] temp_a;
            temp_a = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            System.Array.Sort(temp_a, (obj1, obj2) => obj1.transform.GetSiblingIndex().CompareTo(obj2.transform.GetSiblingIndex()));

            List<Transform> lst = new List<Transform>();


            foreach (GameObject obj in temp_a)
            {

                if (!obj.transform.parent)
                {
                    lst.Add(obj.transform);
                }

            }


            if (lst.Count > 0)
            {

                childobjects = new List<MyGameObject>();

                childsdic = new Dictionary<string, MyGameObject>();



                MyGameObject temp_mygameobject;
                foreach (Transform temp_transform in lst)
                {

                    temp_mygameobject = new MyGameObject();
                    temp_mygameobject.selfobject = temp_transform.gameObject;
                    childobjects.Add(temp_mygameobject);

                    childsdic.Add(temp_mygameobject.selfobject.name, temp_mygameobject);

                    temp_mygameobject.saiki();

                }
            }
        }



        else if (selfobject.transform.childCount > 0)
        {

            childobjects = new List<MyGameObject>();
            childsdic = new Dictionary<string, MyGameObject>();

            MyGameObject temp_mygameobject;
            foreach (Transform temp_transform in selfobject.transform)
            {

                temp_mygameobject = new MyGameObject();
                temp_mygameobject.selfobject = temp_transform.gameObject;
                childobjects.Add(temp_mygameobject);

                childsdic.Add(temp_mygameobject.selfobject.name, temp_mygameobject);

                temp_mygameobject.saiki();

            }

        }

    }

}
