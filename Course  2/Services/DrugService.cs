using Course__2.Classes.Repos;
using System.Collections.Generic;

namespace Course__2.Classes.Services
{
    public class DrugService
    {
        private DrugRepos productRepos;
        public DrugService()
        {
            productRepos = new DrugRepos();
        }

        public Drug getFromID(int id)
        {
           return  productRepos.GetItemFromId(id);
        }
        public bool add(Drug product)
        {
            return productRepos.AddItem(product);
        }
        public bool update(Drug product)
        {
           return productRepos.UpdateItem(product);
        }
        public bool delete(Drug product)
        {
           return  productRepos.DeleteItem(product);
        }
        public List<Drug> getProducts()
        {
            return productRepos.getAll();
        }
        public List<Drug> getProductsFromParament(string param,string str)
        {
            return productRepos.GetItemsFromParametr(param,str);
        }
    }
}
