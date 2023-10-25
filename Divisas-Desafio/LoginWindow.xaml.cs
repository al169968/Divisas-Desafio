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
using System.Windows.Shapes;
using System.Data.Entity;
using Divisas_Desafio.Models;
using System.Data.Entity.Migrations;
using System.Runtime.CompilerServices;

namespace Divisas_Desafio
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        
        public LoginWindow()
        {
            InitializeComponent();
            
            ///Apis.CurrencyLayerAPI api = new Apis.CurrencyLayerAPI();
            //api.AsyncApiFunction();
           // api.currencyapi();
           // Apis.BanxicoWebScrapping oBanxico = new Apis.BanxicoWebScrapping();
            //oBanxico.AsyncWebScrapping();
           // oBanxico.LoadBanxico();

        }

        //Loggearse en la aplicacion con un usuario ya creado
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = Username.Text.Trim();
                string pass = Password.Password.ToString().Trim();
                
                if (username.Length < 1 || pass.Length < 1)
                {
                    MessageBox.Show("No pueden estar los campos vacios");
                    
                }
                else
                {
                    // Set cursor as waiting
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    disableBtns();
                    if (IsUserRegistered(username, pass))
                    {
                        clearFields();
                        this.Hide();
                        var main = new MainWindow();
                        main.Show(); 
                    }
                    else
                    {
                        MessageBox.Show("No existe alguien con esas credenciales");
                        clearFields();
                            
                    }

                }
                // Set cursor as normal
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                disableBtns();
            }
            catch {
                Exception ex = new Exception();
                Console.WriteLine(ex.Message);

            }
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Hidden;
            RegisterGrid.Visibility = Visibility.Visible;
        }

        //Crear nuevo usuario en la BD 
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            
            string Name = newName.Text;
            string Username = NewUsername.Text;
            string Pass = NewPassword.Password.ToString();
            if(Name.Length >0 && Username.Length > 0 && Pass.Length > 0)
            {
                using (Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities())
                {

                    if (IsUserRegistered(Username))
                    {
                        MessageBox.Show("El usuario ya existe");
                        clearFields();
                    }
                    else
                    {
                        // Set cursor as waiting
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                        Usuarios oUsuarios = new Usuarios();
                        oUsuarios.nombre = Name;
                        oUsuarios.username = Username;
                        oUsuarios.pass = Pass;

                        try
                        {
                            db.Usuarios.Add(oUsuarios);
                            db.SaveChanges();
                            MessageBox.Show("Usuario creado exitosamente.", "WPF");
                            clearFields();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    // Set cursor as normal
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                }
            }
            else
            {
                MessageBox.Show("No puede haber campos vacios","WPF");
                clearFields() ;
            }

           
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RegisterGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
            
        }

        private bool IsUserRegistered(string Username)
        {
            try {
                using (Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities())
                {
                    //Checando si ya existe el usuario con LINQ 
                    var existeUser = from d in db.Usuarios
                                     where d.username == Username
                                     select d;

                    return existeUser.Any();
                }
            }
            catch (Exception ex) {
                return true;
            }
             
        }
        private bool IsUserRegistered(string Username, string password)
        {
            try
            {
                using (Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities())
                {
                    //Checando si ya existe el usuario con LINQ 
                    //creamos el Query con LINQ buscar si existe algun campo con esas credenciales
                    var existeUser = from d in db.Usuarios
                                     where d.username == Username
                                     && d.pass == password
                                     select d;
                    

                    return  existeUser.Any();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Limpiar los campos de la informacion escrita
        private void clearFields()
        {
            if (LoginGrid.IsVisible)
            {
                Username.Text = "";
                Password.Password = "";
                
            }
            else
            {
                newName.Text = "";
                NewUsername.Text = "";
                NewPassword.Password = "";
                
            }
            
        }
        private void disableBtns()
        {
            if (LoginGrid.IsVisible)
            {
                if(LoginBtn.IsEnabled)
                {
                    LoginBtn.IsEnabled = false;
                }
                else
                {
                    LoginBtn.IsEnabled = true;  
                }
            }
        }

        private void NewUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* string username = newName.Text;
            var existeUser = from d in db.Usuarios
                             where d.username == username
                             select d;
            Username.Background = (existeUser.Count() > 0) ? Brushes.White : Brushes.Red;
           */
        }
    }
}
