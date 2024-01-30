namespace DrahtenWeb.Exceptions
{
    public class ClaimsPrincipalNameIdentifierNotFoundException : ApplicationException
    {
        public ClaimsPrincipalNameIdentifierNotFoundException() : base("No NameIdentifier claim type was found in ClaimsPrincipal.")
        {
                
        }
    }
}
