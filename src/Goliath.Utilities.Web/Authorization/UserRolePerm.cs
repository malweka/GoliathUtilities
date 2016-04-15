using System;
using Goliath.Models;

namespace Goliath.Authorization
{

    public interface IUserRolePerm
    {

         int ResourceId { get; set; }

         int RoleNumber { get; set; }

         PermActionType PermValue { get; set; }

         DateTime CreatedOn { get; set; }

         string CreatedBy { get; set; }

         DateTime? ModifiedOn { get; set; }

         string ModifiedBy { get; set; }

    }
}