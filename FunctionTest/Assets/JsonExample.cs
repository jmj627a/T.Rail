using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JTestClass
{
    public int i;
    public float f;
    public bool b;

    public Vector3 v;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();


    public JTestClass() { }


    public JTestClass(bool isSet)
    {

        if (isSet)

        {
            i = 10;
            f = 99.9f;
            b = true;

            v = new Vector3(39.56f, 21.2f, 6.4f);
            str = "JSON Test String";
            iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };





            for (int idx = 0; idx < 5; idx++)
            {
                iList.Add(2 * idx);
            }
        }
    }


    public void Print()
    {
        Debug.Log("i = " + i);
        Debug.Log("f = " + f);
        Debug.Log("b = " + b);

        Debug.Log("v = " + v);
        Debug.Log("str = " + str);

        for (int idx = 0; idx < iArray.Length; idx++)
        {
            Debug.Log(string.Format("iArray[{0}] = {1}", idx, iArray[idx]));
        }

        for (int idx = 0; idx < iList.Count; idx++)
        {
            Debug.Log(string.Format("iList[{0}] = {1}", idx, iList[idx]));
        }
    }

}


public class JsonExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //JTestClass jtc = new JTestClass(true);
        //string jsonData = ObjectToJson(jtc);
        //Debug.Log(jsonData);

        //var jtc2 = JsonToOject<JTestClass>(jsonData);
        //jtc2.Print();


        JTestClass jtc = new JTestClass(true);
        string jsonData = ObjectToJson(jtc);
      //  CreateJsonFile(Application.dataPath, "JTestClass", jsonData);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    string ObjectToJson(object obj)
    {
        // JsonUtility 클래스의 ToJson() 함수를 이용해서 오브젝트를 문자열로 된 json 데이터로 변환하여 
        // 반환하는 처리를 함
        return JsonUtility.ToJson(obj);
    }

    T JsonToOject<T>(string jsonData)
    {
        // fromjson()함수를 이용해서 문자열로 된 json데이터를 받아서 원하는 타입의 객체로 반환하는 처리
        return JsonUtility.FromJson<T>(jsonData);
    }

    // 문자열로 만든 json 데이터를 파일로 저장하는 코드의 예시
   /* void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    */

    // 방금 저장한 Json 파일을 읽어들여서 오브젝트로 변환하는 코드
  //  T LoadJsonFile<T>(string loadPath, string fileName)
   // {
        //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        //byte[] data = new byte[fileStream.Length];
        //fileStream.Read(data, 0, data.Length);
        //fileStream.Close();
        //string jsonData = Encoding.UTF8.GetString(data);
        //return JsonUtility.FromJson<T>(jsonData);
  //  }

}
