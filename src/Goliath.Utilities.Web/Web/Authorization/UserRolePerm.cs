using System;
using Goliath.Models;

namespace Goliath.Web.Authorization
{

    public interface IUserRolePerm
    {

         int ResourceId { get; set; }

         int RoleNumber { get; set; }

         int PermValue { get; set; }

         DateTime CreatedOn { get; set; }

         string CreatedBy { get; set; }

         DateTime? ModifiedOn { get; set; }

         string ModifiedBy { get; set; }

    }
}