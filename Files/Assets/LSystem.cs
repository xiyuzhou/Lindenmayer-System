using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystem : MonoBehaviour
{
    [SerializeField] private int iteration = 4;
    [SerializeField] private GameObject Branch;
    [SerializeField] private float length = 10f;
    [SerializeField] private float angle = 30f;
    private List<GameObject> treeSegments = new List<GameObject>();

    private const string axiom = "X";

    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;
    private string currentString = string.Empty;

    public int ruleNum = 0;
    void Start()
    {
        transformStack = new Stack<TransformInfo>();
        rules = new Dictionary<char, string>
        {
            //{'X',"[F-[[X]+X]+F[+FX]-X]"},
            {'X',"[FX][-FX][+FX]"},
            {'F',"FF"}
        };

        Generate();
    }
    public void ChangeAngle(float value)
    {
        angle = value;
        ReGenerate();
    }
    public void ChangeIteration(float value)
    {
        iteration = (int)value;
        ReGenerate();
    }
    public void DropDownChange(int num)
    {
        ruleNum = num;
        ReGenerate();
    }
    public void ReGenerate()
    {
        foreach (GameObject treeSegment in treeSegments)
        {
            Destroy(treeSegment);
        }
        treeSegments.Clear();

        string ruleString = "";
        transformStack = new Stack<TransformInfo>();

        switch (ruleNum)
        {
            case 0:
                ruleString = "[FX][-FX][+FX]";
                break;
            case 1:
                ruleString = "[F-[[X]+X]+F[+FX]-X]";
                break;
            case 2:
                ruleString = "[-FX]X[+FX][+F-FX]";
                break;
            case 3:
                ruleString = "[FF[+XF-F+FX]--F+F-FX]";
                break;
            case 4:
                ruleString = "[FX[+F[-FX]FX][-F-FXFX]]";
                break;
            case 5:
                ruleString = "[F[+FX][*+FX][/+FX]]";
                break;
            case 6:
                ruleString = "[*+FX]X[+FX][/+F-FX]";
                break;
            case 7:
                ruleString = "[F[-X+F[+FX]][*-X+F[+FX]][/-X+F[+FX]-X]]";
                break;
        }

         
        rules = new Dictionary<char, string>
        {
            {'X',ruleString},
            {'F',"FF"}
        };

        Generate();
    }
    private void Generate()
    {
        currentString = axiom;

        StringBuilder sb = new StringBuilder();


        for (int i = 0; i < iteration; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }
            currentString = sb.ToString();
            sb = new StringBuilder();
        }


        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    GameObject treeSegement = Instantiate(Branch);
                    treeSegments.Add(treeSegement);
                    treeSegement.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    treeSegement.GetComponent<LineRenderer>().SetPosition(1, transform.position);

                    break;
                case 'X':
                    break;

                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;
                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '*':
                    transform.Rotate(Vector3.up * 120);
                    break;
                case '/':
                    transform.Rotate(Vector3.down * 120);
                    break;
                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;
                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;
                default:
                    throw new InvalidOperationException("invalid L-Tree operation");
            }
        }
    }
    
}
