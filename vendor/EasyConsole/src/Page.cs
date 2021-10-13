using System;
using System.Linq;

namespace EasyConsole
{
    public abstract class Page
    {
        #region Public Constructors

        public Page(string title, Program program)
        {
            Title = title;
            Program = program;
        }

        #endregion Public Constructors

        #region Public Properties

        public Program Program { get; set; }
        public string Title { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public virtual void Display()
        {
            if (Program.History.Count > 1 && Program.BreadcrumbHeader)
            {
                string breadcrumb = null;
                foreach (var title in Program.History.Select((page) => page.Title).Reverse())
                    breadcrumb += title + " > ";
                breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);
                Console.WriteLine(breadcrumb);
            }
            else
            {
                Console.WriteLine(Title);
            }
            Console.WriteLine("---");
        }

        #endregion Public Methods
    }
}