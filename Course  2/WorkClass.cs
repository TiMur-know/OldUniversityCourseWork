using Course__2.Classes;
using Course__2.Classes.Models;
using Course__2.Classes.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Course__2
{
    public class WorkClass
    {
        DrugService productService;
        DrugSellService drugSellService;
        public WorkClass()
        {
            productService = new DrugService();
            drugSellService = new DrugSellService();
        }
        public void AuthorizeMeny()
        {
            Console.WriteLine("Enter.Вход\tEsc.Выход\tR.Регистрация");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            switch (consoleKey.Key)
            {
                case ConsoleKey.R:
                    Register();
                    break;
                case ConsoleKey.Enter:
                    Login();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }

        }
        private void Login()
        {

            UserService userService = new UserService();
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            bool isLogin = userService.Login(login, password);
            if (isLogin)
                Medium();
            else
                AuthorizeMeny();

        }
        private void Register()
        {
            Regex regex;
            User pharmacist = new User();
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            Console.WriteLine("Введите пароль повторно:");
            string password2 = Console.ReadLine();
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите номер телефона:");
            string phone = Console.ReadLine();

            if (login.Equals("") || password.Equals("") || name.Equals("") || phone.Equals(""))
            {
                Console.WriteLine("Не все поля заполнены!");
                AuthorizeMeny();
            }
            regex = new Regex(@"^[a-zA-Z](.[a-zA-Z0-9_-]*)$");
            if (!regex.Match(login).Success)
            {
                Console.WriteLine("Неправильный ввод логина!");
                AuthorizeMeny();
            }
            if (password.Length < 8)
            {
                Console.WriteLine("Пароль должен быть больше или равно восьми символам!");
                AuthorizeMeny();
            }
            else if (password != password2)
            {
                Console.WriteLine("Пароли не совпадают!");
                AuthorizeMeny();
            }
            regex = new Regex(@"^[0-9\-\+]{9,15}$");
            //regex = new Regex(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/");
            if (!regex.Match(phone).Success)
            {
                Console.WriteLine("Неверно указан номер телефона!");
                AuthorizeMeny();
            }
            pharmacist.Login = login;
            pharmacist.Password = password;
            pharmacist.Name = name;
            pharmacist.Phone = phone;
            UserService userService = new UserService();
            bool isRegister = userService.add(pharmacist);
            if (isRegister)
                Medium();
            else
                AuthorizeMeny();
        }
        private void GeneLine()
        {
            int r = 100;
            string str = "";
            for (int i = 0; i < r; i++)
            {
                str = str + "-";
            }
            str = str + "\n";
            Console.WriteLine(str);
        }
        private void DisplayAll()
        {
            
            Console.WriteLine("Все препараты:");
            List<Drug> products = productService.getProducts();
            GeneLine();
            string format = "|{0,-10}|{1,-15}|{2,-10}|{3,-10}|{4,-30}|{5,-20}|{6,-10}|";
            Console.WriteLine(format,"Id", "Title", "PriceBuy", "PriceSell", "Disease", "ExpiryData", "Count");
            GeneLine();
            foreach (Drug product in products)
            {
                DisplayRowTable(product);
                GeneLine();
            }
            ChooseProduct();
        }
        private void DisplayRowTable(Drug product)
        {
            string format = "|{0,-10}|{1,-15}|{2,-10}|{3,-10}|{4,-30}|{5,-20}|{6,-10}|";
            Console.WriteLine(format, product.Id, product.Title, product.PriceBuy, product.PriceSell, product.Disease, product.ExpiryData, product.Count);
        }
        private void DisplayRowTable1(DrugSell product)
        {
            string format = "|{0,-10}|{1,-15}|{2,-10}|{3,-10}|{4,-20}|{5,-20}|{6,-10}|{7,-10}|";
            
            Console.WriteLine(format, product.Id, product.Title, product.PriceBuy, product.PriceSell, product.Disease, product.Supplier, product.Count, product.SellAt);
        }
        private void DisplayAllSell()
        {
            Console.WriteLine("Все проданые препараты:");
            List<DrugSell> products = drugSellService.GetDrugSells();
            GeneLine();
            string format = "|{0,-10}|{1,-15}|{2,-10}|{3,-10}|{4,-20}|{5,-20}|{6,-10}|{7,-10}|";
            Console.WriteLine(format, "Id", "Title", "PriceBuy", "PriceSell", "Disease", "Supplier", "Count","SellAt");
            foreach (DrugSell product in products)
            {

                DisplayRowTable1(product);
                GeneLine();
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            ConsoleKeyInfo input = Console.ReadKey(true);
            switch (input.Key)
            {
                default:
                    Medium();
                    break;
            }     
            }
        private void DisplayOne(Drug product)
        {
            GeneLine();
            Console.WriteLine("Подробно про товар:");
            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"Название: {product.Title}");
            Console.WriteLine($"Болезнь: {product.Disease}");
            Console.WriteLine($"Количество: {product.Count}");
            Console.WriteLine($"Цена покупки: {product.PriceBuy}");
            Console.WriteLine($"Цена продажи: {product.PriceSell}");
            Console.WriteLine($"Рецепт: {product.Recipe}");
            Console.WriteLine($"Срок годности: {product.ExpiryData}");
            Console.WriteLine($"Поставщик: {product.Supplier}");
            Console.WriteLine($"Запись создана: {product.CreatedAt}");
            Console.WriteLine($"Запись изменена: {product.UpdateAt}");
            GeneLine();

        }
        private int changeCount(int id,int countSt)
        {
            int count;
            Console.WriteLine("Введите количество:");
            string countStr = Console.ReadLine();
            if (Convert.ToInt32(countStr) >= 0)
            {
                count = Convert.ToInt32(countStr);
                if (count <= countSt)
                {
                    Sell(id,count);
                    return count;
                }
                else
                {
                    Console.WriteLine("Введенное количество должно быть больше или равно количеству препаратов!");
                    return changeCount(id,countSt);
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода количества!");
                return changeCount(id,countSt);
            }
        }
        private void SellShow(int id,int count)
        {
            Drug product = productService.getFromID(id);
            GeneLine();
            Console.WriteLine($"Название: {product.Title}");
            Console.WriteLine($"Цена продажи: {product.PriceSell*count}");
            Console.WriteLine($"Количество: {count}");
            GeneLine();

        }
        private void SellShowProd(int id)
        {
            Drug product = productService.getFromID(id);
            GeneLine();
            Console.WriteLine($"Название: {product.Title}");
            Console.WriteLine($"Болезнь: {product.Disease}");
            Console.WriteLine($"Цена покупки: {product.PriceBuy}");
            Console.WriteLine($"Цена продажи: {product.PriceSell}");
            Console.WriteLine($"Количество: {product.Count}");
            Console.WriteLine($"Срок годности: {product.ExpiryData}");
            GeneLine();
        }
        private void Sell(int id,int count=1)
        {
            SellShowProd(id);
            Drug product = productService.getFromID(id);
            SellShow(id, count);
            Console.WriteLine("Enter.Продать\tR.Изменить количество.\tEsc.Вернуться");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            if (consoleKey.Key == ConsoleKey.R)
            {
                changeCount(id, product.Count);
            }
            else if (consoleKey.Key == ConsoleKey.Enter)
            {
                SellProd(id, count);
            }
            else if (consoleKey.Key == ConsoleKey.Escape)
            {
                ShowOne(id);
            }
        }
        private void SellProd(int id,int count)
        {
            Drug product = productService.getFromID(id);
            DrugSell drugSell = new DrugSell()
            {
                Count=count,
                PriceBuy=product.PriceBuy,
                PriceSell=product.PriceSell,
                Title=product.Title,
                Disease=product.Disease,
                Supplier=product.Supplier,
                SellAt= DateTime.Now.ToString()
            };
            product.Count = product.Count - count;
            if (product.Count - count == 0)
                productService.delete(product);
            else
                productService.update(product);
            drugSellService.add(drugSell);
            Console.WriteLine("Препарат продан!");
            Medium();
        }
        private void ShowOne(int id)
        {
            Drug product = productService.getFromID(id);
            DisplayOne(product);
            Console.WriteLine("Enter.Продать\tR.Редактировать.\tD.Удалить\tEsc.Вернуться");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);

            if (consoleKey.Key == ConsoleKey.R)
            {
                EditProduct(product.Id);
            }
            else if (consoleKey.Key == ConsoleKey.Enter)
            {
                Sell(product.Id);
            }
            else if (consoleKey.Key == ConsoleKey.D)
            {
                Delete(product.Id);
            }
            else if (consoleKey.Key == ConsoleKey.Escape)
            {
                Medium();
            }
        }
        private void AddProduct()
        {
            double priceBuy = 0;
            double priceSell = 0;
            int count = 0;
            Console.WriteLine("Добавление препарата:");
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите болезнь:");
            string disease = Console.ReadLine();
            Console.WriteLine("Введите поставщика:");
            string supplier = Console.ReadLine();
            Console.WriteLine("Введите цену покупки за 1:");
            string priceStr = Console.ReadLine();
            if (title == "" || disease == "" || supplier == "")
            {
                Console.WriteLine("Одно из полей пустое!");
                return;
            }
            if (Convert.ToDouble(priceStr) > 0)
            {
                priceBuy = Convert.ToDouble(priceStr);
            }
            else
            {
                Console.WriteLine("Ошибка ввода цены!");
                return;
            }
            Console.WriteLine("Введите продажи за 1:");
            priceStr = Console.ReadLine();
            if (Convert.ToDouble(priceStr) > 0)
            {
                priceSell = Convert.ToDouble(priceStr);
            }
            else
            {
                Console.WriteLine("Ошибка ввода цены!");
                return;
            }
            Console.WriteLine("Введите количество:");
            string countStr = Console.ReadLine();
            if (Convert.ToInt32(countStr) > 0)
            {
                count = Convert.ToInt32(countStr);
            }
            else
            {
                Console.WriteLine("Ошибка ввода количества!");
                return;
            }
            Console.WriteLine("Введите рецепт:");
            string recipe = Console.ReadLine();
            Console.WriteLine("Введите срок годности:");
            string expiryData = Console.ReadLine();
            Drug product = new Drug()
            {
                Title = title,
                Disease = disease,
                Supplier = supplier,
                Recipe = recipe,
                ExpiryData = expiryData,
                Count = count,
                PriceBuy = priceBuy,
                PriceSell = priceSell,
                CreatedAt = DateTime.Now.ToString(),
                UpdateAt = DateTime.Now.ToString()
            };
            Console.WriteLine("Вы уверены что хотите добавить этот продукт?");
            Console.WriteLine("Enter.Да\t Esc.Нет ");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            if (consoleKey.Key == ConsoleKey.Enter)
            {
                bool del = productService.add(product);
                if (del)
                {
                    Console.WriteLine("Препарат добавлен");
                    Medium();
                }
                else
                {
                    Console.WriteLine("Ошибка изменения");
                    AddProduct();
                }
            }
            else if (consoleKey.Key == ConsoleKey.Escape)
                Medium();
        }
        private void EditProduct(int id)
        {
            Drug product = productService.getFromID(id);
            DisplayOne(product);
            double price = 0;
            Console.WriteLine("Редактирование препарата:");
            Console.WriteLine("(При отсутствии необходимости изменения строки, оставьте поле пустым)");
            Console.WriteLine("Введите название :");
            string title = Console.ReadLine();
            Console.WriteLine("Введите болезнь:");
            string disease = Console.ReadLine();
            Console.WriteLine("Введите поставщика:");
            string supplier = Console.ReadLine();
            Console.WriteLine("Введите цену продажи за 1 шт (0 если оставить без изменения):");
            string priceStr = Console.ReadLine();
            if (Convert.ToDouble(priceStr) >= 0)
            {
                price = Convert.ToDouble(priceStr);
            }
            else
            {
                Console.WriteLine("Ошибка ввода цены!");
                return;
            }
            Console.WriteLine("Введите рецепт:");
            string recipe = Console.ReadLine();
            Console.WriteLine("Введите срок годности:");
            string expiryData = Console.ReadLine();

            Drug product1 = new Drug()
            {
                Title = title,
                Disease = disease,
                Supplier = supplier,
                Recipe = recipe,
                ExpiryData = expiryData,
                PriceSell = price
            };
            product.Title = product1.Title==""?product.Title:product1.Title;
            product.Disease = product1.Disease == "" ? product.Disease : product1.Disease;
            product.Supplier = product1.Supplier == "" ? product.Supplier : product1.Supplier;
            product.Recipe = product1.Recipe == "" ? product.Recipe : product1.Recipe;
            product.ExpiryData = product1.ExpiryData == "" ? product.ExpiryData : product1.ExpiryData;
            product.PriceSell = product1.PriceSell == 0 ? product.PriceSell : product1.PriceSell;
            product.UpdateAt = DateTime.Now.ToString();
            Console.WriteLine("Вы уверены что хотите изменить этот продукт?");
            Console.WriteLine("Enter.Да\t Esc.Нет ");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            if (consoleKey.Key == ConsoleKey.Enter)
            {
                bool del = productService.update(product);
                if (del)
                {
                    Console.WriteLine("Препарат изменен");
                    ShowOne(id);
                }
                else
                {
                    Console.WriteLine("Ошибка изменения");

                }
            }
            else if (consoleKey.Key == ConsoleKey.Escape)
                ShowOne(id);

        }
        private void Delete(int id)
        {
            Console.WriteLine("Удаление препарата:");
            Drug product = productService.getFromID(id);
            Console.WriteLine("Вы уверены что хотите удалить этот продукт?");
            Console.WriteLine("Enter.Да\t Esc.Нет ");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            if (consoleKey.Key == ConsoleKey.Enter)
            {
                bool del = productService.delete(product);
                if (del)
                {
                    Console.WriteLine("Препарат удален");
                    Medium();
                }
                else { 
                    Console.WriteLine("Ошибка удаления");
                    Medium();
                }
            }
            else if (consoleKey.Key == ConsoleKey.Escape)
                ShowOne(id);

        }
        private void ChooseProduct()
        {
            List<Drug> products = productService.getProducts();
            Console.WriteLine("Выбор препарата.\n Введите номер препарата:");
            string strnum = Console.ReadLine();
            if (int.TryParse(strnum, out int n))
            {
                int num = Convert.ToInt32(strnum);
                if (num > -1)
                {
                    Drug product = products.Where(i => i.Id == num).FirstOrDefault();
                    if (product != null)
                        ShowOne(product.Id);
                }
            }
        }
        
        private void Medium()
        {
            Console.Clear();
            List<Drug> products = productService.getProducts();
            Console.WriteLine("Основное меню:");
            GeneLine();
            int count = 0;

            if (products == null)
                count = 0;
            else
                count = products.Count;
            if (count!=0) {
                int i = 0;
                foreach(Drug product in products)
                {
                    string format = "|{0,-10}|{1,-15}|{2,-10}|{3,-10}|{4,-30}|{5,-20}|{6,-10}|";
                    Console.WriteLine(format, "Id", "Title", "PriceBuy", "PriceSell", "Disease", "ExpiryData", "Count");
                    GeneLine();
                    DisplayRowTable(product);
                    GeneLine();
                    if (i == 19)
                        break;
                    i++;
                }
            }
            else { 
                Console.WriteLine("Препаратов нет");
            GeneLine();
            }
            if (count == 0) { 
            Console.WriteLine("1.Добавить препарат");
                Console.WriteLine("2.Показать все проданые препараты");
            }
            else { 
            Console.WriteLine($"1.Показать все препараты({count}/20)");
            Console.WriteLine("2.Выбрать препарат");
            Console.WriteLine("3.Добавить препарат");
            Console.WriteLine("4.Поиск препарата по параметру");
                Console.WriteLine("5.Показать все проданые препараты");
                Console.WriteLine("6.Печать всего");
                Console.WriteLine("Escape.Выйти");
            }
            GeneLine();
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (count != 0) {
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        DisplayAll();
                        break;
                    case ConsoleKey.D2:
                        ChooseProduct();
                        break;
                    case ConsoleKey.D3:
                        AddProduct();
                        break;
                    case ConsoleKey.D4:
                        SearchMeny();
                        break;
                    case ConsoleKey.D5:
                        DisplayAllSell();
                        break;
                    case ConsoleKey.D6:
                        printAll();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Medium();
                        break;
                }
            }
            else
            {
                if(input.Key == ConsoleKey.D1)
                {
                    AddProduct();
                }
                else if (input.Key == ConsoleKey.D2)
                {
                    DisplayAllSell();
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Medium();
                }
            }
            
        }
        private async void printAll()
        {
            
               List<Drug> drugs= productService.getProducts();
                List<DrugSell> drugSells = drugSellService.GetDrugSells();
                string data = "";
                string line = "------------------------------------\n";
                data += line;
                data +="\tПрепараты:\n";
                data += line;
                foreach (Drug product in drugs) { 
                data += line;
               data += $"Id: {product.Id}\n";
                data += $"Название: {product.Title}\n";
                data += $"Болезнь: {product.Disease}\n";
                data += $"Количество: {product.Count}\n";
                data += $"Цена покупки: {product.PriceBuy}\n";
                data += $"Цена продажи: {product.PriceSell}\n";
                data += $"Рецепт: {product.Recipe}\n";
                data += $"Срок годности: {product.ExpiryData}\n";
                data += $"Поставщик: {product.Supplier}\n";
                data += $"Запись создана: {product.CreatedAt}\n";
                data += $"Запись изменена: {product.UpdateAt}\n";
                }
                data += line;
                data += "\tПроданые препараты:\n";
                data += line;
                foreach (DrugSell drugSell in drugSells)
                {
                    data += line;
                    data += $"Id: {drugSell.Id}\n";
                    data += $"Название: {drugSell.Title}\n";
                    data += $"Количество: {drugSell.Count}\n";
                    data += $"Цена покупки: {drugSell.PriceBuy}\n";
                    data += $"Цена продажи: {drugSell.PriceSell}\n";
                    data += $"Болезнь: {drugSell.Disease}\n";
                    data += $"Поставщик: {drugSell.Supplier}\n";
                    data += $"Продано: {drugSell.SellAt}\n";

                }
                string path = "note.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    await writer.WriteLineAsync(data);
                }
                Console.WriteLine("Данные успешно распечатаны!");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private void DisplaySearch(List<Drug> drugs)
        {
            GeneLine();
            if (drugs != null) { 
            foreach (Drug drug in drugs)
            {
                
                DisplayRowTable(drug);
                GeneLine();
            }
            ChooseProduct();
            }
            else
            {
                Console.WriteLine("Нічого не знайдено");
                Medium();
            }
        }
        private void SearchMeny()
        {
            Console.WriteLine("Поиск препарата:");
            Console.WriteLine("(Шаблон--) 1.Парацетамол");
            Console.WriteLine("1.Названию");
            Console.WriteLine("2.Болезни");
            Console.WriteLine("3.Стоимости");
            Console.WriteLine("4.Дате истечения срока:");
            Console.WriteLine("5.Поставщику:");
            string str = Console.ReadLine();
            string[] strings = str.Split('.');
            List<Drug> drugs = new List<Drug>();
            switch (strings[0])
            {
                case "1":
                    drugs=productService.getProductsFromParament("title", strings[1]);
                    break;
                case "2":
                    drugs= productService.getProductsFromParament("disease", strings[1]);
                    break;
                case "3":
                    drugs= productService.getProductsFromParament("price", strings[1]);
                    break;
                case "4":
                    drugs= productService.getProductsFromParament("expiry", strings[1]);
                    break;
                case "5":
                    productService.getProductsFromParament("supplier", strings[1]);
                    break;
                default:
                    Medium();
                    break;
            }
            DisplaySearch(drugs);
        }
    }
}
