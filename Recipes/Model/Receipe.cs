using Recipes.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Model {
    public class Receipe : NotifyableObject {
        private int id;
        [Key]
        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string title;
        public string Title {
            get { return title; }
            set { title = value; base.OnChanged(); }
        }

        private TimeSpan prepareTime;
        public TimeSpan PrepareTime {
            get { return prepareTime; }
            set { prepareTime = value; base.OnChanged(); }
        }

        private string description;
        public string Description {
            get { return description; }
            set { description = value; base.OnChanged(); }
        }

        private string note;
        public string Note {
            get { return note; }
            set { note = value; base.OnChanged(); }
        }
    }
}