using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DrahtenWeb.Attributes
{
    public class CheckTermsAndConditionsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var consentCookie = context.HttpContext.Request.Cookies["TermsAndConditionsConsent"];

            // Check if the user has not consented to terms and conditions.
            if (string.IsNullOrEmpty(consentCookie) || !bool.TryParse(consentCookie, out bool hasConsented) || !hasConsented)
            {
                // User has not consented or cookie doesn't exist, redirect to terms and conditions page.
                context.Result = new RedirectToActionResult("Index", "TermsAndConditions", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
