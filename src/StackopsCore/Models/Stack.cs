namespace StackopsCore.Models
{
    public class Stack
    {
        public string Name { get; set; }

        public string[] Ec2InstanceIds { get; set; }
    }
}