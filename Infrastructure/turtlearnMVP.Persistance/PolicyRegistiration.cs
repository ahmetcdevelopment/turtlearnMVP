using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace turtlearnMVP.Persistance
{
    public class PolicyRegistiration
    {
        public static void ConfigurePolicies(AuthorizationOptions options)
        {
            //otomatize edilebilir üzerine düşünülecek

            options.AddPolicy("EditorRolAdd", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        (c.Type == ClaimTypes.Role && c.Value == "Editor") &&
                        (c.Type == "Rol" && c.Value == "Add")
                    )
                )
            );

            
        }
    }
}
