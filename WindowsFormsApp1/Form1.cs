using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
/*
 Auteur: Raphaël Vögeli
 Date: 10.01.2018
 Description: 
 Ce programme se connecte dans une base de donnée locale il demande qu'un base de donnée "test" aie été crée au préalable
 Le programme créer un table ajoute des nouvelles information puis les met à joue et les affiche dans le label prévu à cet effet  
*/

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //variable de connection à la base mySQL
        MySqlConnection mySqlConnection = new MySqlConnection(@"server=localhost; database=test; uid=root; pwd=root;");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //déclarations des commandes
            MySqlCommand select = new MySqlCommand("SELECT * FROM test WHERE id=1", mySqlConnection);
            MySqlCommand insert = new MySqlCommand("INSERT INTO test (id, password) values (1, '.Etml-')", mySqlConnection);
            MySqlCommand create = new MySqlCommand("CREATE TABLE test(id int, password text)", mySqlConnection);
            MySqlCommand update = new MySqlCommand("UPDATE test SET password='.Etml-44' WHERE id=1", mySqlConnection);

            //se connecte à la base de donnée
            mySqlConnection.Open();

            //crée les tables selon la commande create
            create.ExecuteNonQuery();

            //insére les valeurs
            insert.ExecuteNonQuery();

            //change la valeur dans la table de .Etml- à .Etml-44
            update.ExecuteNonQuery();

            //Met dans la variable reader le contenu de la requete select
            MySqlDataReader reader = select.ExecuteReader();

            //passe à travers les donnee de la requete (equivalent d'un fetch en php)
            while (reader.Read())
            {
                //met à jours l'affichage
                lblQuery.Text = "Contenu de la requête: " + Convert.ToString(reader["password"]);
            }
        }
    }
}
