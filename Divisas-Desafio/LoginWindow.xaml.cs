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

namespace Divisas_Desafio
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities();
        public LoginWindow()
        {
            InitializeComponent();
            Apis.CurrencyLayerAPI api = new Apis.CurrencyLayerAPI();
            api.currencyapi();
            //Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = Username.Text;
                string pass = Password.Password.ToString();
                if (username.Length < 1 || pass.Length < 1)
                {
                    MessageBox.Show("No pueden estar los campos vacios");
                }
                else
                {
                    //Abrimos conexion
                   // using(Models.DB_DivisasDesafioEntities db = new Models.DB_DivisasDesafioEntities())
                   // {
                        // Set cursor as waiting
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                        //creamos el Query con LINQ
                        var tabla = from d in db.Usuarios
                                    where d.username == username
                                    && d.pass == pass
                                    select d;
                        
                        if (tabla.Count() > 0)
                        {
                            //this.Hide();
                            var main = new MainWindow();
                            
                            main.Show(); 
                        }
                        else
                        {
                            MessageBox.Show("No existe alguien con esas credenciales");
                            Username.Text = "";
                            
                        }
                   // }
                }
                // Set cursor as normal
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                // MessageBox.Show(pass);
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

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            
            string Name = newName.Text;
            string Username = NewUsername.Text;
            string Pass = NewPassword.Password.ToString();
            //Insertando un nuevo row en la tabla usuarios
            var existeUser = from d in db.Usuarios
                            where d.username == Username
                            && d.pass == Pass
                            select d;
            if(existeUser.Count() > 0)
            {
                MessageBox.Show("El usuario ya existe");
                newName.Text = "";
                NewUsername.Text = "";
                NewPassword.Password = "";
            }
            else
            {
                // Set cursor as normal
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                Usuarios newUserValues = new Usuarios
                {
                    nombre = Name,
                    username = Username,
                    pass = Pass,
                };
                try
                {
                    db.Usuarios.Add(newUserValues);
                    db.SaveChanges();
                    
                        
                }
                catch (Exception ex) { 
                    MessageBox.Show(ex.Message);
                }
                // Set cursor as normal
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
    
                                    
           
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RegisterGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
            
        }
    }
}
