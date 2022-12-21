using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;

namespace WinFormsApp1
{
    internal class DatabaseWorks
    {

        SqlConnection connection;

        public DatabaseWorks()
        {
            connection = new SqlConnection(LocalStorage.Credentials);
            connection.Open();
        }

        public DataView GetKey(string Table, string KeyColumn, string FindByColumn, string Key)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT {KeyColumn} FROM {Table} WHERE {FindByColumn} = {Key};", connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet.Tables[0].DefaultView;
        }

        public DataView FetchData(string Columns, string TablesName, string Arguments)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT {Columns} FROM {TablesName} {Arguments};", connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet.Tables[0].DefaultView;
        }

        // Справочники

        public void AddTechPref(string Name, string ShName)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Технический_показатель (наименование, краткое_название) VALUES ('{Name}', '{ShName}');", connection);
            sql.ExecuteNonQuery();
        }

        public void AddOtdel(string Name, string ShName)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Отдел (название, краткое_название) VALUES ('{Name}', '{ShName}');", connection);
            sql.ExecuteNonQuery();
        }

        public void AddMetric(string Name, string ShName)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Единицы_измерения (название, краткое_название) VALUES ('{Name}', '{ShName}');", connection);
            sql.ExecuteNonQuery();
        }

        public void AddRadiatorType(string Name, string ShName, string Description, int MetricKey)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Вид_выпускаемой_продукции (наименование_вида, краткое_название_вида, описание, код_единицы_измерения) VALUES ('{Name}', '{ShName}', '{Description}', {MetricKey});", connection);
            sql.ExecuteNonQuery();
        }

        public void AddPredpriyatie()
        {

        }

        public void ConnectRadTypeTechPref(int RadTypeKey, int TechPrefKey)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Вид_продукции_технический_показатель (код_вида, код_показателя) VALUES ({RadTypeKey}, {TechPrefKey});", connection);
            sql.ExecuteNonQuery();
        }

        public void AddRadiator(string Name, string ShName, string Description, int RadKey, double Stoikost, double Vibrator, double MinTemp, double Srok)
        {
            SqlCommand sql = new SqlCommand($"INSERT INTO Выпускаемая_продукция (наименование_выпускаемой_продукции, краткое_название, описание, стойкость_прокладок, устойчивость_к_вибр, мин_температура, срок_службы, код_вида) VALUES ('{Name}', '{ShName}', '{Description}', {Stoikost}, {Vibrator}, {MinTemp}, {Srok}, {RadKey});", connection);
            sql.ExecuteNonQuery();
        }

    }
}
