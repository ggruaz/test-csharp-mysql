using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
/*
 Auteur: Raphaël Vögeli
 Date: 10.01.2018
 Description: 
 Ce programme se connecte sur une base de donnée locale il demande qu'une base de donnée "test" aie été créée au préalable
 Le programme créer un table ajoute des nouvelles information puis les met à joue et les affiche dans le label prévu à cet effet  
*/

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //variable de connection à la base mySQL
        MySqlConnection mySqlConnectionSrv = new MySqlConnection(@"server=localhost; uidTableroot; pwd=root;");
        MySqlConnection mySqlConnection = new MySqlConnection(@"server=localhost; database=db_test; uidTableroot; pwd=root;");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //déclarations des commandes
            MySqlCommand createdb = new MySqlCommand("CREATE DATABASE IF NOT EXISTS db_test", mySqlConnectionSrv);
            MySqlCommand select = new MySqlCommand("SELECT * FROM t_table WHERE idTable=1", mySqlConnection);
            MySqlCommand insert = new MySqlCommand("INSERT INTO t_table (idTable, tabColonne) values (1, '.Etml-')", mySqlConnection);
            MySqlCommand create = new MySqlCommand("CREATE TABLE IF NOT EXISTS t_table(idTable int, tabColonne text)", mySqlConnection);
            MySqlCommand update = new MySqlCommand("UPDATE t_table SET tabColonne='.Etml-44' WHERE idTable=1", mySqlConnection);

            //se connecte au serveur
            mySqlConnectionSrv.Open();

            //crée la db selon la commande createdb
            createdb.ExecuteNonQuery();
            mySqlConnectionSrv.Close();

            // se connecte à la bd
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
                lblQuery.Text = "Contenu de la requête: " + Convert.ToString(reader["tabColonne"]);
            }

            mySqlConnection.Close();
        }
    }
}
