namespace turtlearnMVP.WEB.Areas.Admin.Models;

public class UserRoleEditViewModel
{
    public int Id { get; set; }//Rolü güncellenecek kullanıcının id'si
    public string FirstName { get; set; }//Rolü güncellenecek kullanıcının ismi
    public string LastName { get; set; }//Rolü güncellenecek kullanıcının soyismi
    public IList<RoleEditViewModel> Roles { get; set; }

}
