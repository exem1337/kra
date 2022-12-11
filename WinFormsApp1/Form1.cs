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
                    new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72 , 5 },
                    new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 },
                    new List<double>() { 5.34, 5.22, 5.44, 4.42, 5.5, 4.99, 4.55, 5.49, 5.29, 5.31, 5.72, 5 },
                    new List<double>() { 103.5, 97.6, 101.1, 84.6, 103, 100.2, 90.5, 102.8, 99.3, 100.1, 104, 100.8 }
                );
                kra.startKra();
                label1.Text = $"Эластичность {kra.elasticAl}";
                label2.Text = $"r {kra.r}";
                label3.Text = $"a0 {kra.countA0}";
                label4.Text = $"a1 {kra.countA1}";
                label5.Text = $"sigmaY {kra.sigmaY}";
                label6.Text = $"sigmaYX {kra.sigmaYX}";
                label7.Text = $"big R {kra.bigR}";
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
    }
}