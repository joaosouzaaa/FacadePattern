using FacadePattern.API.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FacadePattern.API.Filters;

public class NotificationFilter(INotificationHandler notificationHandler) : ActionFilterAttribute
{
    private readonly INotificationHandler _notificationHandler = notificationHandler;

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (_notificationHandler.HasNotifications())
            context.Result = new BadRequestObjectResult(_notificationHandler.GetNotifications());

        base.OnActionExecuted(context);
    }
}
