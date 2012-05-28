using System.Collections.Generic;

namespace StoryPrinter.Models
{
    public class TaskViewModel
    {
        //public TaskViewModel(IList<string> args)
        //{
        //    if (string.IsNullOrEmpty(args[0])) return;
        //    Name = args[0];
        //    Status = args[1];
        //    Id = int.Parse(args[3]);
        //    Sprint = args[5].Replace("Sprint ", "");
        //}

        public TaskViewModel(int id, string sprint, string name)
        {
            Id = id;
            Sprint = sprint;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { set; get; }
        //public string Status { get; set; }
        public string Sprint { get; set; }
    }
}