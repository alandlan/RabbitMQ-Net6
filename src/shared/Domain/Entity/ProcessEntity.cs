namespace Domain.Entity
{
    public class ProcessEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ProcessAt { get; set; }

        public ProcessEntity()
        {
        }

        public ProcessEntity(string name)
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
