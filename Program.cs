using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        public class GenericTree<T> where T : GenericTree<T> // recursive constraint  
        {
            // no specific data declaration  

            protected List<T> children;

            public GenericTree()
            {
                this.children = new List<T>();
            }

            public virtual void AddChild(T newChild)
            {
                this.children.Add(newChild);
            }

            public void Traverse(Action<int, T> visitor)
            {
                this.traverse(0, visitor);
            }

            protected virtual void traverse(int depth, Action<int, T> visitor)
            {
                visitor(depth, (T)this);
                foreach (T child in this.children)
                    child.traverse(depth + 1, visitor);
            }
        }

        public class GenericTreeNext : GenericTree<GenericTreeNext> // concrete derivation
        {
            public string Name { get; set; } // user-data example

            public GenericTreeNext(string name)
            {
                this.Name = name;
            }
        }

        static void Main(string[] args)
        {
            GenericTreeNext A = new GenericTreeNext("Green");
            A.AddChild(new GenericTreeNext("Red"));
            GenericTreeNext inter = new GenericTreeNext("Blue");
            inter.AddChild(new GenericTreeNext("Orange"));
            inter.AddChild(new GenericTreeNext("Yellow"));
            A.AddChild(inter);
            GenericTreeNext inter1 = new GenericTreeNext("Violet");
            inter1.AddChild(new GenericTreeNext("Cyan"));
            A.AddChild(inter1);
            A.Traverse(NodeWorker);
            Console.ReadKey();
        }


        static void NodeWorker(int depth, GenericTreeNext node)
        {                                // a little one-line string-concatenation (n-times)
            Console.WriteLine("{0}{1}: {2}", String.Join("   ", new string[depth + 1]), depth, node.Name);
        }
        
    }
}
