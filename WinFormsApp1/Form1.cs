using System;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected KRA kra = new KRA();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kra.setValues(
                    readDataGrid(0), readDataGrid(1), readDataGrid(2), readDataGrid(3)
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72 , 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 },
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 }
                );
                kra.startKra();
                label1.Text = $"Эластичность {kra.elasticAl}";
                label2.Text = $"Коэффициент корреляции r : {kra.r}";
                label3.Text = $"Параметр a0 : {kra.countA0}";
                label4.Text = $"Параметр a1 : {kra.countA1}";
                label5.Text = $"sigmaY {kra.sigmaY}";
                label6.Text = $"sigmaYX {kra.sigmaYX}";
                label7.Text = $"Коэффициент детерминации R : {kra.bigR}";
                label8.Text = $"Уравнение регрессии : {kra.regressionExpressionAl}";

                label13.Text = $"Эластичность {kra.elasticCu}";
                label15.Text = $"Коэффициент корреляции r : {kra.rCu}";
                label17.Text = $"Параметр a0 : {kra.countA0Cu}";
                label19.Text = $"Параметр a1 : {kra.countA1Cu}";
                label18.Text = $"sigmaY {kra.sigmaYCu}";
                label16.Text = $"sigmaYX {kra.sigmaYXCu}";
                label14.Text = $"Коэффициент детерминации R : {kra.bigRCu}";
                label20.Text = $"Уравнение регрессии : {kra.regressionExpressionCu}";
            }
            catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        private List<double> readDataGrid(int index)
        {
            List<double> data = new List<double>();
            
            for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
            {
                try
                {
                    data.Add(Convert.ToDouble(dataGridView1.Rows[rows].Cells[index].Value));
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }

            return data;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double max = 0;
            string maxi = "";

            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {

                if (Convert.ToString(row.Cells[0].Value) == "")
                    break;
                max = Convert.ToDouble(row.Cells[1].Value);
                for (int i = 2; i < 5; i++)
                {
                    if (max < Convert.ToDouble(row.Cells[i].Value))
                    {
                        max = Convert.ToDouble(row.Cells[i].Value);
                    }
                }

                row.Cells[5].Value = max.ToString();
            }
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                if (max < Convert.ToDouble(row.Cells[5].Value))
                {
                    max = Convert.ToDouble(row.Cells[5].Value);
                    maxi = row.Cells[0].Value.ToString();
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                }
            }
            label12.Text = maxi;
            label12.Visible = true;
            label11.Visible = true;
        }
    }
}