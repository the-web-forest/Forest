using System.ComponentModel.DataAnnotations;

public class StringLengthListAttribute : ValidationAttribute
{
    private int MinLength { get; }
    private int MaxLength { get; }

    public StringLengthListAttribute(int minimumLength, int maximumLength) : base() {
        MinLength = minimumLength;
        MaxLength = maximumLength;
    }

    public override bool IsValid(object value)
    {
        var List = value as List<string>;

        foreach (var Item in List)
        {
            if(Item.Length > MaxLength || Item.Length < MinLength)
            {
                return false;
            }
        }

        return true;
    }
}