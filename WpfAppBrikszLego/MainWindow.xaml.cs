using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfAppBrikszLego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        List<Brick> bricks = new List<Brick>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
             
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                XDocument xaml = XDocument.Load(ofd.FileName);
                foreach (var elem in xaml.Descendants("Item"))
                {
                    bricks.Add(new Brick($"{elem.Element("ItemID").Value};{elem.Element("ItemName").Value};{elem.Element("CategoryName").Value};{elem.Element("ColorName").Value};{elem.Element("Qty").Value};"));
                }
                dgTabla.ItemsSource = bricks;
            }
        }

        private void btnSzur_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}