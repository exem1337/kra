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

            dataGridView4.Rows.Add(-12, 2500, 2450, 50000, 5, 200);
            dataGridView4.Rows.Add(-13, 2500, 3500, 0, 0, 125);
            dataGridView4.Rows.Add(-5, 3550, 2525, 32000, 10, 150);
            dataGridView4.Rows.Add(7, 2500, 2500, 41000, 10, 220);
            dataGridView4.Rows.Add(16, 3550, 4600, 5000, 15, 260);
            dataGridView4.Rows.Add(20, 2450, 2500, 22000, 0, 230);
            dataGridView4.Rows.Add(22, 2500, 2550, 40000, 5, 280);
        }

        protected KRA kra = new KRA();

        private void toggleLabels(bool value)
        {
            label3.Visible = value;
            label5.Visible = value;
            label9.Visible = value;
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

            for (int rows = 0; rows < dataGridView4.Rows.Count - 1; rows++)
            {
                try
                {
                    data.Add(Convert.ToDouble(dataGridView4.Rows[rows].Cells[index].Value));
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
                    maxi = row.Cells[0].Value.ToString()!;
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

        private void calcKraAl()
        {
            List<List<double>> aluminium = new List<List<double>>();
            List<double> aluminiumResult = readDataGrid1(5);
            resultLabel.Text = "Степень кореляции";
            resultFuncLabel.Text = "Результатирующая функция";
            fisherLabel.Text = "Критерий Фишера";

            aluminium.Add(readDataGrid1(0));
            aluminium.Add(readDataGrid1(1));
            aluminium.Add(readDataGrid1(2));
            aluminium.Add(readDataGrid1(3));
            aluminium.Add(readDataGrid1(4));

            kra.setValues(
                aluminium, aluminiumResult
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

            for (int i = 0; i < kra.pairCorrelations.Count; i++)
            {
                pairAlGrid.Rows.Add(
                    kra.pairCorrelations[i][0],
                    kra.pairCorrelations[i][1],
                    kra.pairCorrelations[i][2],
                    kra.pairCorrelations[i][3],
                    kra.pairCorrelations[i][4],
                    kra.pairCorrelations[i][5]
                );
                if (i != 5)
                {
                    pairAlGrid.Rows[i].HeaderCell.Value = $"x{i + 1}";
                }
                else
                {
                    pairAlGrid.Rows[i].HeaderCell.Value = "y";
                }
            }

            kra._result.Clear();
        }

        private void calcCraCu()
        {
            List<List<double>> cu = new List<List<double>>();
            List<double> cuResult = readDataGrid2(5);
            corelationCu.Text = "Степень кореляции";
            resultFuncCuLabel.Text = "Результатирующая функция";
            fisherCuLabel.Text = "Критерий Фишера";

            cu.Add(readDataGrid2(0));
            cu.Add(readDataGrid2(1));
            cu.Add(readDataGrid2(2));
            cu.Add(readDataGrid2(3));
            cu.Add(readDataGrid2(4));

            kra.setValues(
                cu, cuResult
            );
            kra.startKra();
            corelationCu.Text += "\n";

            for (int i = 0; i < kra._result.Count; i++)
            {
                if (kra._result[i].r >= 0.7)
                {
                    dataGridView4.Columns[i].HeaderCell.Style.BackColor = Color.Green;
                }
                if (kra._result[i].r < 0.7 && kra._result[i].r >= 0.3)
                {
                    dataGridView4.Columns[i].HeaderCell.Style.BackColor = Color.Yellow;
                }
                if (kra._result[i].r < 0.3)
                {
                    dataGridView4.Columns[i].HeaderCell.Style.BackColor = Color.Red;
                }
                corelationCu.Text += (dataGridView4.Columns[i].HeaderText).ToString() + ' ' + kra._result[i].r.ToString() + $", Коэфициент детерминации R: {kra._result[i].bigR}" + '\n';
            }

            for (int j = 0; j < kra._result.Count; j++)
            {
                if (j == 0)
                {
                    resultFuncCuLabel.Text += " " + kra.kraA + $" + {kra._result[j].r}x{j + 1}";
                    continue;
                }
                resultFuncCuLabel.Text += $" + {kra._result[j].r}x{j + 1}";
            }

            fisherCuLabel.Text += $": {kra.fisher}";

            for (int i = 0; i < kra.pairCorrelations.Count; i++)
            {
                pairCuGrid.Rows.Add(
                    kra.pairCorrelations[i][0],
                    kra.pairCorrelations[i][1],
                    kra.pairCorrelations[i][2],
                    kra.pairCorrelations[i][3],
                    kra.pairCorrelations[i][4],
                    kra.pairCorrelations[i][5]
                );
                if (i != 5)
                {
                    pairCuGrid.Rows[i].HeaderCell.Value = $"x{i + 1}";
                }
                else
                {
                    pairCuGrid.Rows[i].HeaderCell.Value = "y";
                }
            }

            kra._result.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            calcKraAl();
            calcCraCu();    

                
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc.ToString());    
            //    MessageBox.Show("Некоректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}