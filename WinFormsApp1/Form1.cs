using System;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillRadiatorTypeSelect();
            this.toggleLabels(false);
        }

        protected KRA kra = new KRA();

        private void toggleLabels(bool value)
        {
            label2.Visible = value;
            label3.Visible = value;
            label4.Visible = value;
            label5.Visible = value;
            label6.Visible = value;
            label7.Visible = value;
            label8.Visible = value;
            label13.Visible = value;
            label21.Visible = value;
            label20.Visible = value;
            label19.Visible = value;
            label18.Visible = value;
            label8.Visible = value;
            label9.Visible = value;
            label17.Visible = value;
            label16.Visible = value;
            label14.Visible = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kra.setValues(

                new List<double>() { 100, 150, 130, 45, 75 },
                new List<double>() { 100, 142, 140, 80, 60 },
                new List<double>() { 100, 150, 130, 45, 75 },
                new List<double>() { 100, 142, 140, 80, 60 }

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
                    return;
                }

                this.toggleLabels(true);

                label2.Text = $"Эластичность {kra.elasticAl}";
                label3.Text = $"Эластичность {kra.elasticCu}";

                label4.Text = $"Уравнение регрессии Алюминий {kra.regressionExpressionAl}";
                label5.Text = $"Уравнение регрессии Медь {kra.regressionExpressionCu}";
                
                label6.Text = $"Коэффициент кореляции r: {kra.r}";
                label7.Text = $"Коэффициент кореляции r: {kra.rCu}";
                
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
            dataGridViewTemp.DataSource = db.FetchData("код_вида", "Вид_выпускаемой_продукции", "");
            int RadTypeKey = Convert.ToInt32(dataGridViewTemp.Rows[dataGridViewTemp.Rows.Count - 2].Cells[0].Value);
            foreach (DataGridViewRow row in dataGridViewTechPrefSelect.SelectedRows)
            {
                db.ConnectRadTypeTechPref(RadTypeKey, Convert.ToInt32(row.Cells[0].Value));
            }
            FillProductionTypeGrid();
        }

        private void buttonAddMetric_Click(object sender, EventArgs e)
        {
            DatabaseWorks db = new DatabaseWorks();
            db.AddMetric(textBoxMetricName.Text, textBoxMetricShName.Text);
        }

        void FillTechPrefSelect()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTechPrefSelect.DataSource = db.FetchData("код_технического_показателя, наименование", "Технический_показатель", "");
        }

        void FillTechMetricSelect()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTemp.DataSource = db.FetchData("название", "Единицы_измерения", "");
            comboBoxRadiatorMetrics.Items.Clear();
            for(int i = 0; i < dataGridViewTemp.Rows.Count - 1; i++)
            {
                comboBoxRadiatorMetrics.Items.Add(dataGridViewTemp.Rows[i].Cells[0].Value.ToString());
            }
        }

        void FillProductionTypeGrid()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewProdType.DataSource = db.FetchData("наименование_вида, краткое_название_вида, описание, Единицы_измерения.название AS Единица_измерения", "Вид_выпускаемой_продукции, Единицы_измерения", "WHERE Вид_выпускаемой_продукции.код_единицы_измерения = Единицы_измерения.код_единицы_измерения");
        }

        void FillProductionGrid()
        {
            DatabaseWorks db = new DatabaseWorks();

        }

        void FillRadiatorTypeSelect()
        {
            DatabaseWorks db = new DatabaseWorks();
            dataGridViewTemp.DataSource = db.FetchData("наименование_вида", "Вид_выпускаемой_продукции", "");
            comboBoxRadiatorType.Items.Clear();
            for(int i = 0; i < dataGridViewTemp.Rows.Count - 1; i++)
            {
                comboBoxRadiatorType.Items.Add(dataGridViewTemp.Rows[i].Cells[0].Value.ToString());
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl2.SelectedIndex)
            {
                case 0:
                    FillRadiatorTypeSelect();
                    break;
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

        private void textBoxTechProkladka_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if(!double.TryParse(textBoxTechProkladka.Text, out dummy))
            {
                if(textBoxTechProkladka.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ");
                }
                textBoxTechProkladka.Text = String.Empty;
            }
        }

        private void textBoxTechVibrator_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if (!double.TryParse(textBoxTechVibrator.Text, out dummy))
            {
                if (textBoxTechVibrator.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ, только цифры");
                }
                textBoxTechVibrator.Text = String.Empty;
            }
        }

        private void textBoxTechTemp_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if (!double.TryParse(textBoxTechTemp.Text, out dummy))
            {
                if (textBoxTechTemp.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ, только цифры");
                }
                textBoxTechTemp.Text = String.Empty;
            }
        }

        private void textBoxTechYears_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if (!double.TryParse(textBoxTechYears.Text, out dummy))
            {
                if (textBoxTechYears.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ, только цифры");
                }
                textBoxTechYears.Text = String.Empty;
            }
        }

        private void textBoxWorkDays_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if (!double.TryParse(textBoxWorkDays.Text, out dummy))
            {
                if (textBoxWorkDays.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ, только цифры");
                }
                textBoxWorkDays.Text = String.Empty;
            }
        }

        private void textBoxDemandedCapacity_TextChanged(object sender, EventArgs e)
        {
            double dummy;
            if (!double.TryParse(textBoxDemandedCapacity.Text, out dummy))
            {
                if (textBoxDemandedCapacity.Text != String.Empty)
                {
                    MessageBox.Show("Проверьте правильность введенной информации. Недопустимый символ, только цифры");
                }
                textBoxDemandedCapacity.Text = String.Empty;
            }
        }
    }
}