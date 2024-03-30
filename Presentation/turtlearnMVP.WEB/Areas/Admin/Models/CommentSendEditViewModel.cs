namespace turtlearnMVP.WEB.Areas.Admin.Models;

public class CommentSendEditViewModel
{
    public int? Id { get; set; }
    public int ParentId { get; set; }//Session'a yapılan yorumlarda ParentId tutulacak.
    public int RecordId { get; set; }//Eğer sessionda kullanıyorsak sessionId,
                                     //course'da kullanıyorsak courseId
    public string Text { get; set; }

}
