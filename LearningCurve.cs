using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    int a = 5;
    int b = 7;

    List<string> QuestPartyMembers = new List<string>()
        {
            "Grim the Barbarian",
            "Merlin the Wise",
            "Sterling the Knight"
        };
    void Start()
    {
        AddPartyMember("Oscar Bagger", 1);
        FindPartyMembers();
        //Debug.Log(a + "," + b);
        //Debug.Log(Swap(a, b));
        Debug.Log(CalculateMax(a,b));
    }
    public void FindPartyMembers()
    {

        int listLength = QuestPartyMembers.Count;

        for (int i = 0; i < listLength; i++)
        {
            Debug.Log(QuestPartyMembers[i]);
        }
    }
    public void AddPartyMember(string name, int myIndex)
    {
        QuestPartyMembers.Insert(myIndex, name);
    }
    private (int,int) Swap(int m, int n)
    {
        int c = m;
        m = n;
        n = c;
        return (m, n);
    }
    private int CalculateMax(int m, int n)
    {
        if (m>n)
        {
            return m;
        } else
        {
            return n;
        }
    }

}
