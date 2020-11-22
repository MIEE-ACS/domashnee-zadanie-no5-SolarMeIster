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

namespace HomeWork_5
{
    class Aeroflot
    {
        public string destination;
        public int flight_number;
        public int number_of_tickets;
        public int number_of_remaining_tickets;
        public int Flight_number
        {
            set
            {
                if (value > 0)
                {
                    flight_number = value;
                }
            }
            get
            {
                return flight_number;
            }
        }
        public int Number_of_tickets
        {
            set
            {
                if (value > 0 && value < 1000)
                {
                    number_of_tickets = value;
                }
            }
            get
            {
                return number_of_tickets;
            }
        }
        public override string ToString()
        {
            if(number_of_remaining_tickets == 0 || number_of_remaining_tickets < 0)            
                return $"Пункт назначения: {destination}, Номер рейса: {flight_number}, Кол-во билетов: {number_of_tickets}, Кол-во оставшихся мест: МЕСТ НЕТ.";
            else
                return $"Пункт назначения: {destination}, Номер рейса: {flight_number}, Кол-во билетов: {number_of_tickets}, Кол-во оставшихся мест: {number_of_remaining_tickets}.";
        }
    }
    public partial class MainWindow : Window
    {
        static List<Aeroflot> aeroflots = new List<Aeroflot>
        {
            new Aeroflot { destination = "Москва", flight_number = 44,  number_of_tickets = 400, number_of_remaining_tickets = 50},
            new Aeroflot { destination = "Санкт-Петербург", flight_number = 34,  number_of_tickets = 200, number_of_remaining_tickets = 180},
            new Aeroflot { destination = "Екатеринбург", flight_number = 145,  number_of_tickets = 500, number_of_remaining_tickets = 410},
            new Aeroflot { destination = "Магадан", flight_number = 6,  number_of_tickets = 350, number_of_remaining_tickets = 200},
            new Aeroflot { destination = "Нижний-Новгород", flight_number = 16,  number_of_tickets = 100, number_of_remaining_tickets = 40}
        };
        public void updateAeroFlotList()
        {
            lbAeroFlots.Items.Clear();
            foreach (var aeroflot in aeroflots)
            {
                lbAeroFlots.Items.Add(aeroflot);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            updateAeroFlotList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number;
                bool flag = int.TryParse(tbDestination.Text, out number);
                if (flag == false) {
                    Random rand = new Random();
                    int rnd = rand.Next(int.Parse(tbNumberOfTickets.Text));
                    Aeroflot aeroflot = new Aeroflot
                    {
                        destination = tbDestination.Text,
                        flight_number = int.Parse(tbFlightNumber.Text),
                        number_of_tickets = int.Parse(tbNumberOfTickets.Text),
                        number_of_remaining_tickets = int.Parse(tbNumberOfTickets.Text) - rnd
                    };
                    aeroflots.Add(aeroflot);
                    updateAeroFlotList();
                }
                else
                    MessageBox.Show("Что-то не то!!!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте введенные значения", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Обратитесь к разработчику: " + ex.Message, "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var aeroflot in aeroflots)
                {
                    if (tbFlightNumber1.Text == aeroflot.flight_number.ToString())
                    {
                        aeroflot.number_of_remaining_tickets -= 1;
                    }
                }
                updateAeroFlotList();
            }
            catch
            {
                MessageBox.Show("Проверьте введенные значения", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lbAeroFlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Aeroflot aeroflot = lbAeroFlots.SelectedItem as Aeroflot;
            if (aeroflot != null)
            {
                tbFlightNumber1.Text = aeroflot.flight_number.ToString();
            }
        }
    }
}