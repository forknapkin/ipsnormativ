using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;

namespace reestr
{
	public class Form1 : Form
	{
		public static System.Data.DataTable dt;

		public static bool editMode;

		public static string numberOfInstr;

		public static bool ok;

		public static int idGen;

		private DataRow dr;

		public static int rowInd;

		private IContainer components = null;

		private Panel panel1;

		private MenuStrip menuStrip1;

		private DataGridView dataGridView1;

		private ToolStripMenuItem файлToolStripMenuItem;

		private ToolStripMenuItem сохранитьРеестрToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem выходToolStripMenuItem;

		private ToolStripMenuItem правкаToolStripMenuItem;

		private ToolStripMenuItem добавитьToolStripMenuItem;

		private ToolStripMenuItem изменитьToolStripMenuItem;

		private ToolStripMenuItem удалитьToolStripMenuItem;

		private System.Windows.Forms.Button btnAdd;

		private System.Windows.Forms.Button btnOpen;

		private System.Windows.Forms.Button btnExit;

		private System.Windows.Forms.TextBox tbSearch;

		private System.Windows.Forms.Button btnSearch;

		private System.Windows.Forms.Button btnDelete;

		private System.Windows.Forms.Button btnEdit;

		private ToolStripMenuItem tsmiImport;

		private System.Windows.Forms.Button btnWord;

		private System.Windows.Forms.Label lblQuantity;

		private System.Windows.Forms.Button btnAllRecs;

		static Form1()
		{
			Form1.editMode = false;
			Form1.ok = false;
		}

		public Form1()
		{
			this.InitializeComponent();
			Form1.dt = new System.Data.DataTable("Reestr");
			Form1.dt.Columns.Add("number", typeof(string));
			Form1.dt.Columns.Add("type", typeof(string));
			Form1.dt.Columns.Add("name", typeof(string));
			Form1.dt.Columns.Add("status", typeof(string));
			Form1.dt.Columns.Add("dateOfReg", typeof(string));
			Form1.dt.Columns.Add("numberOfReg", typeof(string));
			Form1.dt.Columns.Add("org", typeof(string));
			Form1.dt.Columns.Add("conf", typeof(string));
			Form1.dt.Columns.Add("dateOfConf", typeof(string));
			Form1.dt.Columns.Add("numOfDoc", typeof(string));
			Form1.dt.Columns.Add("concor", typeof(string));
			Form1.dt.Columns.Add("dateOfConcor", typeof(string));
			Form1.dt.Columns.Add("year", typeof(string));
			Form1.dt.Columns.Add("pages", typeof(string));
			Form1.dt.Columns.Add("place", typeof(string));
			Form1.dt.Columns.Add("key", typeof(string));
			Form1.dt.Columns.Add("text", typeof(string));
			Form1.dt.Columns.Add("annot", typeof(string));
			Form1.dt.Columns.Add("path", typeof(string));
			Form1.dt.Columns.Add("id", typeof(int));
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				(new Add()).ShowDialog();
				if (Form1.ok)
				{
					this.dataGridView1.DataSource = Form1.dt;
					Form1.dt.WriteXml("data.xml");
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
			Form1.ok = false;
			int count = this.dataGridView1.Rows.Count - 1;
			this.lblQuantity.Text = string.Concat("Записей: ", count.ToString());
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			Form1.numberOfInstr = this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells["number"].Value.ToString();
			for (int i = 0; i < Form1.dt.Rows.Count; i++)
			{
				if (Form1.numberOfInstr == Form1.dt.Rows[i].ItemArray[0].ToString())
				{
					this.dr = Form1.dt.Rows[i];
					Form1.dt.Rows.Remove(this.dr);
				}
			}
			this.dataGridView1.DataSource = Form1.dt;
			Form1.dt.WriteXml("data.xml");
			int count = this.dataGridView1.Rows.Count - 1;
			this.lblQuantity.Text = string.Concat("Записей: ", count.ToString());
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.DialogResult dialogResult;
			bool flag;
			bool flag1;
			System.Windows.Forms.DialogResult dialogResult1;
			Form1.editMode = true;
			Form1.numberOfInstr = this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells["id"].Value.ToString();
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
					Form1.rowInd = num;
					break;
				}
			}
			Form1.rowInd = Form1.dt.Rows.IndexOf(this.dr);
			DateTime dateTime = new DateTime();
			if ((!(this.dr.ItemArray[4].ToString() != "") || !(this.dr.ItemArray[4].ToString() != "без регистрации") ? false : !DateTime.TryParse(this.dr.ItemArray[4].ToString(), out dateTime)))
			{
				NewDate newDate = new NewDate();
				newDate.tbCurVal.Text = this.dr.ItemArray[4].ToString();
				if (newDate.ShowForm("Дата регистрации МЮ"))
				{
					goto Label1;
				}
				return;
			}
		Label4:
			flag = (this.dr.ItemArray[8].ToString() == "" ? false : !DateTime.TryParse(this.dr.ItemArray[8].ToString(), out dateTime));
			if (flag)
			{
				NewDate str = new NewDate();
				str.tbCurVal.Text = this.dr.ItemArray[8].ToString();
				if (str.ShowForm("Дата утверждения"))
				{
					goto Label2;
				}
				return;
			}
		Label5:
			flag1 = (this.dr.ItemArray[11].ToString() == "" ? false : !DateTime.TryParse(this.dr.ItemArray[11].ToString(), out dateTime));
			if (flag1)
			{
				NewDate newDate1 = new NewDate();
				newDate1.tbCurVal.Text = this.dr.ItemArray[11].ToString();
				if (newDate1.ShowForm("Дата согласования"))
				{
					Form1.dt.Rows[Form1.rowInd]["dateOfConcor"] = NewDate.val;
					this.dataGridView1.DataSource = Form1.dt;
					Form1.dt.WriteXml("data.xml");
					try
					{
						dialogResult = (new Add()).ShowDialog();
						if (Form1.ok)
						{
							this.dataGridView1.DataSource = Form1.dt;
							Form1.dt.WriteXml("data.xml");
						}
					}
					catch (Exception exception)
					{
						dialogResult1 = MessageBox.Show(exception.Message);
					}
					return;
				}
				return;
			}
			try
			{
				dialogResult = (new Add()).ShowDialog();
				if (Form1.ok)
				{
					this.dataGridView1.DataSource = Form1.dt;
					Form1.dt.WriteXml("data.xml");
				}
			}
			catch (Exception exception)
			{
				dialogResult1 = MessageBox.Show(exception.Message);
			}
			return;
		Label1:
			Form1.dt.Rows[Form1.rowInd]["dateOfReg"] = NewDate.val;
			this.dataGridView1.DataSource = Form1.dt;
			Form1.dt.WriteXml("data.xml");
			goto Label4;
		Label2:
			Form1.dt.Rows[Form1.rowInd]["dateOfConf"] = NewDate.val;
			this.dataGridView1.DataSource = Form1.dt;
			Form1.dt.WriteXml("data.xml");
			goto Label5;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Application.Exit();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.SelectedCells.Count <= 0)
			{
				MessageBox.Show("Выберите файл для открытия!");
			}
			else
			{
				try
				{
					int rowIndex = this.dataGridView1.SelectedCells[0].RowIndex;
					string str = string.Concat(System.Windows.Forms.Application.StartupPath, "\\pdffiles\\", this.dataGridView1["path", rowIndex].Value.ToString());
					if ((this.dataGridView1["path", rowIndex].Value.ToString() == "" ? true : !File.Exists(str)))
					{
						MessageBox.Show("Нет связанного с инструкцией файла!");
					}
					else
					{
						Process.Start(str);
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Form1.dt.WriteXml("data.xml");
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string lower = "";
			if (sender.Equals(this.btnSearch))
			{
				lower = this.tbSearch.Text.ToLower();
			}
			EnumerableRowCollection<DataRow> dataRows = Form1.dt.AsEnumerable().Where<DataRow>((DataRow search) => search.Field<string>("number").ToLower().Contains(lower) | search.Field<string>("type").ToLower().Contains(lower) | search.Field<string>("name").ToLower().Contains(lower) | search.Field<string>("status").ToLower().Contains(lower) | search.ItemArray[4].ToString().ToLower().Contains(lower) | search.Field<string>("numberOfReg").ToLower().Contains(lower) | search.Field<string>("org").ToLower().Contains(lower) | search.Field<string>("conf").ToLower().Contains(lower) | search.Field<string>("dateOfConf").ToLower().ToString().Contains(lower) | search.Field<string>("numOfDoc").ToLower().Contains(lower) | search.Field<string>("concor").ToLower().Contains(lower) | search.Field<string>("dateOfConcor").ToLower().ToString().Contains(lower) | search.Field<string>("year").ToLower().Contains(lower) | search.Field<string>("pages").ToLower().Contains(lower) | search.Field<string>("place").ToLower().Contains(lower) | search.Field<string>("key").ToLower().Contains(lower) | search.Field<string>("text").ToLower().Contains(lower) | search.Field<string>("annot").ToLower().Contains(lower)).OrderBy<DataRow, string>((DataRow search) => search.Field<string>("number"));
			this.dataGridView1.DataSource = dataRows.AsDataView<DataRow>();
			int count = this.dataGridView1.Rows.Count - 1;
			this.lblQuantity.Text = string.Concat("Записей: ", count.ToString());
		}

		private void btnWord_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					MessageBox.Show("Формирование отчета может занять продолжительное время! Это зависит от количества записей!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
					this.Cursor = Cursors.WaitCursor;
					object value = Missing.Value;
					Microsoft.Office.Interop.Word._Application applicationClass = new Microsoft.Office.Interop.Word.ApplicationClass();
					_Document __Document = applicationClass.Documents.Add(ref value, ref value, ref value, ref value);
					Paragraph paragraph = __Document.Content.Paragraphs.Add(ref value);
					string str = "";
					for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
					{
						if ((bool)this.dataGridView1.Rows[i].Cells["check"].EditedFormattedValue)
						{
							for (int j = 1; j < this.dataGridView1.ColumnCount - 1; j++)
							{
								str = string.Concat(new string[] { str, this.dataGridView1.Columns[j].HeaderText, ": ", this.dataGridView1[j, i].Value.ToString(), "\n" });
							}
							str = string.Concat(str, "\n\n");
						}
					}
					paragraph.Range.Text = str;
					applicationClass.Visible = true;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
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

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				if (!Directory.Exists(string.Concat(System.Windows.Forms.Application.StartupPath, "\\pdffiles")))
				{
					Directory.CreateDirectory(string.Concat(System.Windows.Forms.Application.StartupPath, "\\pdffiles"));
				}
				if (!File.Exists("data.xml"))
				{
					Form1.idGen = 0;
				}
				else
				{
					XmlReader xmlReader = XmlReader.Create("data.xml", new XmlReaderSettings());
					DataSet dataSet = new DataSet();
					dataSet.ReadXml(xmlReader);
					Form1.dt = dataSet.Tables[0];
					this.dataGridView1.DataSource = Form1.dt;
					Form1.idGen = Convert.ToInt32(Form1.dt.Rows[Form1.dt.Rows.Count - 1].ItemArray[Form1.dt.Columns.Count - 1]);
					xmlReader.Close();
				}
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn(false)
				{
					Name = "check"
				};
				this.dataGridView1.Columns.Add(dataGridViewCheckBoxColumn);
				this.dataGridView1.Columns["check"].DisplayIndex = 0;
				this.dataGridView1.Columns["check"].HeaderText = "";
				this.dataGridView1.Columns["check"].Width = 100;
				this.dataGridView1.Columns["number"].HeaderText = "Инв. № в СИФ";
				this.dataGridView1.Columns["type"].HeaderText = "Тип документа";
				this.dataGridView1.Columns["name"].HeaderText = "Название";
				this.dataGridView1.Columns["status"].HeaderText = "Статус";
				this.dataGridView1.Columns["dateOfReg"].HeaderText = "Дата регистрации МЮ";
				this.dataGridView1.Columns["numberOfReg"].HeaderText = "№ регистрации МЮ";
				this.dataGridView1.Columns["org"].HeaderText = "Организация-разработчик";
				this.dataGridView1.Columns["conf"].HeaderText = "Утверждение";
				this.dataGridView1.Columns["dateOfConf"].HeaderText = "Дата утверждения";
				this.dataGridView1.Columns["numOfDoc"].HeaderText = "№ документа";
				this.dataGridView1.Columns["concor"].HeaderText = "Согласование";
				this.dataGridView1.Columns["dateOfConcor"].HeaderText = "Дата согласования";
				this.dataGridView1.Columns["year"].HeaderText = "Год";
				this.dataGridView1.Columns["pages"].HeaderText = "Кол-во страниц";
				this.dataGridView1.Columns["place"].HeaderText = "Место издания";
				this.dataGridView1.Columns["key"].HeaderText = "Ключевые слова";
				this.dataGridView1.Columns["text"].HeaderText = "Текст";
				this.dataGridView1.Columns["annot"].HeaderText = "Примечание";
				this.dataGridView1.Columns["path"].HeaderText = "Файл";
				this.dataGridView1.Columns["id"].Visible = false;
				for (int i = 0; i < this.dataGridView1.Columns.Count - 1; i++)
				{
					this.dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
					this.dataGridView1.Columns[i].ReadOnly = true;
				}
				int count = this.dataGridView1.Rows.Count - 1;
				this.lblQuantity.Text = string.Concat("Записей: ", count.ToString());
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void InitializeComponent()
		{
			this.panel1 = new Panel();
			this.btnAllRecs = new System.Windows.Forms.Button();
			this.lblQuantity = new System.Windows.Forms.Label();
			this.btnWord = new System.Windows.Forms.Button();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.menuStrip1 = new MenuStrip();
			this.файлToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьРеестрToolStripMenuItem = new ToolStripMenuItem();
			this.tsmiImport = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.выходToolStripMenuItem = new ToolStripMenuItem();
			this.правкаToolStripMenuItem = new ToolStripMenuItem();
			this.добавитьToolStripMenuItem = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new ToolStripMenuItem();
			this.dataGridView1 = new DataGridView();
			this.panel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.btnAllRecs);
			this.panel1.Controls.Add(this.lblQuantity);
			this.panel1.Controls.Add(this.btnWord);
			this.panel1.Controls.Add(this.tbSearch);
			this.panel1.Controls.Add(this.btnSearch);
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Controls.Add(this.btnEdit);
			this.panel1.Controls.Add(this.btnOpen);
			this.panel1.Controls.Add(this.btnExit);
			this.panel1.Controls.Add(this.btnAdd);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 399);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(958, 38);
			this.panel1.TabIndex = 0;
			this.btnAllRecs.Anchor = AnchorStyles.Right;
			this.btnAllRecs.Location = new System.Drawing.Point(790, 8);
			this.btnAllRecs.Name = "btnAllRecs";
			this.btnAllRecs.Size = new System.Drawing.Size(75, 23);
			this.btnAllRecs.TabIndex = 13;
			this.btnAllRecs.Text = "Все записи";
			this.btnAllRecs.UseVisualStyleBackColor = true;
			this.btnAllRecs.Click += new EventHandler(this.btnSearch_Click);
			this.lblQuantity.Anchor = AnchorStyles.Right;
			this.lblQuantity.AutoSize = true;
			this.lblQuantity.Location = new System.Drawing.Point(795, 13);
			this.lblQuantity.Name = "lblQuantity";
			this.lblQuantity.Size = new System.Drawing.Size(0, 13);
			this.lblQuantity.TabIndex = 12;
			this.btnWord.Anchor = AnchorStyles.Right;
			this.btnWord.Location = new System.Drawing.Point(430, 8);
			this.btnWord.Name = "btnWord";
			this.btnWord.Size = new System.Drawing.Size(75, 23);
			this.btnWord.TabIndex = 11;
			this.btnWord.Text = "Word";
			this.btnWord.UseVisualStyleBackColor = true;
			this.btnWord.Click += new EventHandler(this.btnWord_Click);
			this.tbSearch.Anchor = AnchorStyles.Right;
			this.tbSearch.Location = new System.Drawing.Point(594, 10);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(190, 20);
			this.tbSearch.TabIndex = 10;
			this.btnSearch.Anchor = AnchorStyles.Right;
			this.btnSearch.Location = new System.Drawing.Point(511, 8);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(77, 23);
			this.btnSearch.TabIndex = 9;
			this.btnSearch.Text = "Поиск";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			this.btnDelete.Location = new System.Drawing.Point(193, 8);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 8;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.btnEdit.Location = new System.Drawing.Point(93, 8);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(94, 23);
			this.btnEdit.TabIndex = 7;
			this.btnEdit.Text = "Редактировать";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			this.btnOpen.Anchor = AnchorStyles.Right;
			this.btnOpen.Location = new System.Drawing.Point(302, 8);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(122, 23);
			this.btnOpen.TabIndex = 6;
			this.btnOpen.Text = "Открыть инструкцию";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
			this.btnExit.Anchor = AnchorStyles.Right;
			this.btnExit.Location = new System.Drawing.Point(871, 8);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 5;
			this.btnExit.Text = "Выход";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnAdd.Location = new System.Drawing.Point(12, 8);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "Добавить";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.файлToolStripMenuItem, this.правкаToolStripMenuItem });
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(958, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "Файл";
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.сохранитьРеестрToolStripMenuItem, this.tsmiImport, this.toolStripSeparator1, this.выходToolStripMenuItem });
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			this.сохранитьРеестрToolStripMenuItem.Name = "сохранитьРеестрToolStripMenuItem";
			this.сохранитьРеестрToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.сохранитьРеестрToolStripMenuItem.Text = "Сохранить реестр";
			this.сохранитьРеестрToolStripMenuItem.Visible = false;
			this.tsmiImport.Enabled = false;
			this.tsmiImport.Name = "tsmiImport";
			this.tsmiImport.Size = new System.Drawing.Size(205, 22);
			this.tsmiImport.Text = "Импорт данных из Excel";
			this.tsmiImport.Visible = false;
			this.tsmiImport.Click += new EventHandler(this.tsmiImport_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new EventHandler(this.btnExit_Click);
			this.правкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.добавитьToolStripMenuItem, this.изменитьToolStripMenuItem, this.удалитьToolStripMenuItem });
			this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
			this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.правкаToolStripMenuItem.Text = "Правка";
			this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
			this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.добавитьToolStripMenuItem.Text = "Добавить";
			this.добавитьToolStripMenuItem.Click += new EventHandler(this.btnAdd_Click);
			this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
			this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.изменитьToolStripMenuItem.Text = "Редактировать";
			this.изменитьToolStripMenuItem.Click += new EventHandler(this.btnEdit_Click);
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new EventHandler(this.btnDelete_Click);
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 24);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(958, 375);
			this.dataGridView1.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(958, 437);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(974, 476);
			base.Name = "Form1";
			this.Text = "ИПС НОРМАТИВ";
			base.Load += new EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void tsmiImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			Microsoft.Office.Interop.Excel.Application applicationClass = null;
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					try
					{
						applicationClass = new Microsoft.Office.Interop.Excel.ApplicationClass();
						Workbook workbook = applicationClass.Workbooks.Open(openFileDialog.FileName, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
						Worksheet sheets = (Worksheet)workbook.Sheets[1];
						List<string> strs = new List<string>();
						for (int i = 3; i <= 360; i++)
						{
							for (char j = 'C'; j <= 'T'; j = (char)(j + 1))
							{
                                strs.Add(sheets.Range[j.ToString(), i.ToString()].Text.ToString());
							}
							Form1.idGen++;
							Form1.dt.Rows.Add(new object[] { strs[0], strs[1], strs[2], strs[3], strs[4], strs[5], strs[6], strs[7], strs[8], strs[9], strs[10], strs[11], strs[12], strs[13], strs[14], strs[15], strs[16], strs[17], "", Form1.idGen });
							strs.Clear();
						}
						this.dataGridView1.DataSource = Form1.dt;
						Form1.dt.WriteXml("data.xml");
						System.Windows.Forms.Application.DoEvents();
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message);
					}
				}
				finally
				{
					applicationClass.Quit();
				}
			}
		}
	}
}