namespace MVCBlog.Core.Commands
{
    public class UpdateCommand<T> where T : Entities.EntityBase
    {
        public T Entity { get; set; }
    }
}
