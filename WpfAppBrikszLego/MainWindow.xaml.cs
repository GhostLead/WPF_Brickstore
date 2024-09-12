using Microsoft.Win32;
using System.IO;
using System.Linq;
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
                finally
                {
                    List<string> categories = new List<string>();
                    
                    foreach (var item in bricks)
                    {
                        if (!categories.Contains(item.CategoryName))
                        {
                            categories.Add(item.CategoryName.ToString());
                        }
                        else
                        {
                            continue;
                        }
                    }
                    categories.Sort();
                    categories.Insert(0,"WHATEVER");
                    cbLista.ItemsSource = categories;
                    cbLista.SelectedIndex = 0;
                }

            }

            
        }

        private void textChanged(object sender, RoutedEventArgs e)
        {
            if (bricks.Count > 0)
            {
                IEnumerable<Brick> filteredList = bricks;

                if (!string.IsNullOrWhiteSpace(txtID.Text))
                {
                    filteredList = filteredList.Where(x => x.ItemID.ToLower().StartsWith(txtID.Text.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(txtNAME.Text))
                {
                    filteredList = filteredList.Where(x => x.ItemName.ToLower().StartsWith(txtNAME.Text.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(txtCOLOR.Text))
                {
                    filteredList = filteredList.Where(x => x.ColorName.ToLower().StartsWith(txtCOLOR.Text.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(txtQTY.Text) && int.TryParse(txtQTY.Text, out int qty))
                {
                    filteredList = filteredList.Where(x => x.Qty1 == qty);
                }

                if (cbLista.SelectedIndex > 0)
                {
                    filteredList = filteredList.Where(x => x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }

                var finalList = filteredList.ToList();
                dgTabla.ItemsSource = finalList;

                var categories = finalList.Select(x => x.CategoryName).Distinct().ToList();
                categories.Sort();
                categories.Insert(0, "WHATEVER");

                cbLista.ItemsSource = categories;
                cbLista.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Még nincs importálva fájl");
            }
        }


        private void cbLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLista.SelectedIndex == 0)
            {
                dgTabla.ItemsSource = bricks;
            }
            else
            {
                dgTabla.ItemsSource = bricks.Where(x => x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
            }
        }
    }
}
