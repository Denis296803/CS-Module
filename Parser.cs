using System;
using System.Collections;


public class Operation
{
    public Operation a;
    public Operation b;
    public Operator oper;
    public double? constant = null;
    public Operation(Operation a, Operation b, Operator oper)
    {
        this.a = a; this.b = b; this.oper = oper;
    }
}
public abstract class Operator
{
    public string oper = "";
    public int? priority = 0;
    public bool is_unary = false;
    public virtual double F1(double? a)
    {
        return 0;
    }
    public virtual double F2(double? a,double? b)
    {
        return 0;
    }
}
public class Calc
{
    public static Operation Parse(string str,Operator[] operators)
    {
        Operation op = new Operation(null, null, null);
        if (str.Length == 0) { op.constant = 0f; return op; }
        while (true)
        {
            if (str[0] == '(' && str[str.Length - 1] == ')')
            {
                string temp = str.Substring(1, str.Length - 2);
                int c = 0; bool change = false;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '(') { c++; change = true; }
                    else if (temp[i] == ')')
                    {
                        c--;
                        change = true;
                        if (c < 0) break;
                    }
                }
                if (c >= 0)
                    str = temp;
                else
                    break;
            }
            else
                break;
        }
        List<int[]> priority = new List<int[]>();
        for (int oper = 0; oper < operators.Length; oper++)
        {
            for (int index = 0; index < str.Length - operators[oper].oper.Length; index++)
            {
                if (str[index..(index + operators[oper].oper.Length)] == operators[oper].oper)
                {
                    int c = 0;
                    for (int i = 0; i < index; i++)
                    {
                        if (str[i] == '(') c++;
                        if (str[i] == ')') c--;
                    }
                    priority.Add(new int[] { index, oper, c });
                }
            }
        }
        if (priority.Count > 0)
        {
            int bestIdx = 0;
            int bestDepth = int.MaxValue;
            int bestPrio = int.MaxValue;
            int bestPos = int.MaxValue;
            int ik = 0;

            for (int i = 0; i < priority.Count; i++)
            {
                int depth = priority[i][2];
                int prio = priority[i][1];
                int pos = priority[i][0];

                if (depth < bestDepth || (depth == bestDepth && (prio < bestPrio || (prio == bestPrio && pos < bestPos))))
                {
                    bestDepth = depth;
                    bestPrio = prio;
                    bestPos = pos;
                    bestIdx = i;
                    ik = priority[bestIdx][0];
                }
            }
            op.oper = operators[priority[bestIdx][1]];
            if (op.oper.is_unary)
                op.a = null;
            else
                op.a = Parse(str.Substring(0, ik), operators);

            op.b = Parse(str.Substring(ik + op.oper.oper.Length), operators);
            return op;
        }
        else
        {
            op.constant = Convert.ToSingle(str);
            return op;
        }
    }
    public static double? Decode(Operation tree,Operator[] operators)
    {
        if (tree == null) return null;
        if (tree.constant == null)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (operators[i].oper == tree.oper.oper)
                {
                    if(tree.oper.is_unary){
                        return tree.oper.F1(Decode(tree.b,operators));
                    }
                    else if(!tree.oper.is_unary){
                        return tree.oper.F2(Decode(tree.a,operators),Decode(tree.b, operators));
                    }
                }
            }
        }
        else
        {
            return tree.constant;
        }

        return null;
    }
}