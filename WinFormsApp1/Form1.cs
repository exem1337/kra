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

                new List<double>() { 100, 150, 130, 45, 75 },
                new List<double>() { 100, 142, 140, 80, 60 },
                new List<double>() { 100, 150, 130, 45, 75 },
                new List<double>() { 100, 142, 140, 80, 60 }

                //readDataGrid(0), readDataGrid(1), readDataGrid(2), readDataGrid(3)
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
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
                label8.Text = $"Уравнение регрессии алюминиевых радиаторов : {kra.regressionExpressionAl}";

                label13.Text = $"Эластичность {kra.elasticCu}";
                label15.Text = $"Коэффициент корреляции r : {kra.rCu}";
                label17.Text = $"Параметр a0 : {kra.countA0Cu}";
                label19.Text = $"Параметр a1 : {kra.countA1Cu}";
                label18.Text = $"sigmaY {kra.sigmaYCu}";
                label16.Text = $"sigmaYX {kra.sigmaYXCu}";
                label14.Text = $"Коэффициент детерминации R : {kra.bigRCu}";
                label20.Text = $"Уравнение регрессии медных радиаторов : {kra.regressionExpressionCu}";

                if (kra.bigR > kra.bigRCu) {
                    label23.Text = $"В следующем месяце требуется увеличить производство : алюминиевых радиаторов";
                }
                else { label23.Text = $"В следующем месяце требуется увеличить производство : медных радиаторов ";
                }

                if (kra.bigR == kra.bigRCu)
                {
                    label23.Text = $"Сбалансированное производство, требуется производство в равных количествах";
                }
                else { }
           


            }
            catch(Exception exc)
            {
                //throw new Exception(exc.Message);
                MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dataGridView1.Rows.Clear();

            }
        }

      


        private List<double> readDataGrid(int index)
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

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void buttonRadiatorTypeAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddTechShow_Click(object sender, EventArgs e)
        {
            DatabaseWorks db = new DatabaseWorks();
            db.AddTechPref(textBoxTechShowName.Text,textBoxTechShowShName.Text);
        }

        private void buttonOtdelAdd_Click(object sender, EventArgs e)
        {
            DatabaseWorks db = new DatabaseWorks();
            db.AddOtdel(textBoxOtdelName.Text, textBoxOtdelShName.Text);
        }

        private void buttonRadiatorTypeAdd_Click_1(object sender, EventArgs e)
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTemp.DataSource = db.GetKey("Единицы_измерения", "код_единицы_измерения", "название", $"'{comboBoxRadiatorMetrics.Text}'");
            db.AddRadiator(textBoxRadiatorTypeName.Text, textBoxRTypeShortName.Text, textBoxRTypeDescription.Text, Convert.ToInt32(dataGridViewTemp.Rows[0].Cells[0].Value));

        }

        private void buttonAddMetric_Click(object sender, EventArgs e)
        {
            DatabaseWorks db = new DatabaseWorks();
            db.AddMetric(textBoxMetricName.Text, textBoxMetricShName.Text);
        }

        void FillTechPrefSelect()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTechPrefSelect.DataSource = db.ReturnTable("код_технического_показателя, наименование", "Технический_показатель", "");
        }

        void FillTechMetricSelect()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTemp.DataSource = db.ReturnTable("название", "Единицы_измерения", "");
            comboBoxRadiatorMetrics.Items.Clear();
            for(int i = 0; i < dataGridViewTemp.Rows.Count - 1; i++)
            {
                comboBoxRadiatorMetrics.Items.Add(dataGridViewTemp.Rows[i].Cells[0].Value.ToString());
            }
        }

        void FillProductionTypeGrid()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewProdType.DataSource = db.ReturnTable("наименование_вида, краткое_название_вида, описание, Единицы_измерения.название AS Единица_измерения", "Вид_выпускаемой_продукции, Единицы_измерения", "WHERE Вид_выпускаемой_продукции.код_единицы_измерения = Единицы_измерения.код_единицы_измерения");
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl2.SelectedIndex)
            {
                case 3:
                    FillTechMetricSelect();
                    FillTechPrefSelect();
                    FillProductionTypeGrid();
                    break;
            }
        }

        private void textBoxRTypeShortName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}