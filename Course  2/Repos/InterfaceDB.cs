using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Course__2.Classes.Repos
{
    public interface InterfaceDB<T>
    {
        List<T> getAll();
        T GetItemFromId(int id);
        bool AddItem(T item);
        bool UpdateItem(T item);
        bool DeleteItem(T item);
    }
}
