using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPattern.API.Policies
{
    /// <summary>
    /// Class to set policies for user Login.
    /// </summary>
    public class Policies
    {
        /// <summary>
        /// If user is Admin
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// If user is Guest
        /// </summary>
        public const string Guest = "Guest";
        /// <summary>
        /// 
        /// </summary>
        public const string Admin_Guest = "Admin_Guest";
        /// <summary>
        /// Role is Admin
        /// </summary>
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        /// <summary>
        /// Role is Guest
        /// </summary>
        public static AuthorizationPolicy GuestPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Guest).Build();
        }
    }
}
