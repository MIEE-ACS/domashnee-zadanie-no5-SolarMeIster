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
            {
                return $"Пункт назначения: {destination}, Номер рейса: {flight_number}, Кол-во билетов: {number_of_tickets}, Кол-во оставшихся мест: мест нет.";
            }
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
            new Aeroflot { destination = "Екатеринбург", flight_number = 16,  number_of_tickets = 500, number_of_remaining_tickets = 410}
        };
        public void firstAeroFlotList()
        {
            lbAeroFlots.Items.Clear();
            foreach (var aeroflot in aeroflots)
            {
                cbDestination.Items.Add(aeroflot.destination);
                cbFlightNumber.Items.Add(aeroflot.flight_number);
                lbAeroFlots.Items.Add(aeroflot);
            }
        }
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
            firstAeroFlotList();
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
                    cbDestination.Items.Add(aeroflot.destination);
                    cbFlightNumber.Items.Add(aeroflot.flight_number);
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
                int number = int.Parse(cbFlightNumber.Text);
                foreach (var aeroflot in aeroflots)
                {
                    if (cbDestination.Text == aeroflot.destination && number == aeroflot.flight_number)
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
    }
}
