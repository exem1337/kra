using System;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (UserStore.role == "operator")
            {
                tabControl1.TabPages[2].Parent = null;
            }
            this.toggleLabels(false);
            dataGridView3.Rows.Add(-12, 2500, 2450, 50000, 5, 200);
            dataGridView3.Rows.Add(-13, 2500, 3500, 0, 0, 125);
            dataGridView3.Rows.Add(-5, 3550, 2525, 32000, 10, 150);
            dataGridView3.Rows.Add(7, 2500, 2500, 41000, 10, 220);
            dataGridView3.Rows.Add(16, 3550, 4600, 5000, 15, 260);
            dataGridView3.Rows.Add(20, 2450, 2500, 22000, 0, 230);
            dataGridView3.Rows.Add(22, 2500, 2550, 40000, 5, 280);
        }

        protected KRA kra = new KRA();

        private void toggleLabels(bool value)
        {
            label3.Visible = value;
            label5.Visible = value;
            label7.Visible = value;
            label13.Visible = value;
            label20.Visible = value;
            label18.Visible = value;
            label9.Visible = value;
            label16.Visible = value;
            label14.Visible = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<List<double>> aluminium = new List<List<double>>();
                List<double> aluminiumResult = readDataGrid1(5);

                aluminium.Add(readDataGrid1(0));
                aluminium.Add(readDataGrid1(1));
                aluminium.Add(readDataGrid1(2));
                aluminium.Add(readDataGrid1(3));
                aluminium.Add(readDataGrid1(4));

                kra.setValues(
                    aluminium, aluminiumResult
                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 },
                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 }

                
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 },
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 }
                );
                kra.startKra();

                foreach (KRAResultItem res in kra._result)
                {
                    resultLabel.Text = res.ToString();
                }

                //if (Double.IsNaN(kra.elasticAl))
                //{
                //    MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //this.toggleLabels(true);

                //label2.Text = $"Эластичность {kra.elasticAl}";
                //label3.Text = $"Эластичность {kra.elasticCu}";

                //label4.Text = $"Уравнение регрессии Алюминий {kra.regressionExpressionAl}";
                //label5.Text = $"Уравнение регрессии Медь {kra.regressionExpressionCu}";

                //label6.Text = $"Коэффициент кореляции r: {kra.r}";
                //label7.Text = $"Коэффициент кореляции r: {kra.rCu}";

                //label8.Text = $"Параметр а0 {kra.countA0}";
                //label13.Text = $"Параметр а0 {kra.countA0Cu}";

                //label21.Text = $"Параметр а1 {kra.countA1}";
                //label20.Text = $"Параметр а1 {kra.countA1Cu}";

                //label19.Text = $"SigmaY {kra.sigmaY}";
                //label18.Text = $"SigmaY {kra.sigmaYCu}";

                //label8.Text = $"SigmaYX {kra.sigmaYX}";
                //label9.Text = $"SigmaYX {kra.sigmaYXCu}";

                //label17.Text = $"Коэфициент детерминации R {kra.bigR}";
                //label16.Text = $"Коэфициент детерминации R {kra.bigRCu}";

                //if (kra.bigR > kra.bigRCu)
                //{
                //    label14.Text = "Итог прогнозирования: увеличить производство алюминевых радиаторов на следующий период";
                //}
                //else if (kra.bigR == kra.bigRCu)
                //{
                //    label14.Text = "Сбалансированное производство. Изменений не требуется.";
                //}
                //else
                //{
                //    label14.Text = "Итог прогнозирования: увеличить производство медных радиаторов на следующий период";
                //}
            }
            catch (Exception)
            {
                MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private List<double> readDataGrid1(int index)
        {
            List<double> data = new List<double>();

            for (int rows = 0; rows < dataGridView3.Rows.Count - 1; rows++)
            {
                try
                {
                    data.Add(Convert.ToDouble(dataGridView3.Rows[rows].Cells[index].Value));
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //try
            //{
                List<List<double>> aluminium = new List<List<double>>();
                List<double> aluminiumResult = readDataGrid1(5);
                resultLabel.Text = "Степень кореляции";
                resultFuncLabel.Text = "Результатирующая функция";

                aluminium.Add(readDataGrid1(0));
                aluminium.Add(readDataGrid1(1));
                aluminium.Add(readDataGrid1(2));
                aluminium.Add(readDataGrid1(3));
                aluminium.Add(readDataGrid1(4));

                kra.setValues(
                    aluminium, aluminiumResult
                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 },
                //new List<double>() { 100, 150, 130, 45, 75 },
                //new List<double>() { 100, 142, 140, 80, 60 }


                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 },
                //new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                //new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 }
                );
                kra.startKra();
                resultLabel.Text += "\n";

                for (int i = 0; i < kra._result.Count; i++)
                {
                    if (kra._result[i].r >= 0.7)
                    {
                        dataGridView3.Columns[i].HeaderCell.Style.BackColor = Color.Green;
                    }
                    if (kra._result[i].r < 0.7 && kra._result[i].r >= 0.3)
                    {
                        dataGridView3.Columns[i].HeaderCell.Style.BackColor = Color.Yellow;
                    }
                    if (kra._result[i].r < 0.3)
                    {
                        dataGridView3.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                    }
                    resultLabel.Text += (dataGridView3.Columns[i].HeaderText).ToString() + ' ' + kra._result[i].r.ToString() + $", Коэфициент детерминации R: {kra._result[i].bigR}" + '\n';
                }

                for (int j = 0; j < kra._result.Count; j++)
                {
                    if (j == 0)
                    {
                        resultFuncLabel.Text += " " + kra.kraA + $" + {kra._result[j].r}x{j + 1}";
                        continue;
                    }
                    resultFuncLabel.Text += $" + {kra._result[j].r}x{j + 1}";
                }

                fisherLabel.Text += $": {kra.fisher}";

                kra._result.Clear();

                //if (Double.IsNaN(kra.elasticAl))
                //{
                //    MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //this.toggleLabels(true);

                //label2.Text = $"Эластичность {kra.elasticAl}";
                //label3.Text = $"Эластичность {kra.elasticCu}";

                //label4.Text = $"Уравнение регрессии Алюминий {kra.regressionExpressionAl}";
                //label5.Text = $"Уравнение регрессии Медь {kra.regressionExpressionCu}";

                //label6.Text = $"Коэффициент кореляции r: {kra.r}";
                //label7.Text = $"Коэффициент кореляции r: {kra.rCu}";

                //label8.Text = $"Параметр а0 {kra.countA0}";
                //label13.Text = $"Параметр а0 {kra.countA0Cu}";

                //label21.Text = $"Параметр а1 {kra.countA1}";
                //label20.Text = $"Параметр а1 {kra.countA1Cu}";

                //label19.Text = $"SigmaY {kra.sigmaY}";
                //label18.Text = $"SigmaY {kra.sigmaYCu}";

                //label8.Text = $"SigmaYX {kra.sigmaYX}";
                //label9.Text = $"SigmaYX {kra.sigmaYXCu}";

                //label17.Text = $"Коэфициент детерминации R {kra.bigR}";
                //label16.Text = $"Коэфициент детерминации R {kra.bigRCu}";

                //if (kra.bigR > kra.bigRCu)
                //{
                //    label14.Text = "Итог прогнозирования: увеличить производство алюминевых радиаторов на следующий период";
                //}
                //else if (kra.bigR == kra.bigRCu)
                //{
                //    label14.Text = "Сбалансированное производство. Изменений не требуется.";
                //}
                //else
                //{
                //    label14.Text = "Итог прогнозирования: увеличить производство медных радиаторов на следующий период";
                //}
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc.ToString());    
            //    MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}