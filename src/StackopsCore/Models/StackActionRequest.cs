﻿namespace StackopsCore.Models
{
    public class StackActionRequest
    {
        public string StackName { get; }

        public string Action { get; }

        public StackActionRequest(string stackName, string action)
        {
            StackName = stackName;
            Action = action;
        }
    }
}