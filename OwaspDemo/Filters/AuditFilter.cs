﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using OwaspDemo.Data;
using OwaspDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwaspDemo.Filters
{
    public class AuditFilter : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _databaseContext;
        public AuditFilter(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string actionName = null;
                string controllerName = null;

                // Getting ActionName
                if (((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName != null)
                {
                    actionName =
                        ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor)
                        .ActionName;
                }
                // Getting ControllerName
                if (((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName != null)
                {
                    controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
                }

                // Assigning values to AuditTb Class
                var objaudit = new AuditLog
                {
                    UserId = context.HttpContext.Session.GetInt32("UserID") ?? 0,
                    SessionId = context.HttpContext.Session.Id,
                    IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    PageAccessed = context.HttpContext.Request.GetDisplayUrl(),
                    LoggedInAt = DateTime.Now,
                    Method = context.HttpContext.Request.Method
                };

                if (actionName == "Logout")
                {
                    objaudit.LoggedOutAt = DateTime.Now;
                }

                objaudit.LoginStatus = "A";
                objaudit.ControllerName = controllerName;
                objaudit.ActionName = actionName;

                _databaseContext.AuditLogs.Add(objaudit);
                _databaseContext.SaveChanges();

                base.OnActionExecuting(context);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
