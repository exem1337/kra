using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        string[] login = { "admin", "ruk", "operator" };
        string[] pass = { "admin", "123", "123" };
        int N;
        string hash1;
        string hash2;
        int id;
        int num;
        int numtrans = 1;
        int counttrans = 10;
        string role = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (string log in login)
            {
                dataGridView1.Rows.Add(login[i], pass[i]);
                dataGridView2.Rows.Add(login[i], pass[i]);
                i++;

            }
            i = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            id = 0;
            foreach (string log in login)
            {
                if (textBox1.Text == log)
                {
                    flag = true;
                    break;
                }
                id++;


            }
            if (flag == true)
            {

                Random rnd = new Random();
                N = rnd.Next();

            }
            else { MessageBox.Show("Ошибка аутентификации: неверный логин или пароль"); }
            flag = false;

            //if (textBox1.Text == "admin")
            //    UserStore.role = "operator";
            //else UserStore.role = "rukovoditel";

            if (textBox1.Text == "operator")
            {
                UserStore.role = "operator";
                MessageBox.Show("Вы вошли как оператор");
            }

            if (textBox1.Text == "ruk")
            {
                UserStore.role = "rukovoditel";
                MessageBox.Show("Вы вошли как руководитель");
            }

            if (textBox1.Text == "admin")
            {
                UserStore.role = "admin";
                MessageBox.Show("Вы вошли как администратор. Доступны расширенные действия ");
            }


            string NP = N + textBox2.Text;
            hash1 = SHA(NP);
            string NP1 = N + pass[id];
            hash2 = SHA(NP1);
            if (hash1 == hash2)
            {

                MessageBox.Show("Аутентификация успешна");
                Form1 form1 = new Form1();
                form1.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка аутентификации: неверный логин или пароль");
            }
        }

        int counter = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            // string hash1=String.Empty;
            // string hash2=String.Empty; 
            switch (counter)
            {
                case 0:
                    {

                        // label9.Text = SHA(N+textBox1.Text);

                        string NP = N + textBox2.Text;
                        hash1 = SHA(NP);

                        counter++;
                        break;
                    }
                case 1:
                    {

                        label5.Text = "Ответ: Генерация своего хеш-пароля Hash(N,P1). Проверка совпадения Hash(N,P) и Hash(N,P1)";
                        label6.Text = "Запрос: Подтверждение аутентификации";
                        label10.Visible = true;
                        textBox4.Text = hash1;
                        textBox4.Visible = true;

                        string NP1 = N + pass[id];
                        hash2 = SHA(NP1);
                        label11.Visible = true;
                        textBox5.Text = hash2;
                        textBox5.Visible = true;
                        counter++;
                        break;
                    }
                case 2:
                    {
                        if (hash1 == hash2)
                        {
                            label5.Text = "Ответ: Аутентификация пройдена";
                            label6.Text = "Запрос: Подтверждение аутентификации";
                            MessageBox.Show("Аутентификация успешна");
                            Form1 form1 = new Form1();
                            form1.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка аутентификации: неверный логин или пароль");
                        }
                        counter = 0;
                        break;

                    }
            }
        }
        public static string SHA(string input)
        {
            SHA256 hash = SHA256.Create();
            return BitConverter.ToString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool flag = false;
            num = 0;
            foreach (string log in login)
            {
                if (textBox6.Text == log)
                {
                    flag = true;
                    break;
                }
                num++;


            }
            if (flag == true)
            {
                label14.Text = "Сервер";
                label15.Text = "Пользователь";
                label14.Visible = true;
                label15.Visible = true;
                string k1 = pass[num];
                for (int i = 0; i < counttrans; i++)
                {
                    k1 = SHA(k1);
                    dataGridView3.Rows.Add(i + 1, k1);
                }
                string k2 = textBox7.Text;
                for (int i = 0; i < counttrans; i++)
                {
                    k2 = SHA(k2);
                    dataGridView4.Rows.Add(i + 1, k2);
                }
                dataGridView3.Visible = true;
                dataGridView4.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;
                button4.Visible = true;
                label20.Text = $"Номер транзакции {numtrans}";
                label18.Text = $"Ключ пользователя для {numtrans} транзакции";
                label19.Text = $"Ключ сервера для {numtrans} транзакции";
                label20.Visible = true;
                label18.Visible = true;
                // label19.Visible = true;
                textBox10.Text = Convert.ToString(dataGridView3.Rows[counttrans - numtrans - 1].Cells[1].Value);
                textBox10.Visible = true;
                label21.Text = $"Ключ полученный после хеширования ключа переданного пользователем ";
            }
            else { MessageBox.Show("Ошибка аутентификации: неверный логин или пароль"); }
            flag = false;
        }
        int count;
        private void button5_Click(object sender, EventArgs e)
        {
            switch (count)
            {
                case 0:
                    {
                        label20.Text = $"Номер транзакции {numtrans}";
                        label18.Text = $"Ключ пользователя для {numtrans} транзакции";
                        label19.Text = $"Ключ сервера для {numtrans} транзакции";
                        label20.Visible = true;
                        label18.Visible = true;
                        label19.Visible = true;
                        textBox10.Text = Convert.ToString(dataGridView3.Rows[counttrans - numtrans - 1].Cells[1].Value);
                        textBox10.Visible = true;
                        textBox11.Text = Convert.ToString(dataGridView4.Rows[counttrans - numtrans].Cells[1].Value);
                        textBox11.Visible = true;

                        count++;
                        break;
                    }
                case 1:
                    {


                        label21.Visible = true;
                        textBox12.Text = SHA(textBox9.Text);
                        textBox12.Visible = true;


                        count++;
                        break;
                    }
                case 2:
                    {
                        if (textBox12.Text == textBox11.Text)
                        {
                            textBox11.Text = textBox12.Text;
                            textBox12.Clear();
                            numtrans = numtrans + 1;
                            label20.Text = $"Номер транзакции {numtrans}";
                            label18.Text = $"Ключ пользователя для {numtrans} транзакции";
                            textBox10.Text = Convert.ToString(dataGridView3.Rows[counttrans - numtrans - 1].Cells[1].Value);
                            button5.Visible = false;
                            textBox9.Clear();
                            MessageBox.Show("Аутентификация успешна");
                        }
                        else
                        {
                            textBox9.Clear();
                            MessageBox.Show("Ошибка аутентификации: неверный логин или пароль");
                        }
                        count = 0;
                        break;

                    }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
        }
    }
}
