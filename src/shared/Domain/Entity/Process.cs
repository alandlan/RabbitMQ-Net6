namespace Domain.Entity
{
    public class Process
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ProcessAt { get; set; }

        public Process()
        {
        }

        public Process(string name)
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
            Name = name;
        }

        public override string ToString()
        {
            return "{ Id: " + Id + ", Name: " + Name + ", CreateAt: " + CreateAt + ", ProcessAt: " + ProcessAt + " }";
        }
    }
}
