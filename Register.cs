﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATGate
{
    public partial class Register : Form
    {

        Task<bool> regStatus;

        public Register()
        {
            InitializeComponent();
        }

        private async void Btn_register_Click(object sender, EventArgs e)
        {

            EnableControls(false);

            string username = tb_username.Text;
            string password = tb_password.Text;

            if (!username.Equals("") && !password.Equals("")) //field not empty
            {
                if (password.Length > 3) //password length > 3
                {

                    DBWrapper dw = new DBWrapper("default");
                    regStatus = await Task.Factory.StartNew(() => dw.CreateAccountWithMacVerificationAsync(username, password));
                }
                else
                {
                    MessageBox.Show("密码过短。");
                }
                
            }
            else
            {
                MessageBox.Show("请输入用户名与密码。");
            }
            try
            {
                if (regStatus.Result)
                {
                    this.Close();
                }
                else
                {
                    EnableControls(true);
                }
            }
            catch (NullReferenceException)
            {
                EnableControls(true);
            }

        }

        private void EnableControls(bool enable) {
            if (enable)
            {
                btn_register.Enabled = true;
                btn_register.UseWaitCursor = false;
                tb_username.Enabled = true;
                tb_password.Enabled = true;
                btn_register.Text = "注册";
            }
            else
            {
                btn_register.Enabled = false;
                btn_register.UseWaitCursor = true;
                tb_username.Enabled = false;
                tb_password.Enabled = false;
                btn_register.Text = "注册中";
            }
        }

        private void Tb_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"[^a-z^A-Z^0-9]"))
                {
                    // Stop the character from being entered into the control since it is illegal.
                    e.Handled = true;
                }
            }
        }

        private void Tb_username_Enter(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.Show(@"只允许字母与数字", (TextBox)sender, 0, 23, 3000);
        }

        private void Tb_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"[^a-z^A-Z^0-9^+^@^/^*]"))
                {
                    e.Handled = true;
                }
            }

        }

        private void Tb_password_Enter(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.Show(@"只允许字母,数字以及 + @ * / 等符号", (TextBox)sender, 0, 23, 3000);
        }

    }
}
