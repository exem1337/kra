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
                    readDataGrid(0),
                    readDataGrid(1),
                    readDataGrid(2)
                ); 
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