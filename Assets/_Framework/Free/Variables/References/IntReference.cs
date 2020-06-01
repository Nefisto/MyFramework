using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public int Value
    {
        get => UseConstant ? ConstantValue : Variable.Value; 
        set
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.Value = value;
        }
    }

    public static implicit operator int(IntReference reference)
        => reference.Value;
    
    public static IntReference operator --(IntReference reference)
    {
        reference.Value--;

        return reference;
    }

    public static IntReference operator ++(IntReference reference)
    {
        reference.Value++;

        return reference;
    }
}
