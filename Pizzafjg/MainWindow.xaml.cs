using System;
using System.Collections.Generic;
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

namespace pizzaShop
{
    public partial class MainWindow : Window
    {
        private int priceMargarita = 450;
        private int pricePepperoni = 550;
        private int priceHawaii = 500;
        private int priceCola = 100;
        private int priceJuice = 80;
        private int priceWater = 50;
        private int priceDelivery = 150;
        public MainWindow()
        {
            InitializeComponent();
        }
        //получаем цену выбраной пиццы
        private int GetPizzaPrice()
        {
            if (MargaritaRbtn.IsChecked == true)
                return priceMargarita;
            if (PepperoniRbtn.IsChecked == true)
                return pricePepperoni;
            else
                return priceHawaii;
        }
        //получаем название пиццы
        private string GetPizzaName()
        {
            if (MargaritaRbtn.IsChecked == true)
                return "Маргарита";
            if (PepperoniRbtn.IsChecked == true)
                return "Пепперони";
            else
                return "Гавайская";
        }
        //получаем количество
        private int GetQuantity()
        {
            if (int.TryParse(QuantityTb.Text, out int quantity))
                return quantity;
            return 1;
        }
        //Получаем цену напитков
        private int GetDrinkPrice()
        {
            if (DrinkChbx.IsChecked != true)
                return 0;

            if (ColaRbtn.IsChecked == true)
                return priceCola;
            if (JuiceRbtn.IsChecked == true)
                return priceJuice;
            else
                return priceWater;
        }
        //получаем название напитков
        private string GetDrinkName()
        {
            if (DrinkChbx.IsChecked != true)
                return "не выбран";

            if (ColaRbtn.IsChecked == true)
                return "Кола";
            if (JuiceRbtn.IsChecked == true)
                return "Сок";
            else
                return "Вода";
        }
        private void UpdateTotal()
        {
            int pizzaPrice = GetPizzaPrice();
            int quantity = GetQuantity();
            int drinkPrice = GetDrinkPrice();
            int deliveryPrice = GetDeliveryPrice();
            int extraPrice = GetExtraPrice();
            int total = (pizzaPrice * quantity) + drinkPrice + deliveryPrice + extraPrice;
            if (HasDiscount())
            {
                total = (int)(total * 0.9);
            }

            TotalTl.Text = $"ИТОГО: {total} руб.";
            if (total > 1000)
            {
                TotalTl.Foreground = Brushes.Green;
            }
            else
            {
                TotalTl.Foreground = Brushes.Red;
            }
        }

        private bool HasDiscount()
        {
            return PromoTb.Text.ToLower() == "pizza10";
        }
        private void AnyParameter_Changed(object sender, RoutedEventArgs e)
        {
            UpdateTotal();
        }
        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity > 1)
            {
                QuantityTb.Text = (quantity - 1).ToString();
                UpdateTotal();
            }
        }
        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = GetQuantity();
            if (quantity < 10)
            {
                QuantityTb.Text = (quantity + 1).ToString();
                UpdateTotal();
            }
        }
        private void DrinkChbx_Click(object sender, RoutedEventArgs e)
        {
            bool isEnabled = DrinkChbx.IsChecked == true;
            DrinkPanel.IsEnabled = isEnabled;
            UpdateTotal();
        }
        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string pizzaName = GetPizzaName();
            string extraName = GetExtraName();
            string deliveryName = GetDeliveryName();
            int quant = GetQuantity();
            string drinkName = GetDrinkName();
            int total = (GetPizzaPrice() * quant) + GetDrinkPrice() + GetDeliveryPrice() + GetExtraPrice();
                if (HasDiscount())
            {
                total = (int)(total * 0.9);
            }
            

            string message = "ВАШ ЗАКАЗ\n" +
                            $"Пицца: {pizzaName}, {quant} шт.\n" +
                            $"Напиток: {drinkName}\n" +
                            $"Способ получения: {deliveryName} \n" +
                            $"Доп. ингредиенты: {extraName} \n" +
                            $"Сумма: {total} руб.\n" +
                            "Спасибо за заказ";
            MessageBox.Show(message, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetAll();
        }
        private string GetDeliveryName()
        {
            if (DeliveryRbtn.IsChecked == true)
            {
                return "Доставка";
            }
            else
            {
                return "Самовывоз";
            }
        }
        private int GetDeliveryPrice()
        {
            if (DeliveryRbtn.IsChecked == true)
                return priceDelivery;

            return 0;
        }

        private int GetExtraPrice()
        {
            int total = 0;

            if (CheeseChbx.IsChecked == true)
                total += 50;

            if (BaconChbx.IsChecked == true)
                total += 70;

            if (OlivkiChbx.IsChecked == true)
                total += 100;

            if (GribChbx.IsChecked == true)
                total += 80;

            return total;
        }

        private string GetExtraName()
        {
            if (CheeseChbx.IsChecked == true)
                return "Сыр";
            if (BaconChbx.IsChecked == true)
                return "Бекончик";
            if (OlivkiChbx.IsChecked == true)
                return "Оливочки";
            if (GribChbx.IsChecked == true)
                return "Грибочки";
            else
                return "нет доп";
        }

        private void ReserBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }
        private void ResetAll()
        {
            MargaritaRbtn.IsChecked = true;
            QuantityTb.Text = "1";
            DrinkChbx.IsChecked = false;
            WaterRbtn.IsChecked = true;
            DrinkPanel.IsEnabled = false;
            CheeseChbx.IsChecked = false;
            BaconChbx.IsChecked = false;
            OlivkiChbx.IsChecked = false;
            GribChbx.IsChecked = false;
            DeliveryRbtn.IsChecked = false;
            UpdateTotal();
        }
    }
}