using System;
namespace StackopsCore.Models
{
    public class StackActionRequest
    {
        public string[] Stacks { get; set; }

        public string Action { get; set; } 
    }
}