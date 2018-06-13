using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace reestr
{
	public class NewDate : Form
	{
		public static string val;

		private bool okBut;

		private IContainer components = null;

		private Label lblCurVal;

		private Label lblNewVal;

		private Button btnOk;

		private DateTimePicker dtpNewVal;

		public TextBox tbCurVal;

		private Button btnCancel;

		public NewDate()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.okBut = false;
			base.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			NewDate.val = this.dtpNewVal.Value.ToShortDateString();
			this.okBut = true;
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.lblCurVal = new Label();
			this.lblNewVal = new Label();
			this.tbCurVal = new TextBox();
			this.btnOk = new Button();
			this.dtpNewVal = new DateTimePicker();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.lblCurVal.Anchor = AnchorStyles.Left;
			this.lblCurVal.AutoSize = true;
			this.lblCurVal.Location = new Point(12, 23);
			this.lblCurVal.Name = "lblCurVal";
			this.lblCurVal.Size = new System.Drawing.Size(105, 13);
			this.lblCurVal.TabIndex = 0;
			this.lblCurVal.Text = "Текущее значение:";
			this.lblNewVal.Anchor = AnchorStyles.Left;
			this.lblNewVal.AutoSize = true;
			this.lblNewVal.Location = new Point(25, 59);
			this.lblNewVal.Name = "lblNewVal";
			this.lblNewVal.Size = new System.Drawing.Size(92, 13);
			this.lblNewVal.TabIndex = 1;
			this.lblNewVal.Text = "Новое значение:";
			this.tbCurVal.Anchor = AnchorStyles.Right;
			this.tbCurVal.Location = new Point(135, 20);
			this.tbCurVal.Name = "tbCurVal";
			this.tbCurVal.ReadOnly = true;
			this.tbCurVal.Size = new System.Drawing.Size(137, 20);
			this.tbCurVal.TabIndex = 2;
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
			this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.btnOk.Location = new Point(42, 101);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.dtpNewVal.Anchor = AnchorStyles.Right;
			this.dtpNewVal.Location = new Point(135, 53);
			this.dtpNewVal.Name = "dtpNewVal";
			this.dtpNewVal.Size = new System.Drawing.Size(137, 20);
			this.dtpNewVal.TabIndex = 3;
			this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
			this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(180, 101);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.AcceptButton = this.btnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(308, 164);
			base.ControlBox = false;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.dtpNewVal);
			base.Controls.Add(this.tbCurVal);
			base.Controls.Add(this.lblNewVal);
			base.Controls.Add(this.lblCurVal);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NewDate";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Корректное значение даты";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public bool ShowForm(string formTitle)
		{
			this.Text = formTitle;
			base.ShowDialog();
			return this.okBut;
		}
	}
}