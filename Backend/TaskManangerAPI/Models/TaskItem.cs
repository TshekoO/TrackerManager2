namespace TaskManangerAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }//Unique identifier for the task
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";//Piority level
        public bool IsCompleted { get; set; } = false;//Indicates if the task is completed
    }
}
