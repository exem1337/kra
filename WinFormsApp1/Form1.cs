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

                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 },
                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 }

                readDataGrid1(0), readDataGrid1(1), readDataGrid2(0), readDataGrid2(1)
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 },
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 }
                );
                kra.startKra();

                if (Double.IsNaN(kra.elasticAl))
                {
                    MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                label2.Text = $"Эластичность {kra.elasticAl}";
                label3.Text = $"Эластичность {kra.elasticCu}";

                label4.Text = $"Уравнение регрессии {kra.regressionExpressionAl}";
                label5.Text = $"Уравнение регрессии {kra.regressionExpressionCu}";
                
                label6.Text = $"Коэффициент кореляции {kra.r}";
                label7.Text = $"Коэффициент кореляции {kra.rCu}";
                
                label8.Text = $"Параметр а0 {kra.countA0}";
                label13.Text = $"Параметр а0 {kra.countA0Cu}";

                label21.Text = $"Параметр а1 {kra.countA1}";
                label20.Text = $"Параметр а1 {kra.countA1Cu}";

                label19.Text = $"SigmaY {kra.sigmaY}";
                label18.Text = $"SigmaY {kra.sigmaYCu}";

                label8.Text = $"SigmaYX {kra.sigmaYX}";
                label9.Text = $"SigmaYX {kra.sigmaYXCu}";

                label17.Text = $"Коэфициент детерминации R {kra.bigR}";
                label16.Text = $"Коэфициент детерминации R {kra.bigRCu}";

                if (kra.bigR > kra.bigRCu)
                {
                    label14.Text = "Итог прогнозирования: увеличить производство алюминевых радиаторов на следующий период";
                }
                else if (kra.bigR == kra.bigRCu)
                {
                    label14.Text = "Сбалансированное производство. Изменений не требуется.";
                }
                else
                {
                    label14.Text = "Итог прогнозирования: увеличить производство медных радиаторов на следующий период";
                }
            }
            catch(Exception)    
            {
                MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      


        private List<double> readDataGrid1(int index)
        {
            List<double> data = new List<double>();
            
            for (int rows = 0; rows < dataGridView1.Rows.Count-1; rows++)
            {
                try
                {
                    data.Add(Convert.ToDouble(dataGridView1.Rows[rows].Cells[index].Value));
                }
                //catch (Exception exc)
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                    //MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return data;
        }

        private List<double> readDataGrid2(int index)
        {
            List<double> data = new List<double>();

            for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
            {
                try
                {
                    data.Add(Convert.ToDouble(dataGridView1.Rows[rows].Cells[index].Value));
                }
                //catch (Exception exc)
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                    //MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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