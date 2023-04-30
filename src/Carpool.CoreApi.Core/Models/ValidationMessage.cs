namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Generic format for a validation error, listed in a ValidationMessages array inside an error message.
/// </summary>
public class ValidationMessage
{
    /// <summary>
    /// Internal error code for this validation
    /// </summary>
    /// <value>Internal error code for this validation</value>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// The name of the field that failed validation
    /// </summary>
    /// <value>The name of the field that failed validation</value>
    public string? Field { get; set; }

    /// <summary>
    /// Validation message for the field
    /// </summary>
    /// <value>Validation message for the field</value>
    public string? Message { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(ErrorCode, Field, Message);
    }

    public override bool Equals(object obj)
    {
#pragma warning disable CS8604
        return Equals(obj as ValidationMessage);
#pragma warning restore CS8604
    }

    public bool Equals(ValidationMessage validationMessage)
    {
        if (validationMessage == null)
        {
            return false;
        }

        return validationMessage.GetHashCode() == GetHashCode();
    }
}
