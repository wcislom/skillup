using System.Text;

namespace Shared.Core;

public abstract class DomainException : Exception
{
    public virtual string Code => GetCodeFromName();

    public DomainException(string message) : base(message)
    {
    }

    private string GetCodeFromName() => ConvertToSnakeCase(GetType().Name.Replace("Exception", ""));

    private string ConvertToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        { 
            return input;
        }

        StringBuilder result = new ();
        result.Append(char.ToLower(input[0]));

        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                result.Append('_');
                result.Append(char.ToLower(input[i]));
            }
            else
            {
                result.Append(input[i]);
            }
        }

        return result.ToString();
    }
}
