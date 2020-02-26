namespace cReg_WebApp.Models.ViewModels
{
    public class CommentsViewModel
    {
        public string studentName { get; set; }
        public string comments { get; set; }

        public CommentsViewModel(string name,string comments)
        {
            this.studentName = name;
            this.comments = comments;
        }
    }
}
