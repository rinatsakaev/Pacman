using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class SinglyLinkedList<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;
        public readonly int Length;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
            Length = previous?.Length + 1 ?? 1;
        }
        public List<T> ToList()
        {
            var result = new List<T> { Value };
            var pathItem = Previous;
            while (pathItem != null)
            {
                result.Add(pathItem.Value);
                pathItem = pathItem.Previous;
            }
            return result;
        }
    }
}
