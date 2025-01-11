using System;

namespace GameOfVlad.Utils;

public struct Range<TObj> where TObj: IComparable<TObj>
{
    public TObj MinValue { get; }
    public TObj MaxValue { get; }
    
    public Range(TObj minValue, TObj maxValue)
    {
        if (minValue.CompareTo(maxValue) > 0)
        {
            throw new ArgumentException("The min value must be greater than the max value.", nameof(maxValue));
        }
        
        this.MinValue = minValue;
        this.MaxValue = maxValue;
    }
    
    public static Range<TObj> Create(TObj minValue, TObj maxValue) => new(minValue, maxValue);
}