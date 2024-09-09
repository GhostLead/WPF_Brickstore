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
            if (bricks.Count >0)
            {
                if (txtID.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ItemID.ToLower().StartsWith(txtID.Text.ToLower()) && x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }
                if (txtNAME.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ItemName.ToLower().StartsWith(txtNAME.Text.ToLower()) && x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }
                if (cbLista.SelectedIndex != 0)
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }
                if (txtCOLOR.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.ColorName.ToLower().StartsWith(txtCOLOR.Text.ToLower()) && x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }
                if (txtQTY.Text != "")
                {
                    dgTabla.ItemsSource = bricks.Where(x => x.Qty1 == int.Parse(txtQTY.Text) && x.CategoryName.ToLower() == cbLista.SelectedItem.ToString().ToLower());
                }
                if (txtID.Text == "" && txtNAME.Text == "" && txtCOLOR.Text == "" && txtQTY.Text == "")
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
