using Microsoft.Win32;
using System.IO;
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
                try
                {
                    XDocument xaml = XDocument.Load(ofd.FileName);
                    foreach (var elem in xaml.Descendants("Item"))
                    {
                        bricks.Add(new Brick($"{elem.Element("ItemID").Value};{elem.Element("ItemName").Value};{elem.Element("CategoryName").Value};{elem.Element("ColorName").Value};{elem.Element("Qty").Value};"));
                    }
                    dgTabla.ItemsSource = bricks;
                }
                catch (System.Xml.XmlException)
                {
                    MessageBox.Show("Ez a fájl nem megfelelő fomrátumú!");
                }
                 
                
            }
        }

        private void btnSzur_Click(object sender, RoutedEventArgs e)
        {
            if (bricks.Count >0)
            {
                if (txtID.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ItemID.StartsWith(txtID.Text));
                }
                else if (txtNAME.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ItemName.StartsWith(txtNAME.Text));
                }
                else if (txtCATEGORY.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.CategoryName.StartsWith(txtCATEGORY.Text));
                }
                else if (txtCOLOR.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ColorName.StartsWith(txtCOLOR.Text));
                }
                else if (txtQTY.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.Qty1 == int.Parse(txtQTY.Text));
                }
                else
                {
                    MessageBox.Show("Nincs ilyen!");
                }
            }
            else
            {
                MessageBox.Show("Nincs ilyen!");
            }
        }
    }
}