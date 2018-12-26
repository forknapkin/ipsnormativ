using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace reestr
{
	public class Add : Form
	{
		private string oldFileName;

		private string newFileName;

		private FileInfo fi;

		private DataRow dr;

		private int rowInd;

		private string dateOfReg = "";

		private string dateOfConf = "";

		private string dateOfConcor = "";

		private string year = "";

		private IContainer components = null;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		private TextBox tbNum;

		private Label label2;

		private TextBox tbType;

		private RichTextBox rtbName;

		private Button button2;

		private TextBox tbAnnot;

		private Label label18;

		private TextBox tbText;

		private Label label17;

		private TextBox tbKey;

		private Label label16;

		private TextBox tbPlace;

		private Label label15;

		private TextBox tbPages;

		private Label label14;

		private Label label13;

		private Label label12;

		private TextBox tbConcor;

		private Label label11;

		private TextBox tbNumOfDoc;

		private Label label10;

		private Label label9;

		private TextBox tbConf;

		private TextBox tbStatus;

		private TextBox tbNumOfReg;

		private TextBox tbOrg;

		private Button button1;

		private Panel panel3;

		private CheckBox checkBox3;

		private Panel panel2;

		private DateTimePicker dtpDateOfConcor;

		private CheckBox checkBox2;

		private Panel panel1;

		private DateTimePicker dtpDateOfConf;

		private Panel panel4;

		private DateTimePicker dtpDateOfReg;

		private CheckBox checkBox4;

		private Button button3;

		private TableLayoutPanel tableLayoutPanel2;

		private RichTextBox rtbPath;

		private Label label19;

		private ComboBox cbYear;

		private Label lblReg;

		private RadioButton rbNReg;

		private RadioButton rbReg;

		public Add()
		{
			this.InitializeComponent();
			if (Form1.editMode)
			{
				this.Text = "Редактировать";
				this.button2.Text = "Редактировать";
				this.dr = null;
				int num = 0;
				while (num < Form1.dt.Rows.Count)
				{
					if (Form1.numberOfInstr != Form1.dt.Rows[num].ItemArray[19].ToString())
					{
						num++;
					}
					else
					{
						this.dr = Form1.dt.Rows[num];
						this.rowInd = num;
						break;
					}
				}
				Form1.dt.Rows.IndexOf(this.dr);
				if (this.dr != null)
				{
					this.tbNum.Text = this.dr.ItemArray[0].ToString();
					this.tbType.Text = this.dr.ItemArray[1].ToString();
					this.rtbName.Text = this.dr.ItemArray[2].ToString();
					this.tbStatus.Text = this.dr.ItemArray[3].ToString();
					if ((this.dr.ItemArray[4].ToString() == "" ? false : this.dr.ItemArray[4].ToString() != "без регистрации"))
					{
						DateTime dateTime = new DateTime();
						if (DateTime.TryParse(this.dr.ItemArray[4].ToString(), out dateTime))
						{
							this.dtpDateOfReg.Value = dateTime;
							this.dateOfReg = this.dr.ItemArray[4].ToString();
							this.rbReg.Checked = true;
							this.dtpDateOfReg.Enabled = true;
						}
					}
					else if (this.dr.ItemArray[4].ToString() == "без регистрации")
					{
						this.rbNReg.Checked = true;
						this.dateOfReg = this.dr.ItemArray[4].ToString();
					}
					this.tbNumOfReg.Text = this.dr.ItemArray[5].ToString();
					this.tbOrg.Text = this.dr.ItemArray[6].ToString();
					this.tbConf.Text = this.dr.ItemArray[7].ToString();
					if (this.dr.ItemArray[8].ToString() != string.Empty)
					{
						this.dtpDateOfConf.Value = DateTime.Parse(this.dr.ItemArray[8].ToString());
						this.dateOfConf = this.dr.ItemArray[8].ToString();
					}
					this.tbNumOfDoc.Text = this.dr.ItemArray[9].ToString();
					this.tbConcor.Text = this.dr.ItemArray[10].ToString();
					if (dr.ItemArray[11].ToString() != string.Empty)
					{
						this.dtpDateOfConcor.Value = DateTime.Parse(this.dr.ItemArray[11].ToString());
						this.dateOfConcor = this.dr.ItemArray[11].ToString();
					}
					if (this.dr.ItemArray[12] != null)
					{
						this.cbYear.Text = this.dr.ItemArray[12].ToString();
						this.year = this.dr.ItemArray[12].ToString();
					}
					this.tbPages.Text = this.dr.ItemArray[13].ToString();
					this.tbPlace.Text = this.dr.ItemArray[14].ToString();
					this.tbKey.Text = this.dr.ItemArray[15].ToString();
					this.tbText.Text = this.dr.ItemArray[16].ToString();
					this.tbAnnot.Text = this.dr.ItemArray[17].ToString();
					this.rtbPath.Text = this.dr.ItemArray[18].ToString();
					this.oldFileName = this.rtbPath.Text;
					this.newFileName = this.rtbPath.Text;
				}
			}
		}

		private void Add_Load(object sender, EventArgs e)
		{
			for (int i = 1900; i <= DateTime.Now.Year; i++)
			{
				this.cbYear.Items.Add(i);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				DefaultExt = ".pdf",
				Title = "Выберите документ"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fi = new FileInfo(openFileDialog.FileName);
				this.rtbPath.Text = this.fi.Name;
				this.oldFileName = openFileDialog.FileName;
				this.newFileName = string.Concat(Application.StartupPath, "\\pdffiles\\", this.fi.Name);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DateTime value;
			if (!Form1.editMode)
			{
				int num = 0;
				while (num < Form1.dt.Rows.Count)
				{
					if (this.tbNum.Text != Form1.dt.Rows[num].ItemArray[0].ToString())
					{
						num++;
					}
					else
					{
						MessageBox.Show("Инстурция с таким номером уже существует!");
						return;
					}
				}
			}
			try
			{
				try
				{
					if (this.checkBox4.Checked)
					{
						value = this.dtpDateOfConf.Value;
						this.dateOfConf = value.ToShortDateString();
					}
					if (this.rbReg.Checked)
					{
						value = this.dtpDateOfReg.Value;
						this.dateOfReg = value.ToShortDateString();
					}
					if (this.checkBox2.Checked)
					{
						value = this.dtpDateOfConcor.Value;
						this.dateOfConcor = value.ToShortDateString();
					}
					if (this.checkBox3.Checked)
					{
						this.year = this.cbYear.Text;
					}
					if (Form1.editMode)
					{
						int text = Form1.dt.Rows.IndexOf(this.dr);
						Form1.dt.Rows[text]["number"] = this.tbNum.Text;
						Form1.dt.Rows[text]["type"] = this.tbType.Text;
						Form1.dt.Rows[text]["name"] = this.rtbName.Text;
						Form1.dt.Rows[text]["status"] = this.tbStatus.Text;
						Form1.dt.Rows[text]["dateOfReg"] = this.dateOfReg;
						Form1.dt.Rows[text]["numberOfReg"] = this.tbNumOfReg.Text;
						Form1.dt.Rows[text]["org"] = this.tbOrg.Text;
						Form1.dt.Rows[text]["conf"] = this.tbConf.Text;
						Form1.dt.Rows[text]["dateOfConf"] = this.dateOfConf;
						Form1.dt.Rows[text]["numOfDoc"] = this.tbNumOfDoc.Text;
						Form1.dt.Rows[text]["concor"] = this.tbConcor.Text;
						Form1.dt.Rows[text]["dateOfConcor"] = this.dateOfConcor;
						Form1.dt.Rows[text]["year"] = this.year;
						Form1.dt.Rows[text]["pages"] = this.tbPages.Text;
						Form1.dt.Rows[text]["place"] = this.tbPlace.Text;
						Form1.dt.Rows[text]["key"] = this.tbKey.Text;
						Form1.dt.Rows[text]["text"] = this.tbText.Text;
						Form1.dt.Rows[text]["annot"] = this.tbAnnot.Text;
						Form1.dt.Rows[text]["path"] = this.rtbPath.Text;
					}
					else
					{
						if (this.checkBox4.Checked)
						{
							value = this.dtpDateOfConf.Value;
							this.dateOfConf = value.ToShortDateString();
						}
						if (this.rbReg.Checked)
						{
							value = this.dtpDateOfReg.Value;
							this.dateOfReg = value.ToShortDateString();
						}
						else if (this.rbNReg.Checked)
						{
							this.dateOfReg = "без регистрации";
						}
						if (this.checkBox2.Checked)
						{
							value = this.dtpDateOfConcor.Value;
							this.dateOfConcor = value.ToShortDateString();
						}
						if (this.checkBox3.Checked)
						{
							this.year = this.cbYear.Text;
						}
						Form1.idGen++;
						Form1.dt.Rows.Add(new object[] { this.tbNum.Text, this.tbType.Text, this.rtbName.Text, this.tbStatus.Text, this.dateOfReg, this.tbNumOfReg.Text, this.tbOrg.Text, this.tbConf.Text, this.dateOfConf, this.tbNumOfDoc.Text, this.tbConcor.Text, this.dateOfConcor, this.year, this.tbPages.Text, this.tbPlace.Text, this.tbKey.Text, this.tbText.Text, this.tbAnnot.Text, this.rtbPath.Text, Form1.idGen });
					}
					if (this.oldFileName != this.newFileName)
					{
						File.Copy(this.oldFileName, this.newFileName, true);
					}
					Form1.ok = true;
					base.Close();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				Form1.editMode = false;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Form1.ok = false;
			Form1.editMode = false;
			base.Close();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.rbReg.Checked)
			{
				this.dtpDateOfConf.Enabled = false;
			}
			else
			{
				this.dtpDateOfConf.Enabled = true;
			}
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBox2.Checked)
			{
				this.dtpDateOfConcor.Enabled = false;
			}
			else
			{
				this.dtpDateOfConcor.Enabled = true;
			}
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBox3.Checked)
			{
				this.cbYear.Enabled = false;
			}
			else
			{
				this.cbYear.Enabled = true;
			}
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBox4.Checked)
			{
				this.dtpDateOfConf.Enabled = false;
			}
			else
			{
				this.dtpDateOfConf.Enabled = true;
			}
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
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.label8 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel4 = new Panel();
			this.dtpDateOfReg = new DateTimePicker();
			this.checkBox4 = new CheckBox();
			this.panel3 = new Panel();
			this.checkBox3 = new CheckBox();
			this.panel2 = new Panel();
			this.dtpDateOfConcor = new DateTimePicker();
			this.checkBox2 = new CheckBox();
			this.button3 = new Button();
			this.button2 = new Button();
			this.tbAnnot = new TextBox();
			this.label18 = new Label();
			this.tbText = new TextBox();
			this.label17 = new Label();
			this.tbKey = new TextBox();
			this.label16 = new Label();
			this.tbPlace = new TextBox();
			this.label15 = new Label();
			this.tbPages = new TextBox();
			this.label14 = new Label();
			this.label13 = new Label();
			this.label12 = new Label();
			this.tbConcor = new TextBox();
			this.label11 = new Label();
			this.tbNumOfDoc = new TextBox();
			this.label10 = new Label();
			this.label9 = new Label();
			this.tbConf = new TextBox();
			this.tbStatus = new TextBox();
			this.label1 = new Label();
			this.tbNum = new TextBox();
			this.label2 = new Label();
			this.tbType = new TextBox();
			this.rtbName = new RichTextBox();
			this.tbNumOfReg = new TextBox();
			this.tbOrg = new TextBox();
			this.button1 = new Button();
			this.panel1 = new Panel();
			this.dtpDateOfConf = new DateTimePicker();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.rtbPath = new RichTextBox();
			this.label19 = new Label();
			this.cbYear = new ComboBox();
			this.lblReg = new Label();
			this.rbReg = new RadioButton();
			this.rbNReg = new RadioButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.label3.Anchor = AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(32, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(118, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Название инструкции";
			this.label4.Anchor = AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(415, 71);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Статус";
			this.label5.Anchor = AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(29, 123);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Дата регистрации МЮ";
			this.label6.Anchor = AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(350, 123);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(106, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "№ регистрации МЮ";
			this.label7.Anchor = AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(9, 175);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(141, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "Организация-разработчик";
			this.label8.Anchor = AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(380, 175);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(76, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Утверждение";
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 5);
			this.tableLayoutPanel1.Controls.Add(this.button3, 3, 9);
			this.tableLayoutPanel1.Controls.Add(this.button2, 2, 9);
			this.tableLayoutPanel1.Controls.Add(this.tbAnnot, 3, 8);
			this.tableLayoutPanel1.Controls.Add(this.label18, 2, 8);
			this.tableLayoutPanel1.Controls.Add(this.tbText, 1, 8);
			this.tableLayoutPanel1.Controls.Add(this.label17, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.tbKey, 3, 7);
			this.tableLayoutPanel1.Controls.Add(this.label16, 2, 7);
			this.tableLayoutPanel1.Controls.Add(this.tbPlace, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.label15, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.tbPages, 3, 6);
			this.tableLayoutPanel1.Controls.Add(this.label14, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this.label13, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label12, 2, 5);
			this.tableLayoutPanel1.Controls.Add(this.tbConcor, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.tbNumOfDoc, 3, 4);
			this.tableLayoutPanel1.Controls.Add(this.label10, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.tbConf, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.tbStatus, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label8, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbNum, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbType, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rtbName, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbNumOfReg, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbOrg, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
			this.tableLayoutPanel1.Location = new Point(15, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 10;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(612, 525);
			this.tableLayoutPanel1.TabIndex = 0;
			this.panel4.Controls.Add(this.rbNReg);
			this.panel4.Controls.Add(this.dtpDateOfReg);
			this.panel4.Controls.Add(this.rbReg);
			this.panel4.Controls.Add(this.lblReg);
			this.panel4.Location = new Point(156, 107);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(147, 46);
			this.panel4.TabIndex = 50;
			this.dtpDateOfReg.Anchor = AnchorStyles.Right;
			this.dtpDateOfReg.Enabled = false;
			this.dtpDateOfReg.Location = new Point(24, 6);
			this.dtpDateOfReg.Name = "dtpDateOfReg";
			this.dtpDateOfReg.Size = new System.Drawing.Size(123, 20);
			this.dtpDateOfReg.TabIndex = 21;
			this.checkBox4.Anchor = AnchorStyles.Left;
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new Point(0, 16);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(15, 14);
			this.checkBox4.TabIndex = 0;
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.CheckedChanged += new EventHandler(this.checkBox4_CheckedChanged);
			this.panel3.Controls.Add(this.cbYear);
			this.panel3.Controls.Add(this.checkBox3);
			this.panel3.Location = new Point(156, 315);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(147, 41);
			this.panel3.TabIndex = 49;
			this.checkBox3.Anchor = AnchorStyles.Left;
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new Point(3, 13);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(15, 14);
			this.checkBox3.TabIndex = 0;
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
			this.panel2.Controls.Add(this.dtpDateOfConcor);
			this.panel2.Controls.Add(this.checkBox2);
			this.panel2.Location = new Point(462, 263);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(147, 41);
			this.panel2.TabIndex = 48;
			this.dtpDateOfConcor.Anchor = AnchorStyles.Right;
			this.dtpDateOfConcor.Enabled = false;
			this.dtpDateOfConcor.Location = new Point(24, 10);
			this.dtpDateOfConcor.Name = "dtpDateOfConcor";
			this.dtpDateOfConcor.Size = new System.Drawing.Size(123, 20);
			this.dtpDateOfConcor.TabIndex = 21;
			this.checkBox2.Anchor = AnchorStyles.Left;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new Point(3, 13);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(15, 14);
			this.checkBox2.TabIndex = 0;
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			this.button3.Anchor = AnchorStyles.None;
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Location = new Point(498, 485);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 46;
			this.button3.Text = "Отмена";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button2.Anchor = AnchorStyles.None;
			this.button2.AutoSize = true;
			this.button2.Location = new Point(345, 485);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 45;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.tbAnnot.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbAnnot.Location = new Point(462, 432);
			this.tbAnnot.Name = "tbAnnot";
			this.tbAnnot.Size = new System.Drawing.Size(147, 20);
			this.tbAnnot.TabIndex = 38;
			this.label18.Anchor = AnchorStyles.Right;
			this.label18.AutoSize = true;
			this.label18.Location = new Point(386, 435);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(70, 13);
			this.label18.TabIndex = 37;
			this.label18.Text = "Примечание";
			this.tbText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbText.Location = new Point(156, 432);
			this.tbText.Name = "tbText";
			this.tbText.Size = new System.Drawing.Size(147, 20);
			this.tbText.TabIndex = 36;
			this.label17.Anchor = AnchorStyles.Right;
			this.label17.AutoSize = true;
			this.label17.Location = new Point(113, 435);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(37, 13);
			this.label17.TabIndex = 35;
			this.label17.Text = "Текст";
			this.tbKey.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbKey.Location = new Point(462, 380);
			this.tbKey.Name = "tbKey";
			this.tbKey.Size = new System.Drawing.Size(147, 20);
			this.tbKey.TabIndex = 34;
			this.label16.Anchor = AnchorStyles.Right;
			this.label16.AutoSize = true;
			this.label16.Location = new Point(364, 383);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(92, 13);
			this.label16.TabIndex = 33;
			this.label16.Text = "Ключевые слова";
			this.tbPlace.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbPlace.Location = new Point(156, 380);
			this.tbPlace.Name = "tbPlace";
			this.tbPlace.Size = new System.Drawing.Size(147, 20);
			this.tbPlace.TabIndex = 32;
			this.label15.Anchor = AnchorStyles.Right;
			this.label15.AutoSize = true;
			this.label15.Location = new Point(66, 383);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(84, 13);
			this.label15.TabIndex = 31;
			this.label15.Text = "Место издания";
			this.tbPages.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbPages.Location = new Point(462, 328);
			this.tbPages.Name = "tbPages";
			this.tbPages.Size = new System.Drawing.Size(147, 20);
			this.tbPages.TabIndex = 30;
			this.label14.Anchor = AnchorStyles.Right;
			this.label14.AutoSize = true;
			this.label14.Location = new Point(371, 331);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(85, 13);
			this.label14.TabIndex = 29;
			this.label14.Text = "Кол-во страниц";
			this.label13.Anchor = AnchorStyles.Right;
			this.label13.AutoSize = true;
			this.label13.Location = new Point(125, 331);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(25, 13);
			this.label13.TabIndex = 27;
			this.label13.Text = "Год";
			this.label12.Anchor = AnchorStyles.Right;
			this.label12.AutoSize = true;
			this.label12.Location = new Point(349, 279);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(107, 13);
			this.label12.TabIndex = 25;
			this.label12.Text = "Дата согласования";
			this.tbConcor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbConcor.Location = new Point(156, 276);
			this.tbConcor.Name = "tbConcor";
			this.tbConcor.Size = new System.Drawing.Size(147, 20);
			this.tbConcor.TabIndex = 24;
			this.label11.Anchor = AnchorStyles.Right;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(71, 279);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(79, 13);
			this.label11.TabIndex = 23;
			this.label11.Text = "Согласование";
			this.tbNumOfDoc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbNumOfDoc.Location = new Point(462, 224);
			this.tbNumOfDoc.Name = "tbNumOfDoc";
			this.tbNumOfDoc.Size = new System.Drawing.Size(147, 20);
			this.tbNumOfDoc.TabIndex = 22;
			this.label10.Anchor = AnchorStyles.Right;
			this.label10.AutoSize = true;
			this.label10.Location = new Point(381, 227);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(75, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "№ документа";
			this.label9.Anchor = AnchorStyles.Right;
			this.label9.AutoSize = true;
			this.label9.Location = new Point(48, 227);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(102, 13);
			this.label9.TabIndex = 19;
			this.label9.Text = "Дата утверждения";
			this.tbConf.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbConf.Location = new Point(462, 172);
			this.tbConf.Name = "tbConf";
			this.tbConf.Size = new System.Drawing.Size(147, 20);
			this.tbConf.TabIndex = 18;
			this.tbStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbStatus.Location = new Point(462, 68);
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.Size = new System.Drawing.Size(147, 20);
			this.tbStatus.TabIndex = 14;
			this.label1.Anchor = AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(68, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Инв. № в СИФ";
			this.tbNum.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbNum.Location = new Point(156, 16);
			this.tbNum.Name = "tbNum";
			this.tbNum.Size = new System.Drawing.Size(147, 20);
			this.tbNum.TabIndex = 9;
			this.label2.Anchor = AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(373, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Тип документа";
			this.tbType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbType.Location = new Point(462, 16);
			this.tbType.Name = "tbType";
			this.tbType.Size = new System.Drawing.Size(147, 20);
			this.tbType.TabIndex = 11;
			this.rtbName.Dock = DockStyle.Fill;
			this.rtbName.Location = new Point(156, 55);
			this.rtbName.Name = "rtbName";
			this.rtbName.Size = new System.Drawing.Size(147, 46);
			this.rtbName.TabIndex = 13;
			this.rtbName.Text = "";
			this.tbNumOfReg.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbNumOfReg.Location = new Point(462, 120);
			this.tbNumOfReg.Name = "tbNumOfReg";
			this.tbNumOfReg.Size = new System.Drawing.Size(147, 20);
			this.tbNumOfReg.TabIndex = 15;
			this.tbOrg.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			this.tbOrg.Location = new Point(156, 172);
			this.tbOrg.Name = "tbOrg";
			this.tbOrg.Size = new System.Drawing.Size(147, 20);
			this.tbOrg.TabIndex = 16;
			this.button1.Anchor = AnchorStyles.None;
			this.button1.Location = new Point(192, 485);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 42;
			this.button1.Text = "Обзор";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.panel1.Controls.Add(this.checkBox4);
			this.panel1.Controls.Add(this.dtpDateOfConf);
			this.panel1.Location = new Point(156, 211);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(147, 46);
			this.panel1.TabIndex = 47;
			this.dtpDateOfConf.Anchor = AnchorStyles.Right;
			this.dtpDateOfConf.Enabled = false;
			this.dtpDateOfConf.Location = new Point(23, 13);
			this.dtpDateOfConf.Name = "dtpDateOfConf";
			this.dtpDateOfConf.Size = new System.Drawing.Size(123, 20);
			this.dtpDateOfConf.TabIndex = 21;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.Controls.Add(this.rtbPath, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label19, 0, 0);
			this.tableLayoutPanel2.Location = new Point(3, 471);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 29.78723f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70.21277f));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(147, 47);
			this.tableLayoutPanel2.TabIndex = 51;
			this.rtbPath.BackColor = SystemColors.Control;
			this.rtbPath.BorderStyle = BorderStyle.None;
			this.rtbPath.Dock = DockStyle.Fill;
			this.rtbPath.Location = new Point(3, 16);
			this.rtbPath.Name = "rtbPath";
			this.rtbPath.ReadOnly = true;
			this.rtbPath.Size = new System.Drawing.Size(141, 28);
			this.rtbPath.TabIndex = 46;
			this.rtbPath.Text = "";
			this.label19.AutoSize = true;
			this.label19.Location = new Point(3, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(100, 13);
			this.label19.TabIndex = 0;
			this.label19.Text = "Файл инструкции:";
			this.cbYear.Enabled = false;
			this.cbYear.Location = new Point(23, 10);
			this.cbYear.Name = "cbYear";
			this.cbYear.Size = new System.Drawing.Size(121, 21);
			this.cbYear.Sorted = true;
			this.cbYear.TabIndex = 1;
			this.lblReg.AutoSize = true;
			this.lblReg.Location = new Point(22, 30);
			this.lblReg.Name = "lblReg";
			this.lblReg.Size = new System.Drawing.Size(92, 13);
			this.lblReg.TabIndex = 22;
			this.lblReg.Text = "без регистрации";
			this.lblReg.Click += new EventHandler(this.lblReg_Click);
			this.rbReg.AutoSize = true;
			this.rbReg.Location = new Point(4, 11);
			this.rbReg.Name = "rbReg";
			this.rbReg.Size = new System.Drawing.Size(14, 13);
			this.rbReg.TabIndex = 23;
			this.rbReg.UseVisualStyleBackColor = true;
			this.rbReg.CheckedChanged += new EventHandler(this.rbReg_CheckedChanged);
			this.rbNReg.AutoSize = true;
			this.rbNReg.Checked = true;
			this.rbNReg.Location = new Point(4, 30);
			this.rbNReg.Name = "rbNReg";
			this.rbNReg.Size = new System.Drawing.Size(14, 13);
			this.rbNReg.TabIndex = 24;
			this.rbNReg.TabStop = true;
			this.rbNReg.UseVisualStyleBackColor = true;
			this.rbNReg.CheckedChanged += new EventHandler(this.rbReg_CheckedChanged);
			base.AcceptButton = this.button2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button3;
			base.ClientSize = new System.Drawing.Size(639, 539);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Add";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Добавить";
			base.TopMost = true;
			base.Load += new EventHandler(this.Add_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			base.ResumeLayout(false);
		}

		private void lblReg_Click(object sender, EventArgs e)
		{
		}

		private void rbReg_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.rbReg.Checked)
			{
				this.dtpDateOfReg.Enabled = false;
				this.dateOfReg = "без регистрации";
			}
			else
			{
				this.dtpDateOfReg.Enabled = true;
				this.dateOfReg = this.dtpDateOfReg.Value.ToShortDateString();
			}
		}
	}
}