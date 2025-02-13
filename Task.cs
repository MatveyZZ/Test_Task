public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
    public string Status { get; set; } // "To do", "In Progress", "Done"

    public Task(int id, string title, string description, string assignedTo)
    {
        Id = id;
        Title = title;
        Description = description;
        AssignedTo = assignedTo;
        Status = "To do";
    }
}
