using Course__2.Classes.Models;
using Course__2.Classes.Repos;
using System.Collections.Generic;

namespace Course__2.Classes.Services
{
    public class DrugSellService
    {
        private DrugSellRepos drugSellRepos;
        public DrugSellService()
        {
            drugSellRepos = new DrugSellRepos();
        }
        public bool add(DrugSell item)
        {
            if(drugSellRepos.CheckItem(item))
                return drugSellRepos.UpdateItem(item);
            else
                return drugSellRepos.AddItem(item);
        }
        public List<DrugSell> GetDrugSells()
        {
            return drugSellRepos.getAll();
        }
        public DrugSell getFromID(int id)
        {
            return drugSellRepos.GetItemFromId(id);
        }
    }
}
