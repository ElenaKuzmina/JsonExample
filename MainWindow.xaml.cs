using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JsonExample.Classes;
using Newtonsoft.Json;

namespace JsonExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DGProduct.ItemsSource = CoonectHelper.tradeEntities.Product.ToList();
        }

        private void BtnSaveJson_Click(object sender, RoutedEventArgs e)
        {
           
            File.WriteAllText("input.json", string.Empty); // Удаление содержимого файла
            foreach (Product nt in CoonectHelper.tradeEntities.Product)//Перебирание записей в таблице Note
                File.AppendAllText("input.json", JsonConvert.SerializeObject(nt));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Product> prod = new List<Product>();//Список записей
            JsonTextReader reader = new JsonTextReader(new StreamReader("input.json"));//Открытие файла
            reader.SupportMultipleContent = true;
            while (reader.Read())//Пока не закончатся записи
            {
                JsonSerializer serializer = new JsonSerializer();
                Product temp_point = serializer.Deserialize<Product>(reader); // 1 запись
                if (temp_point.ProductName.Contains(TxtSearch.Text)) //Отображение по совпадению с поиском
                    prod.Add(temp_point);
            }
            DGProduct.ItemsSource = prod;
            if (TxtSearch.Text == string.Empty)
                DGProduct.ItemsSource = CoonectHelper.tradeEntities.Product.ToList();
        }
    }
}
