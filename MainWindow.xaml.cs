using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using VideotecaModel;

namespace Delia_Cristiana_Beian___Proiect_Videoteca_Online
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //using VideotecaModel
        ActionState actionClienti = ActionState.Nothing;
        ActionState actionInventar = ActionState.Nothing;
        ActionState actionComenzi = ActionState.Nothing;



        VideotecaEntitiesModel ctx = new VideotecaEntitiesModel();
        CollectionViewSource clientiViewSource;
        CollectionViewSource inventarViewSource;
        CollectionViewSource clientiComenzisViewSource;


        enum ActionState
        {
            New,
            Edit,
            Delete,
            Nothing
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //using System.Data.Entity;
            clientiViewSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            clientiViewSource.Source = ctx.Clientis.Local;
            ctx.Clientis.Load();

            //using System.Data.Entity;
            inventarViewSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventarViewSource")));
            inventarViewSource.Source = ctx.Inventars.Local;
            ctx.Inventars.Load();

            //using System.Data.Entity;
            clientiComenzisViewSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiComenzisViewSource")));
            clientiComenzisViewSource.Source = ctx.Comenzis.Local;
            ctx.Comenzis.Load();


            cmbClienti.ItemsSource = ctx.Clientis.Local;
            cmbClienti.DisplayMemberPath = "Prenume";
            cmbClienti.SelectedValuePath = "ClientID";
            cmbInventar.ItemsSource = ctx.Inventars.Local;
            cmbInventar.DisplayMemberPath = "Titlu";
            cmbInventar.SelectedValuePath = "VideoID";
        }


        // butoane pentru Clienti
        private void btnInainte_Click(object sender, RoutedEventArgs e)
        {
            clientiViewSource.View.MoveCurrentToNext();
        }

        private void btnInapoi_Click(object sender, RoutedEventArgs e)
        {
            clientiViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNou_Click(object sender, RoutedEventArgs e)
        {
            actionClienti = ActionState.New;
            btnNou.IsEnabled = false;
            btnModifica.IsEnabled = false;
            btnSterge.IsEnabled = false;

            btnSalveaza.IsEnabled = true;
            btnAnuleaza.IsEnabled = true;

            btnInapoi.IsEnabled = false;
            btnInainte.IsEnabled = false;

            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;
            telefonTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(telefonTextBox, TextBox.TextProperty);

            numeTextBox.Text = "";
            prenumeTextBox.Text = "";
            telefonTextBox.Text = "";
            Keyboard.Focus(numeTextBox);
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            actionClienti = ActionState.Edit;
            string tempFirstName = numeTextBox.Text.ToString();
            string tempLastName = prenumeTextBox.Text.ToString();
            string tempTelephone = telefonTextBox.Text.ToString();

            btnNou.IsEnabled = false;
            btnModifica.IsEnabled = false;
            btnSterge.IsEnabled = false;

            btnSalveaza.IsEnabled = true;
            btnAnuleaza.IsEnabled = true;

            btnInapoi.IsEnabled = false;
            btnInainte.IsEnabled = false;

            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;
            telefonTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(telefonTextBox, TextBox.TextProperty);

            numeTextBox.Text = tempFirstName;
            prenumeTextBox.Text = tempLastName;
            telefonTextBox.Text = tempTelephone;
            Keyboard.Focus(numeTextBox);
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            actionClienti = ActionState.Delete;
            string tempFirstName = numeTextBox.Text.ToString();
            string tempLastName = prenumeTextBox.Text.ToString();
            string tempTelephone = telefonTextBox.Text.ToString();

            btnNou.IsEnabled = false;
            btnModifica.IsEnabled = false;
            btnSterge.IsEnabled = false;

            btnSalveaza.IsEnabled = true;
            btnAnuleaza.IsEnabled = true;

            btnInapoi.IsEnabled = false;
            btnInainte.IsEnabled = false;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(telefonTextBox, TextBox.TextProperty);

            numeTextBox.Text = tempFirstName;
            prenumeTextBox.Text = tempLastName;
            telefonTextBox.Text = tempTelephone;
        }

        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            actionClienti = ActionState.Nothing;

            btnNou.IsEnabled = true;
            btnModifica.IsEnabled = true;
            btnSterge.IsEnabled = true;

            btnSalveaza.IsEnabled = false;
            btnAnuleaza.IsEnabled = false;

            btnInapoi.IsEnabled = true;
            btnInainte.IsEnabled = true;

            numeTextBox.Text = "";
            prenumeTextBox.Text = "";
            telefonTextBox.Text = "";


            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;
            telefonTextBox.IsEnabled = true;

        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            Clienti customer = null;
            if (actionClienti == ActionState.New)
            {
                try
                {
                    //instantiem entitatea Clienti
                    customer = new Clienti()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        Telefon = telefonTextBox.Text.Trim(),
                    };
                    //adaugam entitatea noua 
                    ctx.Clientis.Add(customer);
                    clientiViewSource.View.Refresh();
                    //salvam 
                    ctx.SaveChanges();
                }
                //folosim System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNou.IsEnabled = true;
                btnModifica.IsEnabled = true;
                btnSalveaza.IsEnabled = false;
                btnAnuleaza.IsEnabled = false;
                btnSterge.IsEnabled = true;
                btnInapoi.IsEnabled = true;
                btnInainte.IsEnabled = true;

                numeTextBox.Text = "";
                prenumeTextBox.Text = "";
                telefonTextBox.Text = "";

                numeTextBox.IsEnabled = true;
                prenumeTextBox.IsEnabled = true;
                telefonTextBox.IsEnabled = true;
            }
            else if (actionClienti == ActionState.Edit)
            {
                try
                {
                    customer = (Clienti)clientiDataGrid.SelectedItem;
                    customer.Nume = numeTextBox.Text.Trim();
                    customer.Prenume = prenumeTextBox.Text.Trim();
                    customer.Telefon = telefonTextBox.Text.Trim();
                    //salvam 
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // pozitionarea pe item-ul curent
                clientiViewSource.View.Refresh();
                clientiViewSource.View.MoveCurrentTo(customer);
                btnNou.IsEnabled = true;
                btnModifica.IsEnabled = true;
                btnSalveaza.IsEnabled = false;
                btnAnuleaza.IsEnabled = false;
                btnSterge.IsEnabled = true;
                btnInapoi.IsEnabled = true;
                btnInainte.IsEnabled = true;

                numeTextBox.Text = "";
                prenumeTextBox.Text = "";
                telefonTextBox.Text = "";

                numeTextBox.IsEnabled = true;
                prenumeTextBox.IsEnabled = true;
                telefonTextBox.IsEnabled = true;
            }
            else if (actionClienti == ActionState.Delete)
            {
                try
                {
                    customer = (Clienti)clientiDataGrid.SelectedItem;
                    ctx.Clientis.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientiViewSource.View.Refresh();
                btnNou.IsEnabled = true;
                btnModifica.IsEnabled = true;
                btnSalveaza.IsEnabled = false;
                btnAnuleaza.IsEnabled = false;
                btnInapoi.IsEnabled = true;
                btnInainte.IsEnabled = true;

                numeTextBox.Text = "";
                prenumeTextBox.Text = "";
                telefonTextBox.Text = "";

                numeTextBox.IsEnabled = true;
                prenumeTextBox.IsEnabled = true;
                telefonTextBox.IsEnabled = true;
            }
        }










        // tab Clienti
        private void btnInainte1_Click(object sender, RoutedEventArgs e)
        {
            inventarViewSource.View.MoveCurrentToNext();
        }

        private void btnInapoi1_Click(object sender, RoutedEventArgs e)
        {
            inventarViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNou1_Click(object sender, RoutedEventArgs e)
        {
            actionInventar = ActionState.New;
            btnNou1.IsEnabled = false;
            btnModifica1.IsEnabled = false;
            btnSterge1.IsEnabled = false;

            btnSalveaza1.IsEnabled = true;
            btnAnuleaza1.IsEnabled = true;

            btnInapoi1.IsEnabled = false;
            btnInainte1.IsEnabled = false;

            titluTextBox.IsEnabled = true;
            anLansareTextBox.IsEnabled = true;
            pretTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(titluTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(anLansareTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(pretTextBox, TextBox.TextProperty);

            titluTextBox.Text = "";
            anLansareTextBox.Text = "";
            pretTextBox.Text = "";
            Keyboard.Focus(titluTextBox);
        }

        private void btnModifica1_Click(object sender, RoutedEventArgs e)
        {
            actionInventar = ActionState.Edit;
            string tempTitle = titluTextBox.Text.ToString();
            string tempYear = anLansareTextBox.Text.ToString();
            string tempPrice = pretTextBox.Text.ToString();

            btnNou1.IsEnabled = false;
            btnModifica1.IsEnabled = false;
            btnSterge1.IsEnabled = false;

            btnSalveaza1.IsEnabled = true;
            btnAnuleaza1.IsEnabled = true;

            btnInapoi1.IsEnabled = false;
            btnInainte1.IsEnabled = false;

            titluTextBox.IsEnabled = true;
            anLansareTextBox.IsEnabled = true;
            pretTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(titluTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(anLansareTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(pretTextBox, TextBox.TextProperty);

            titluTextBox.Text = tempTitle;
            anLansareTextBox.Text = tempYear;
            pretTextBox.Text = tempPrice;
            Keyboard.Focus(titluTextBox);
        }

        private void btnSterge1_Click(object sender, RoutedEventArgs e)
        {
            actionInventar = ActionState.Delete;
            string tempTitle = titluTextBox.Text.ToString();
            string tempYear = anLansareTextBox.Text.ToString();
            string tempPrice = pretTextBox.Text.ToString();

            btnNou1.IsEnabled = false;
            btnModifica1.IsEnabled = false;
            btnSterge1.IsEnabled = false;

            btnSalveaza1.IsEnabled = true;
            btnAnuleaza1.IsEnabled = true;

            btnInapoi1.IsEnabled = false;
            btnInainte1.IsEnabled = false;

            BindingOperations.ClearBinding(titluTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(anLansareTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(pretTextBox, TextBox.TextProperty);

            titluTextBox.Text = tempTitle;
            anLansareTextBox.Text = tempYear;
            pretTextBox.Text = tempPrice;
        }

        private void btnAnuleaza1_Click(object sender, RoutedEventArgs e)
        {
            actionInventar = ActionState.Nothing;

            btnNou1.IsEnabled = true;
            btnModifica1.IsEnabled = true;
            btnSterge1.IsEnabled = true;

            btnSalveaza1.IsEnabled = false;
            btnAnuleaza1.IsEnabled = false;

            btnInapoi1.IsEnabled = true;
            btnInainte1.IsEnabled = true;

            titluTextBox.Text = "";
            anLansareTextBox.Text = "";
            pretTextBox.Text = "";


            titluTextBox.IsEnabled = true;
            anLansareTextBox.IsEnabled = true;
            pretTextBox.IsEnabled = true;

        }

        private void btnSalveaza1_Click(object sender, RoutedEventArgs e)
        {
            Inventar movie = null;
            if (actionInventar == ActionState.New)
            {
                try
                {
                    //instantiem entitatea Inventar
                    movie = new Inventar()
                    {
                        Titlu = titluTextBox.Text.Trim(),
                        AnLansare = int.Parse(anLansareTextBox.Text),
                        Pret = int.Parse(pretTextBox.Text),
                    };
                    //adaugam noua entitate
                    ctx.Inventars.Add(movie);
                    inventarViewSource.View.Refresh();
                    //salvam 
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNou1.IsEnabled = true;
                btnModifica1.IsEnabled = true;
                btnSalveaza1.IsEnabled = false;
                btnAnuleaza1.IsEnabled = false;
                btnSterge1.IsEnabled = true;
                btnInapoi1.IsEnabled = true;
                btnInainte1.IsEnabled = true;

                titluTextBox.Text = "";
                anLansareTextBox.Text = "";
                pretTextBox.Text = "";

                titluTextBox.IsEnabled = true;
                anLansareTextBox.IsEnabled = true;
                pretTextBox.IsEnabled = true;
            }
            else if (actionInventar == ActionState.Edit)
            {
                try
                {
                    movie = (Inventar)inventarDataGrid.SelectedItem;
                    movie.Titlu = titluTextBox.Text.Trim();
                    movie.AnLansare = int.Parse(anLansareTextBox.Text);
                    movie.Pret = int.Parse(pretTextBox.Text);
                    //salvam 
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // pozitionarea pe item-ul curent
                inventarViewSource.View.Refresh();
                inventarViewSource.View.MoveCurrentTo(movie);
                btnNou1.IsEnabled = true;
                btnModifica1.IsEnabled = true;
                btnSalveaza1.IsEnabled = false;
                btnAnuleaza1.IsEnabled = false;
                btnSterge1.IsEnabled = true;
                btnInapoi1.IsEnabled = true;
                btnInainte1.IsEnabled = true;

                titluTextBox.Text = "";
                anLansareTextBox.Text = "";
                pretTextBox.Text = "";

                titluTextBox.IsEnabled = true;
                anLansareTextBox.IsEnabled = true;
                pretTextBox.IsEnabled = true;
            }
            else if (actionInventar == ActionState.Delete)
            {
                try
                {
                    movie = (Inventar)inventarDataGrid.SelectedItem;
                    ctx.Inventars.Remove(movie);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventarViewSource.View.Refresh();
                btnNou1.IsEnabled = true;
                btnModifica1.IsEnabled = true;
                btnSalveaza1.IsEnabled = false;
                btnAnuleaza1.IsEnabled = false;
                btnInapoi1.IsEnabled = true;
                btnInainte1.IsEnabled = true;

                titluTextBox.Text = "";
                anLansareTextBox.Text = "";
                pretTextBox.Text = "";

                titluTextBox.IsEnabled = true;
                anLansareTextBox.IsEnabled = true;
                pretTextBox.IsEnabled = true;
            }
        }









        // tab-ul Comenzi
        private void btnInainte2_Click(object sender, RoutedEventArgs e)
        {
            clientiComenzisViewSource.View.MoveCurrentToNext();
        }

        private void btnInapoi2_Click(object sender, RoutedEventArgs e)
        {
            clientiComenzisViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNou2_Click(object sender, RoutedEventArgs e)
        {
            actionComenzi = ActionState.New;
            btnNou2.IsEnabled = false;
            btnModifica2.IsEnabled = false;
            btnSterge2.IsEnabled = false;

            btnSalveaza2.IsEnabled = true;
            btnAnuleaza2.IsEnabled = true;

            btnInapoi2.IsEnabled = false;
            btnInainte2.IsEnabled = false;
        }

        private void btnModifica2_Click(object sender, RoutedEventArgs e)
        {
            actionComenzi = ActionState.Edit;

            btnNou2.IsEnabled = false;
            btnModifica2.IsEnabled = false;
            btnSterge2.IsEnabled = false;

            btnSalveaza2.IsEnabled = true;
            btnAnuleaza2.IsEnabled = true;

            btnInapoi2.IsEnabled = false;
            btnInainte2.IsEnabled = false; 

        }

        private void btnSterge2_Click(object sender, RoutedEventArgs e)
        {
            actionComenzi = ActionState.Delete;

            btnNou2.IsEnabled = false;
            btnModifica2.IsEnabled = false;
            btnSterge2.IsEnabled = false;

            btnSalveaza2.IsEnabled = true;
            btnAnuleaza2.IsEnabled = true;

            btnInapoi2.IsEnabled = false;
            btnInainte2.IsEnabled = false; 
        }

        private void btnAnuleaza2_Click(object sender, RoutedEventArgs e)
        {
            actionComenzi = ActionState.Nothing;

            btnNou2.IsEnabled = true;
            btnModifica2.IsEnabled = true;
            btnSterge2.IsEnabled = true;

            btnSalveaza2.IsEnabled = false;
            btnAnuleaza2.IsEnabled = false;

            btnInapoi2.IsEnabled = true;
            btnInainte2.IsEnabled = true;
        }

        private void btnSalveaza2_Click(object sender, RoutedEventArgs e)
        {
            Comenzi order = null;
            Comenzi selectedOrder = null;
            if (actionComenzi == ActionState.New)
            {
                try
                {
                    //instantiem entitatea Comenzi 
                    Clienti customer = (Clienti)cmbClienti.SelectedItem;
                    Inventar video = (Inventar)cmbInventar.SelectedItem;

                    order = new Comenzi()
                    {
                        ClientId = customer.ClientId,
                        VideoId = video.VideoId,
                    };
                    //adaugam entitatea nou creata 
                    ctx.Comenzis.Add(order);
                    clientiComenzisViewSource.View.Refresh();
                    //salvam 
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNou2.IsEnabled = true;
                btnModifica2.IsEnabled = true;
                btnSalveaza2.IsEnabled = false;
                btnAnuleaza2.IsEnabled = false;
                btnSterge2.IsEnabled = true;
                btnInapoi2.IsEnabled = true;
                btnInainte2.IsEnabled = true;
            }
            else if (actionComenzi == ActionState.Edit)
            {
                selectedOrder = (Comenzi)comenzisDataGrid.SelectedItem;
                try
                {

                    int curr_id = selectedOrder.ComandaId;
                    var editedOrder = ctx.Comenzis.FirstOrDefault(s => s.ComandaId == curr_id);

                    if (editedOrder != null)
                    {
                        Clienti customer = (Clienti)cmbClienti.SelectedItem;
                        Inventar video = (Inventar)cmbInventar.SelectedItem;
                        editedOrder.ClientId = customer.ClientId;
                        editedOrder.VideoId = video.VideoId;
                        ctx.SaveChanges();
                    }
                    //salvam 

                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // pozitionarea pe item
                clientiComenzisViewSource.View.Refresh();
                clientiComenzisViewSource.View.MoveCurrentTo(selectedOrder);

                btnNou2.IsEnabled = true;
                btnModifica2.IsEnabled = true;
                btnSalveaza2.IsEnabled = false;
                btnAnuleaza2.IsEnabled = false;
                btnSterge2.IsEnabled = true;
                btnInapoi2.IsEnabled = true;
                btnInainte2.IsEnabled = true;
            }
            else if (actionComenzi == ActionState.Delete)
            {
                try
                {
                    selectedOrder = (Comenzi)comenzisDataGrid.SelectedItem;
                    int curr_id = selectedOrder.ComandaId;
                    var deletedOrder = ctx.Comenzis.FirstOrDefault(s => s.ComandaId == curr_id);

                    if (deletedOrder != null)
                    {
                        ctx.Comenzis.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Comanda a fost stearsa!", "Message");
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientiComenzisViewSource.View.Refresh();
                btnNou2.IsEnabled = true;
                btnModifica2.IsEnabled = true;
                btnSalveaza2.IsEnabled = false;
                btnAnuleaza2.IsEnabled = false;
                btnInapoi2.IsEnabled = true;
                btnInainte2.IsEnabled = true;
            }
        }
    }
}
