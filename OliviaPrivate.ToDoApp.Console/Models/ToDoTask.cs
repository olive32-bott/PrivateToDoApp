namespace OliviaPrivate.ToDoApp.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public User? AssignedTo { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
